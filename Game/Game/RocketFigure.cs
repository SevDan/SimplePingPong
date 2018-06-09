using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Game
{
    public class RocketFigure : AbstractFigure
    {
        public RocketFigure(int width, int height, Color color) : base()
        {
            Height = height;
            Width = width;
            Color = color;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var graphics = e.Graphics;
            this.Location = new Point(X, Y);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.FillRectangle(new SolidBrush(Color), new Rectangle(0, 0, Width, Height));
        }
    }
}
