using System;
using System.Drawing;

namespace M6_Lab
{
    public class Edge : IDrawable
    {
        public int Id { get; set; }
        public Vertex From { get; set; }
        public Vertex To { get; set; }

        private double Dx
        {
            get => To.X - From.X;
        }

        private double Dy
        {
            get => To.Y - From.Y;
        }

        private double Magnitude()
        {
            return Math.Sqrt(Dx * Dx + Dy * Dy);
        }

        private static readonly double ANGLE_LEN = -15, ANGLE_ROT = Math.PI / 6;
        public void Draw(Graphics g)
        {
            double mag = Magnitude();
            g.DrawLine(Pens.Black, From.ToPoint(), To.ToPoint());
            for (int i = -1; i < 2; i += 2)
            {
                double normX = (Dx / mag) * ANGLE_LEN,
                    normY = (Dy / mag) * ANGLE_LEN;
                double rotX = normX * Math.Cos(ANGLE_ROT * i) - normY * Math.Sin(ANGLE_ROT * i),
                    rotY = normX * Math.Sin(ANGLE_ROT * i) + normY * Math.Cos(ANGLE_ROT * i);
                g.DrawLine(Pens.Black, To.ToPoint(), new PointF(To.X + (float)rotX, To.Y + (float)rotY));
            }
        }
    }
}
