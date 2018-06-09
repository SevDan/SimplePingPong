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
    public class BallFigure : AbstractFigure
    {
        public BallFigure(int height, int width, Color color) : base()
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
            graphics.FillEllipse(new SolidBrush(Color), 0, 0, Width-2, Height-2);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            var rectangle = ClientRectangle;
            rectangle.Inflate(-2, -2);
            var path = new GraphicsPath();
            path.AddEllipse(rectangle);
            Region = new Region(path);
        }
    }
}
