using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace M6_Lab
{
    public partial class GraphForm : Form
    {
        public delegate void GraphFormInvoke();
        private IDrawable graph = null;
        public GraphForm()
        {
            InitializeComponent();
        }

        private void GraphForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.Clear(Color.White);
            if (graph != null)
            {
                graph.Draw(e.Graphics);
            }
        }

        private void GraphForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        public void Print(IDrawable g)
        {
            graph = g;
            Show();
            Refresh();
            Focus();
        }
    }
}
