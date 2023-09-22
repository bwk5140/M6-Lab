using System.Drawing;
using System.Windows.Forms;

namespace M6_Lab
{
    public class Edge
    {
        int edge_ID;
        Vertex from_vertex;
        Vertex to_vertex;
        Graph graph;

        public void draw()
        {
            Graphics g;
            GraphForm graphForm_;
            graphForm_ = graph.graphForm;
            g = graphForm_.CreateGraphics();
            PaintEventArgs e = new PaintEventArgs(g, new Rectangle());
            e.Graphics.DrawLine(SystemPens.Highlight, from_vertex.draw(), to_vertex.draw());
        }
        public int GetID()
        {
            return edge_ID;
        }
        public void Edit(int ID, params Vertex[] v)
        {
            edge_ID = ID;
            if (v[0] != null)
                from_vertex = v[0];
            if (v[1] != null)
                to_vertex = v[1];
        }
        public Edge GetEdge()
        {
            return this;
        }
    }
}
