using System.Drawing;
using System.Reflection;

namespace M6_Lab
{
    public class Vertex
    {
        int vertex_ID;
        int x_coordinate;
        int y_coordinate;

        public Point draw()
        {
            Point vertex_point = new Point();
            vertex_point.X = x_coordinate;
            vertex_point.Y = y_coordinate;

            return vertex_point;
        }

        public int GetID()
        {
            return vertex_ID;
        }

        public void Edit(int ID, int x, int y)
        {
            vertex_ID = ID;
            x_coordinate = x;
            y_coordinate = y;
        }

        public Vertex GetVertex()
        {
            return this;
        }
    }
}
