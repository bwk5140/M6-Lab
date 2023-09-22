using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Xml.Linq;

namespace M6_Lab
{
    public class Graph : IGraph
    {
        int ID;
        List<Vertex> vertices;
        List<Edge> edges;
        public GraphForm graphForm;

        public Graph()
        {
            var ID = DateTime.Now;
            var str = ID.ToString();
            string[] strArr = str.Split('/');
            string[] newArr = strArr[2].Split(':');
            string[] arr = newArr[0].Split(' ');
            string[] lastArr = newArr[2].Split(' ');
            str = arr[0] + arr[1] + newArr[1] + lastArr[0];
            this.ID = int.Parse(str);

            if (graphForm == null)
                graphForm = new GraphForm();
        }

        public Graph(int ID)
        {
            this.ID = ID;
        }

        public int getID()
        {
            return ID;
        }

        public void SetID()
        {
            ID++;
        }

        public void SetVertices(List<Vertex> list)
        {
            vertices = list;
        }

        public void SetEdges(List<Edge> list)
        {
            edges = list;
        }

        public void add(Vertex vertex)
        {
            vertices.Add(vertex);
        }

        public void add(Edge edge)
        {
            edges.Add(edge);
        }

        public void remove(Vertex vertex)
        {
            vertices.Remove(vertex);
        }

        public void remove(Edge edge)
        {
            edges.Remove(edge);
        }

        public void Print()
        {
            foreach (Vertex vertex in vertices) { vertex.draw(); }
            foreach (Edge edge in edges) { edge.draw(); }
        }

        public void Revise(int ID)
        {
            Console.WriteLine("Editing vertex or edge? (v/e)");
            string input = Console.ReadLine();
            if (input == "v")
            {
                Console.WriteLine("Enter vertex ID to edit: ");
                int inputID = Console.Read();
                ReviseVertex(inputID);
                
            }
            else if (input == "e")
            {
                Console.WriteLine("Enter edge ID to edit: ");
                int inputID = Console.Read();
                ReviseEdge(inputID);
                
            }
        }

        public Vertex ReviseVertex(int ID)
        {
            Vertex returnVal = null;
            foreach (Vertex var in vertices)
            {
                if (var.GetID() == ID)
                {
                    Console.WriteLine("Enter new ID, x-coordinate, and y-coordinate values: ");
                    var one = Console.Read();
                    var two = Console.Read();
                    var three = Console.Read();
                    var.Edit(one, two, three);
                }
                else
                    throw new System.NotImplementedException();

                returnVal = var;
            }
            return returnVal;
        }

        public void ReviseEdge(int ID)
        {
            foreach (Edge var in edges)
            {
                if (var.GetID() == ID)
                {
                    Console.WriteLine("Enter new ID, from_vertex_ID, and to_vertex_ID values: ");
                    var one = Console.Read();
                    var two = Console.Read();
                    var three = Console.Read();
                    var.Edit(one, ReviseVertex(two), ReviseVertex(three));                   
                }
                else
                    throw new System.NotImplementedException();
            }
        }

        public object Clone()
        {
            Graph newGraph = (Graph)this.MemberwiseClone();
            newGraph.SetID();
            List<Vertex> list = new List<Vertex>();

            foreach (Vertex var in vertices)
            {
                list.Add(var);
            }
            newGraph.SetVertices(list);

            List<Edge> list_ = new List<Edge>();
            foreach (Edge var in edges)
            {
                list_.Add(var);
            }
            newGraph.SetEdges(list_);

            return newGraph;
        }
    }
}
