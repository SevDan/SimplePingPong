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
    public class AbstractFigure : Control
    {
        protected int x, y; // координаты с левого-верхнего угла
        public int X
        {
            get { return x; }
            set
            {
                x = value;
            }
        }
        public int Y
        {
            get { return y; }
            set
            {
                y = value;
               
            }
        }

        public Color Color
        {
            get
            {
                return color;
            }

            set
            {
                this.color = value;
                
            }
        }

        protected Color color = Color.Black;

        public AbstractFigure()
        {
            SetStyle(ControlStyles.ResizeRedraw | 
                ControlStyles.DoubleBuffer | 
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.FixedWidth |
                ControlStyles.FixedHeight |
                ControlStyles.Opaque |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint,
                true);

        }
    }
}
