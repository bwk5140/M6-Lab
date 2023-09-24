using System;
using System.Collections.Generic;
using System.Linq;

namespace M6_Lab
{
    public class Graph_Manager
    {
        private IList<Graph> graphs = new List<Graph>();

        public IEnumerable<int> GraphIds 
        {
            get => graphs.Select(x => x.Id);
        }

        public Graph CreateGraph()
        {
            var graph = new Graph();
            graphs.Add(graph);
            return graph;
        }

        public Graph Clone(int ID)
        {
            var graph = GetGraph(ID);
            if (graph != null)
            {
                var copy = (Graph)graph.Clone();
                graphs.Add(copy);
                return copy;
            }
            return null;
        }

        public Graph GetGraph(int ID)
        {
            return graphs.Where(e => e.Id == ID).FirstOrDefault();
        }
    }
}
