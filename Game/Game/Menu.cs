using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        public static int defaultTimer = 10;

        private void pvpChose_Click(object sender, EventArgs e)
        {
            ViewLayer view = new ViewLayer();
            Controller controller = new Controller(defaultTimer /* timer interval */);

            view.Controller = controller;
            controller.View = view;

            Model model = new Model(controller);
            controller.Model = model;
            controller.GameType = Controller.EGameType.PVP;

            view.Show();
            view.Invalidate();
            controller.Start();
        }

        private void pveChose_Click(object sender, EventArgs e)
        {
            ViewLayer view = new ViewLayer();
            Controller controller = new Controller(defaultTimer /* timer interval */);

            view.Controller = controller;
            controller.View = view;

            Model model = new Model(controller);
            controller.Model = model;
            controller.GameType = Controller.EGameType.PVE;

            view.Show();
            view.Invalidate();
            controller.Start();
        }

        private void settings_Click(object sender, EventArgs e)
        {
            new Settings().ShowDialog();
        }
    }
}
