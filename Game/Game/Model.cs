using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Model
    {
        Controller controller; // Injection
        public Controller Controller
        {
            get { return controller; }
            set { controller = value; }
        }

        Player upPlayer;
        Player downPlayer;
        List<Ball> balls; // набор всех имеющихся шаров
        int width; // ширина поля
        int height; // высота поля
        int level; // уровень жёсткости игры (1 - легко, 5 - сложно)
        int[] points; // число очков игроков 0 - верхний, 1 - нижний
        volatile int hits; // число отбитий шара о что-либо

        public int Level { get { return level; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }
        
        // число совершённых отбитий
        public int Hits { get { return hits; } set { hits = value; } }

        // значения по умолчанию
        public static int rocketSpeedDefault = 15;
        public static int rocketHeightDefault = 6;
        public static int rocketWidthDefault = 42;
        public static int ballsSpeedDefault = 10;
        public static int ballsDiametrDefault = 36;
        public static Color ballsColorDefault = Color.Aqua;
        public static int winPoints = 50;
        public static int levelUpCondition = 20;
        
        // API для бота
        public List<Ball> GetBalls()
        { return balls; } // получить список всех мячей
        public void GoToRight()
        {
            TrigUPPlayerToRight();
        } // пойти вправо
        public void GoToLeft()
        {
            TrigUPPlayerToLeft();
        } // пойти влево
        public Player GetPlayer()
        {
            return upPlayer;
        } // получить мой DTO Player

        // флаг инициализации
        bool playerAdded = false;

        // подъём уровня сложности
        public void UpLevel()
        {
            level++;
            Ball.speed = (int)(Ball.speed + Math.Log10(level));
            if (level == 20) AddGeneratedBall();
            if (level == 100) AddGeneratedBall();
            if (level == 400) AddGeneratedBall();
            if (level == 1000) AddGeneratedBall();
            if (level % 4000 == 0) AddGeneratedBall();
            controller.UpLevel(level);
        }

        public void AddGeneratedBall()
        {
            int xCoordSpawn = 0;
            int yCoordSpawn = 0;

            Random randomGen = new Random();
            xCoordSpawn = 30 + randomGen.Next(width - 60);
            yCoordSpawn = 30 + randomGen.Next(height - 60);

            randomGen = new Random();
            double cosGen = randomGen.NextDouble()*2-1;
            int sign = Math.Sign(-1+randomGen.Next(1)) == 0 ? 1 : Math.Sign(-1 + randomGen.Next(1));
            double sinGen = sign * Math.Sqrt(1 - cosGen * cosGen);

            Ball ball = new Ball(xCoordSpawn, yCoordSpawn, ballsDiametrDefault, ballsColorDefault, Math.Atan2(sinGen, cosGen));

            balls.Add(ball);
            ball.Id = controller.AddBallRedrawing(ball);
        }

        public Model(Controller controller)
        {
            this.controller = controller;
            this.level = 1;
            balls = new List<Ball>();
        }

        public void Initializate(int width, int height)
        {
            this.width = width;
            this.height = height;
            points = new int[2];
            Ball.speed = ballsSpeedDefault;
            AddGeneratedBall();
            AddPlayers();
        }

        public void UpdateField(int width)
        {
            this.width = width;
        }

        public void AddPlayers()
        {
            if (!playerAdded)
            {
                upPlayer = new Player(Player.Position.UP, Color.Black, 0, rocketSpeedDefault, width / 2, 0, rocketHeightDefault, rocketWidthDefault);
                upPlayer.Id = controller.AddPlayerRedrawing(upPlayer);

                downPlayer = new Player(Player.Position.DOWN, Color.Red, 0, rocketSpeedDefault, width / 2, 0, rocketHeightDefault, rocketWidthDefault);
                downPlayer.Id = controller.AddPlayerRedrawing(downPlayer);
                playerAdded = true;
            }
        }

        // флаг остановки игры
        bool isEnded = false;
        public void GameOver() { isEnded = true; balls.Clear(); }

        public void Update()
        {
            if (isEnded) return;
            if (hits / levelUpCondition > level)
            {
                UpLevel();
            }

            // обновление ракеток
            if(upPlayer.Direction > 0 && upPlayer.FigureXPos + upPlayer.Width < width) upPlayer.FigureXPos += upPlayer.Speed;
            if (upPlayer.Direction < 0 && upPlayer.FigureXPos > 0) upPlayer.FigureXPos -= upPlayer.Speed;
            if(downPlayer.Direction > 0 && downPlayer.FigureXPos + downPlayer.Width < width) downPlayer.FigureXPos += downPlayer.Speed;
            if (downPlayer.Direction < 0 && downPlayer.FigureXPos > 0) downPlayer.FigureXPos -= downPlayer.Speed;

            // обработка завершения игры
            if (points[0] >= winPoints || points[1] >= winPoints)
            {
                if (points[0] == points[1]) controller.GameOver(0);
                else if (points[0] > points[1]) controller.GameOver(1);
                else if (points[0] < points[1]) controller.GameOver(2);
            }

            // обновление шаров
            List<Task> allBallsUpdater = new List<Task>();
            
            foreach (Ball ball in balls)
            {
                Task task = new Task(new Action<object>(BallUpdate), ball);
                allBallsUpdater.Add(task);
                task.Start();
            }

            Task allDone = Task.WhenAll(allBallsUpdater);
            allDone.Wait();
            foreach(Task t in allBallsUpdater)
            {
                t.Dispose();
            }
            allDone.Dispose();

            // обновление счёта игроков
            controller.DownPointsSet(points[1]);
            controller.UpPointsSet(points[0]);
        }

        public void BallUpdate(Ball ball)
        {
            double eps = 0.0001;
            double nextX = ball.XPos + ball.Cos * Ball.speed;
            double nextY = ball.YPos + ball.Sin * Ball.speed;
            // случай, когда отбивает верхний игрок
            if (nextX + ball.Diametr + eps >= upPlayer.FigureXPos && nextX - eps <= upPlayer.FigureXPos + upPlayer.Width &&
                 nextY - eps <= upPlayer.Height)
            {
                ball.XPos = (ball.XPos + (ball.YPos - upPlayer.Height) * ball.Cos / ball.Sin);
                ball.YPos = upPlayer.Height;
                ball.Sin = -ball.Sin;
                ball.Angle = Math.Atan2(ball.Sin, ball.Cos);
                hits++;
            }
            // случай, когда шар ударяется о верхнюю сторону
            else if (nextY - eps <= 0)
            {
                hits++;
                double tryY = (ball.XPos + ball.YPos / ball.Sin * ball.Cos);
                // успешно отбился от верхней стенки
                if (tryY - eps <= width && tryY + eps >= 0)
                {
                    ball.YPos = 0;
                    ball.XPos = tryY;
                    ball.Sin = -ball.Sin;
                    ball.Angle = Math.Atan2(ball.Sin, ball.Cos);
                    lock (downPlayer) { DownPointInc(); };
                }
                // угловые случаи
                else
                {
                    // отбился от правой стенки
                    if (tryY + eps >= width)
                    {
                        ball.YPos = (ball.YPos + (width - ball.XPos) * ball.Sin / ball.Cos);
                        ball.XPos = width;
                        ball.Cos = -ball.Cos;
                        ball.Angle = Math.Atan2(ball.Sin, ball.Cos);
                    }
                    // отбился от левой стенки
                    else if (tryY - eps <= 0)
                    {
                        ball.YPos = (ball.YPos - ball.XPos * ball.Sin / ball.Cos);
                        ball.XPos = 0;
                        ball.Cos = -ball.Cos;
                        ball.Angle = Math.Atan2(ball.Sin, ball.Cos);
                    }
                }
            }
            // случай, когда отбивает нижний игрок
            else if (nextX + ball.Diametr + eps >= downPlayer.FigureXPos && nextX - eps <= downPlayer.FigureXPos + downPlayer.Width &&
                nextY + 0.001 >= height - downPlayer.Height - ball.Diametr)
            {
                hits++;
                ball.XPos = (ball.XPos - (ball.YPos - height + downPlayer.Height + ball.Diametr) * ball.Cos / ball.Sin);
                ball.YPos = height - downPlayer.Height - ball.Diametr;
                ball.Sin = -ball.Sin;
                ball.Angle = Math.Atan2(ball.Sin, ball.Cos);
            }
            // случай, когда шар ударяется о нижнюю стенку
            else if (nextY + ball.Diametr + eps >= height)
            {
                hits++;
                double tryY = (ball.XPos + (height - ball.YPos - ball.Diametr) / ball.Sin * ball.Cos);
                // успешно отбился от нижней стенки
                if (tryY + ball.Diametr - eps <= width && tryY + eps >= 0)
                {
                    ball.YPos = height - ball.Diametr;
                    ball.XPos = tryY;
                    ball.Sin = -ball.Sin;
                    ball.Angle = Math.Atan2(ball.Sin, ball.Cos);
                    lock (upPlayer) { UpPointInc(); };
                }
                // угловые случаи
                else
                {
                    // отбился от правой стенки
                    if (tryY + ball.Diametr + eps >= width)
                    {
                        ball.YPos = (ball.YPos + (width - ball.XPos - ball.Diametr) * ball.Sin / ball.Cos);
                        ball.XPos = width - ball.Diametr;
                        ball.Cos = -ball.Cos;
                        ball.Angle = Math.Atan2(ball.Sin, ball.Cos);
                    }
                    // отбился от левой стенки
                    else if (tryY - eps <= 0)
                    {
                        ball.YPos = (ball.YPos - ball.XPos * ball.Sin / ball.Cos);
                        ball.XPos = 0;
                        ball.Cos = -ball.Cos;
                        ball.Angle = Math.Atan2(ball.Sin, ball.Cos);
                    }
                }
            }
            // случай, когда шар ударяется о боковую правую стенку
            else if (nextX + ball.Diametr + eps >= width)
            {
                hits++;
                ball.YPos = (ball.YPos + (width - ball.XPos - ball.Diametr) * ball.Sin / ball.Cos);
                ball.XPos = width - ball.Diametr;
                ball.Cos = -ball.Cos;
                ball.Angle = Math.Atan2(ball.Sin, ball.Cos);
            }
            // случай, когда шар ударяется о боковую левую стенку
            else if (nextX - eps <= 0)
            {
                hits++;
                ball.YPos = (ball.YPos - ball.XPos * ball.Sin / ball.Cos);
                ball.XPos = 0;
                ball.Cos = -ball.Cos;
                ball.Angle = Math.Atan2(ball.Sin, ball.Cos);
            }
            else
            {
                ball.YPos += (Ball.speed * ball.Sin);
                ball.XPos += (Ball.speed * ball.Cos);
            }
        }

        public void BallUpdate(Object obj)
        {
            if(obj is Ball)
            {
                BallUpdate((Ball)obj);
            }
        }

        public void UpPointInc()
        {
            lock (upPlayer)
            {
                points[0]++;   
            }
        }

        public void DownPointInc()
        { 
            lock(downPlayer) 
            {
                points[1]++;
            }
        }

        public void TrigPlayerToRight(Player player)
        {
            player.Direction = 1;
        }
        public void TrigUPPlayerToRight()
        {
            TrigPlayerToRight(upPlayer);
        }
        public void TrigDOWNPlayerToRight()
        {
            TrigPlayerToRight(downPlayer);
        }

        public void StopPlayer(Player player)
        {
            player.Direction = 0;
        }
        public void StopUPPlayer()
        {
            StopPlayer(upPlayer);
        }
        public void StopDOWNPlayer()
        {
            StopPlayer(downPlayer);
        }

        public void TrigPlayerToLeft(Player player)
        {
            player.Direction = -1;
        }
        public void TrigUPPlayerToLeft()
        {
            TrigPlayerToLeft(upPlayer);
        }
        public void TrigDOWNPlayerToLeft()
        {
            TrigPlayerToLeft(downPlayer);
        }
    }

    public class Ball
    {
        int id; // id в списке прорисовки!
        double xPos; // расположение левого верхнего угла квадрата отрисовки
        double yPos;
        int diametrSize; // размер радиуса (ширина=высота)
        Color color; // цвет шарика
        double cos; // косинус угла вектора перемещения (cos)
        double sin; // синус угла перемещения (sin)
        double angle; // угол вектора
        public static int speed; // сколько пикселей за тик проходит шар
        AbstractFigure associateFigure;

        public double XPos
        {
            get { return xPos; }
            set {
                xPos = value;
                if(Associate != null) { Associate.X = (int)this.XPos; }
            }
        }
        public double YPos
        {
            get { return yPos; }
            set { yPos = value;
                if (Associate != null)
                    Associate.Y = (int)this.YPos;
            }
        }
        public int Diametr
        {
            get { return diametrSize; }
            set
            {
                diametrSize = value;
                if (Associate != null)
                {
                    Associate.Width = Diametr;
                    Associate.Height = Diametr;
                }
            }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public Color Color
        {
            get { return color; }
            set { color = value;
                if (Associate != null)
                    Associate.Color = Color;
            }
        }
        public double Cos
        {
            get { return cos; }
            set { cos = value; }
        }
        public double Sin
        {
            get { return sin; }
            set { sin = value; }
        }
        public double Angle
        {
            get { return angle; }
            set { angle = value; }
        }
        public AbstractFigure Associate
        {
            get { return associateFigure; }
            set { associateFigure = value; }
        }

        public Ball(int xPos, int yPos, int diametr, Color color, double angle)
        {
            XPos = xPos;
            YPos = yPos;
            Diametr = diametr;
            Color = color;
            Angle = angle;
            Cos = Math.Cos(angle);
            Sin = Math.Sin(angle);
        }
    }

    public class Player
    {
        Color color; // цвет игрока
        Position position; // позиция игрока (верхний/нижний)
        int points; // число очков
        int speed; // сколько пикселей проходит ракетка за тик
        double figurePos; // позиция на горизонтали ракетки (левая сторона)
        int direction; // направление движения ракетки (-1 - left, 0 - stop, 1 - right)
        int height; // высота ракетки
        int width; // ширина ракетки
        int id; // id в списке прорисовки!
        AbstractFigure associateFigure; // ассоциированная фигура

        public enum Position
        {
            UP = 1,
            DOWN = 2
        } // позиция (UP - 1, DOWN - 2)
        public int Points
        {
            get { return points; }
            set { points = value; }
        }
        public Color Color
        {
            get { return color; }
            set { color = value;
                if (Associate != null) Associate.Color = Color;
            }
        }
        public Position Pos
        {
            get { return position; }
            set
            {
                position = value;
            }
        }
        public double FigureXPos
        {
            get { return figurePos; }
            set { figurePos = value;
                if (Associate != null) Associate.X = (int) FigureXPos; }
        }
        public int GetFigureYPos(Model model)
        {
            if (Pos == Position.UP) return 0;
            else return model.Height - this.height;
        }
        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }
        public int Height
        {
            get { return height; }
            set { height = value; if (Associate != null) Associate.Height = Height; }
        }
        public int Width
        {
            get { return width; }
            set { width = value; if (Associate != null) Associate.Width = Width; }
        }
        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public AbstractFigure Associate
        {
            get { return associateFigure; }
            set { associateFigure = value; }
        }

        public Player() { }
        public Player(Position position, Color color, int points, int speed, int figureXPos, int direction, int height, int width)
        {
            Pos = position;
            Color = color;
            Points = points;
            Speed = speed;
            FigureXPos = figureXPos;
            Direction = direction;
            Height = height;
            Width = width;
        }

        public void IncPoints() { Points += 1; }
    }

    public class SimpleBot
    {
        Player me;
        List<Ball> balls;
        Model model;
        int ticks = 0;
        int limit = 25;
        int number = 0;
        public static int level = 1;

        public SimpleBot(Model model)
        {
            this.model = model;
            me = model.GetPlayer();
            balls = model.GetBalls();
        }

        public void Update()
        {
            ticks++;
            if(ticks == limit)
            {
                ticks = 0;
                number = new Random().Next(0, balls.Count - 1);
                limit = new Random().Next(10, 50);
            }

            if (ticks % (8-level) == 0)
            {
                Ball ball = balls.ElementAt(number);
                if (me.FigureXPos > ball.XPos) model.GoToLeft();
                else if (me.FigureXPos + me.Width < ball.XPos) model.GoToRight();
                else model.StopUPPlayer();
            }
        }
    }
}
