using System;
using System.Drawing;

namespace M6_Lab
{
    public class Vertex : ICloneable, IDrawable
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Point ToPoint()
        {
            return new Point()
            {
                X = X,
                Y = Y,
            };
        }

        public void Edit(int ID, int x, int y)
        {
            Id = ID;
            X = x;
            Y = y;
        }

        public object Clone()
        {
            return new Vertex()
            {
                Id = Id,
                X = X,
                Y = Y,
            };
        }

        private static readonly float RADIUS = 2.5f;
        public void Draw(Graphics g)
        {
            var center = new PointF(X - RADIUS, Y - RADIUS);
            var radius = new SizeF(RADIUS * 2, RADIUS * 2);
            RectangleF rect = new RectangleF(center, radius);
            using (Pen p = new Pen(Brushes.Black, 2))
            {
                g.DrawEllipse(p, rect);
            }
            g.FillEllipse(Brushes.White, rect);
        }
    }
}
