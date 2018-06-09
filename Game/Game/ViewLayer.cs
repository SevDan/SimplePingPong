using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Game
{
    public partial class ViewLayer : Form
    {
        Controller controller; // Injection

        public Controller Controller
        {
            get { return controller; }
            set { controller = value; }
        }
        List<AbstractFigure> toReprint = new List<AbstractFigure>();

        public ViewLayer()
        {
            InitializeComponent();
        }

        // получение размера для дальнейшей инициализации
        public int WidthField
        {
            get { return mainPanel.Width; }
        }
        public int HeightField
        {
            get { return mainPanel.Height; }
        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {
            KeyDown += ViewLayer_KeyDownSec;
            KeyUp += ViewLayer_KeyUpSecond;
        }

        public void AddElement(AbstractFigure fig)
        {
            mainPanel.Controls.Add(fig);
            toReprint.Add(fig);
        }

        public void UpdateElements()
        {
            mainPanel.Invalidate();
            foreach(AbstractFigure fig in toReprint)
            {
                fig.Invalidate();
            }
        }

        private void ViewLayer_KeyUp(object sender, KeyEventArgs e) // Подъём клавиши верхним игроком 
        {
            lock (this)
            {
                if (e.KeyCode == Keys.D)
                {
                    if (controller.GameType == Controller.EGameType.PVP && controller.UpPlayerSetDirection)
                    {
                        controller.UpPlayerSetDirection = false;
                    }
                }
                if (e.KeyCode == Keys.A)
                {
                    if (controller.GameType == Controller.EGameType.PVP && controller.UpPlayerSetDirection)
                    {
                        controller.UpPlayerSetDirection = false;
                    }
                }
            }
        }

        private void ViewLayer_KeyUpSecond(object sender, KeyEventArgs e) // Подъём клавиши нижним игроком
        {
            lock (this)
            {
                if (e.KeyCode == Keys.Right && controller.DownPlayerSetDirection)
                {
                    controller.DownPlayerSetDirection = false;
                }
                if (e.KeyCode == Keys.Left && controller.DownPlayerSetDirection)
                {
                    controller.DownPlayerSetDirection = false;
                }
            }
        }

        private void ViewLayer_KeyDown(object sender, KeyEventArgs e) // Нажатие на клавишу верхним игроком
        {
            lock (this)
            {
                if (e.KeyCode == Keys.D)
                {
                    if (controller.GameType == Controller.EGameType.PVP && !controller.UpPlayerSetDirection)
                    {
                        controller.TriggerUpperPlayerToRight();
                        controller.UpPlayerSetDirection = true;
                    }
                }
                if (e.KeyCode == Keys.A)
                {
                    if (controller.GameType == Controller.EGameType.PVP && !controller.UpPlayerSetDirection)
                    {
                        controller.TriggerUpperPlayerToLeft();
                        controller.UpPlayerSetDirection = true;
                    }
                }
            }
        }

        private void ViewLayer_KeyDownSec(object sender, KeyEventArgs e) // Нажатие на клившу нижним игроком
        {
            lock (this)
            {
                if (e.KeyCode == Keys.Right && !controller.DownPlayerSetDirection)
                {
                    controller.TriggerDownPlayerToRight();
                    controller.DownPlayerSetDirection = true;
                }
                if (e.KeyCode == Keys.Left && !controller.DownPlayerSetDirection)
                {
                    controller.TriggerDownPlayerToLeft();
                    controller.DownPlayerSetDirection = true;
                }
            }
        }

        private void levelCounter_Click(object sender, EventArgs e)
        {

        }

        public void UpPointsSet(int points)
        {
            this.upPlayerCounter.Text = points.ToString();
        }

        public void DownPointsSet(int points)
        {
            this.downPlayerCounter.Text = points.ToString();
        }

        public void GameOver(int winner)
        {
            if (winner == 1) MessageBox.Show("Поздравляем! Верхний игрок победил!");
            else if (winner == 2) MessageBox.Show("Поздравляем! Нижний игрок победил!");
            else MessageBox.Show("Ничья!");
        }

        public void UpLevel(int level)
        {
            levelCounter.Text = level.ToString();
        }

        private void ViewLayer_Load(object sender, EventArgs e)
        {
        }
    }
}
