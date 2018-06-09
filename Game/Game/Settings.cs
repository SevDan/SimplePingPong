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
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            diametrValue.Value = Model.ballsDiametrDefault;
            botDiffValue.Value = SimpleBot.level;
            heighRocketValue.Value = Model.rocketHeightDefault;
            widthRocketValue.Value = Model.rocketWidthDefault;
            rocketSpeedValue.Value = Model.rocketSpeedDefault;
            ballsSpeedValue.Value = Model.ballsSpeedDefault;
            timerSpeed.Value = Game.Menu.defaultTimer;
            winCondition.Value = Model.winPoints;
            levelUpCondition.Value = Model.levelUpCondition;
        }

        private void defaultValues_Click(object sender, EventArgs e)
        {
            botDiffValue.Value = 1;
            heighRocketValue.Value = 6;
            widthRocketValue.Value = 42;
            rocketSpeedValue.Value = 15;
            ballsSpeedValue.Value = 10;
            diametrValue.Value = 36;
            timerSpeed.Value = 10;
            winCondition.Value = 50;
            levelUpCondition.Value = 20;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Model.ballsDiametrDefault = (int)diametrValue.Value;
            Model.ballsSpeedDefault = (int)ballsSpeedValue.Value;
            Model.rocketSpeedDefault = (int)rocketSpeedValue.Value;
            Model.rocketWidthDefault = (int)widthRocketValue.Value;
            Model.rocketHeightDefault = (int)heighRocketValue.Value;
            Model.levelUpCondition = (int)levelUpCondition.Value;
            Model.winPoints = (int)winCondition.Value;
            Game.Menu.defaultTimer = (int)timerSpeed.Value;
            SimpleBot.level = (int)botDiffValue.Value;
            this.Close();
        }
    }
}
