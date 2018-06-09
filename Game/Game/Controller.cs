using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{ 
    public class Controller
    {
        public enum EGameType
        {
            PVP = 1,
            PVE = 2,
            COOP = 3
        }

        ViewLayer view; // Injection
        Model model; // Injection
        bool modelInit = false;
        Timer timer;
        // интервал, с которым таймер совершает тики
        int timerInterval;
        public int TimerInterval
        {
            get { return timerInterval; }
            set { timerInterval = value; timer.Interval = timerInterval; }
        }

        // Бот, если активно PVE
        SimpleBot bot;

        // Флаги, установили ли игроки следующее направление
        bool upPlayerSetDirection;
        bool downPlayerSetDirection;
        public bool UpPlayerSetDirection { get { return upPlayerSetDirection; } set { upPlayerSetDirection = value; } }
        public bool DownPlayerSetDirection { get { return downPlayerSetDirection; } set { downPlayerSetDirection = value; } }

        List<AbstractFigure> redrawingList;

        EGameType gameType; // тип игры
        public EGameType GameType
        {
            get { return gameType; }
            set { gameType = value; }
        }

        // управление отрисовкой фигур (фигура без логики)
        public void AddToRedrawing(AbstractFigure fig)
        {
            redrawingList.Add(fig);
            view.AddElement(fig);
        }
        /* DEPRECETED */ public void RemoveFromRedrawing(AbstractFigure fig) // DEPRECATED
        {
            redrawingList.Remove(fig);
        }
        public int AddPlayerRedrawing(Player player)
        {
            RocketFigure fig = new RocketFigure(player.Width, player.Height, player.Color);
            fig.X = (int)player.FigureXPos;
            fig.Y = player.GetFigureYPos(model);
            AddToRedrawing(fig);
            player.Associate = fig;
            return redrawingList.Count;
        }
        public int AddBallRedrawing(Ball ball)
        {
            BallFigure fig = new BallFigure(ball.Diametr, ball.Diametr, ball.Color);
            fig.X = (int)ball.XPos;
            fig.Y = (int)ball.YPos;
            AddToRedrawing(fig);
            ball.Associate = fig;
            return redrawingList.Count;
        }

        public Controller(int timerInterval)
        {
            timer = new Timer();
            TimerInterval = timerInterval;
            redrawingList = new List<AbstractFigure>();
        }

        public void Start()
        {
            timer.Interval = TimerInterval;
            timer.Tick += new EventHandler(OnTimerTick);
            timer.Start();
        } // запуск игры

        private void OnTimerTick(object sender, System.EventArgs e)
        {
            OnUpdate();
        } // метод запуска тика таймера
        public void OnUpdate()
        {
            if (!modelInit) {
                model.Initializate(view.WidthField, view.HeightField); modelInit = true;
                if (GameType == EGameType.PVE)
                {
                    bot = new SimpleBot(model);
                }
            }

            model.UpdateField(view.WidthField);
            model.Update();
            if (GameType == EGameType.PVE) bot.Update();

            view.UpdateElements();

            if (!upPlayerSetDirection && GameType != EGameType.PVE) StopUpperPlayer();
            if (!downPlayerSetDirection) StopDownPlayer();
        } // обёртка без арг. для метода OnTimerTick(object, EventArgs)
        public void SetTimerInterval(int interval)
        {
            timer.Interval = interval;
        }

        public void TriggerUpperPlayerToLeft()
        {
            model.TrigUPPlayerToLeft();
        }
        public void TriggerUpperPlayerToRight()
        { model.TrigUPPlayerToRight(); }
        public void TriggerDownPlayerToLeft()
        { model.TrigDOWNPlayerToLeft(); }
        public void TriggerDownPlayerToRight()
        { model.TrigDOWNPlayerToRight(); }
        public void StopUpperPlayer() { model.StopUPPlayer(); }
        public void StopDownPlayer() { model.StopDOWNPlayer(); }
        
        public void UpLevel(int level)
        {
            view.UpLevel(level);
        }

        public void GameOver(int winner)
        {
            timer.Stop();
            view.GameOver(winner);
            model.GameOver();
        }

        public void UpPointsSet(int points)
        {
            view.UpPointsSet(points);
        }

        public void DownPointsSet(int points)
        {
            view.DownPointsSet(points);
        }

        public ViewLayer View
        {
            get { return view; }
            set { view = value; }
        }
        public Model Model
        {
            get { return model; }
            set { model = value; }
        }
    }
}
