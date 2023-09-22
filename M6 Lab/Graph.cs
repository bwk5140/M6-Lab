using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace M6_Lab
{
    public class Graph : IGraph, IDrawable
    {
        private static int idCounter = 0;
        protected int VertexCount { get; set; } = 0;
        protected int EdgeCount { get; set; }  = 0;

        public int Id { get; private set; }
        public IList<Vertex> Vertices { get; private set; } = new List<Vertex>();
        public IList<Edge> Edges { get; private set; } = new List<Edge>();

        public Graph(int ID)
        {
            Id = ID;
        }

        private Vertex CreateVertex(int x, int y) {
            return new Vertex()
            { 
                Id = VertexCount++,
                X = x,
                Y = y,
            };
        }

        private Edge CreateEdge(Vertex from, Vertex to)
        {
            return new Edge()
            {
                Id = EdgeCount++,
                From = from,
                To = to,
            };
        }

        public Vertex AddVertex(int x, int y)
        {
            var vertex = CreateVertex(x, y);
            Vertices.Add(vertex);
            return vertex;
        }

        public Edge AddEdge(Vertex start, Vertex end)
        { 
            var edge = CreateEdge(start, end);
            Edges.Add(edge);
            return edge;
        }

        public void Print()
        {
            Program.MainGraph.Invoke(new Action(() => Program.MainGraph.Print(this)));
        }

        public Vertex FindVertex(int id)
        {
            return Vertices.Where(e => e.Id == id).FirstOrDefault();
        }

        public Edge FindEdge(int id)
        {
            return Edges.Where(e => e.Id == id).FirstOrDefault();
        }

        public void Revise(int ID)
        {
            int inputID;
            Console.Write("Editing vertex or edge? (v/e): ");
            string input = Console.ReadLine();
            if (input == "v" && StdinInt("Enter vertex ID to edit: ", out inputID))
            {
                ReviseVertex(inputID);
            }
            else if (input == "e" && StdinInt("Enter edge ID to edit: ", out inputID))
            {
                ReviseEdge(inputID);
            }
        }

        public void ReviseVertex(int ID)
        {
            Vertex returnVal = FindVertex(ID);
            if (returnVal == null)
            {
                Console.WriteLine("No such vertex with ID {0}", ID);
                return;
            }

            int newID, newX, newY;
            if (StdinInt("Enter a new ID: ", out newID))
            {
                returnVal.Id = newID;
            }
            if (StdinInt("Enter a new X: ", out newX))
            {
                returnVal.X = newX;
            }
            if (StdinInt("Enter a new Y: ", out newY))
            {
                returnVal.Y = newY;
            }
        }

        public void ReviseEdge(int ID)
        {
            Edge edge = FindEdge(ID);
            if (edge == null)
            {
                Console.WriteLine("No such edge with ID {0}", ID);
                return;
            }

            int newID, newFrom, newTo;
            if (StdinInt("Enter a new ID: ", out newID))
            {
                edge.Id = newID;
            }
            if (StdinInt("Enter new start vertex ID: ", out newFrom))
            {
                var start = FindVertex(newFrom);
                if (start == null)
                {
                    Console.WriteLine("No such vertex with ID {0}", newFrom);
                }
                else
                {
                    edge.From = start;
                }
            }

            if (StdinInt("Enter new ending vertex ID: ", out newTo))
            {
                var end = FindVertex(newTo);
                if (end == null)
                {
                    Console.WriteLine("No such vertex with ID {0}", newFrom);
                }
                else
                {
                    edge.To = end;
                }
            }
        }

        public Graph CloneWithId(int id)
        {
            var g = new Graph(id)
            {
                VertexCount = VertexCount,
                EdgeCount = EdgeCount,
                Vertices = Vertices.Select(x => (Vertex)x.Clone()).ToList()
            };

            foreach (Edge e in Edges)
            {
                g.AddEdge((Vertex)e.From.Clone(), (Vertex)e.To.Clone());
            }

            return g;
        }

        private bool StdinInt(string label, out int i)
        {
            i = -1;
            try
            {
                while (true)
                {
                    Console.Write(label);
                    string rawInput = Console.ReadLine();
                    if (string.IsNullOrEmpty(rawInput))
                    {
                        break;
                    }
                    else if (int.TryParse(rawInput, out i))
                    {
                        return true;
                    }
                    Console.WriteLine("Invalid input.  Expected int");
                }
                return false;
            }
            catch (IOException)
            {
                return false;
            }
        }

        public void Draw(Graphics g)
        {
            foreach (Edge e in Edges)
            { 
                e.Draw(g);
            }
            foreach (Vertex v in Vertices)
            {
                v.Draw(g);
            }
        }
    }
}
