using System.Threading;
using System.Windows.Forms;

namespace M6_Lab
{
    public class Program
    {
        private static GraphForm _instance;
        public static GraphForm MainGraph
        {
            get => _instance;
        }

        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            _instance = new GraphForm();
            new Thread(ConsoleMain).Start();
            Application.Run(_instance);
        }

        public static void ConsoleMain()
        {
            Graph_Manager manager = new Graph_Manager();
            MakeGraphs(manager);
            GraphCLI cli = new GraphCLI(manager);
            cli.RunLoop();
        }

        private static void MakeGraphs(Graph_Manager manager)
        {
            Graph triGraph = manager.CreateGraph();
            var a = triGraph.AddVertex(300, 72);
            var b = triGraph.AddVertex(100, 200);
            var c = triGraph.AddVertex(300, 300);
            triGraph.AddEdge(a, b);
            triGraph.AddEdge(c, a);
            triGraph.AddEdge(b, c);

            Graph hourGraph = manager.CreateGraph();
            var d = hourGraph.AddVertex(100, 100);
            var e = hourGraph.AddVertex(400, 60);
            var f = hourGraph.AddVertex(300, 300);
            var g = hourGraph.AddVertex(70, 276);
            hourGraph.AddEdge(d, f);
            hourGraph.AddEdge(f, e);
            hourGraph.AddEdge(e, g);
            hourGraph.AddEdge(g, d);
        }
    }
}
