using System;
using System.Collections.Generic;
using System.Linq;

namespace M6_Lab
{
    public class Graph_Manager
    {
        private IList<Graph> graphs = new List<Graph>();
        private int graphCount = 0;

        public IEnumerable<int> GraphIds 
        {
            get => graphs.Select(x => x.Id);
        }

        public Graph CreateGraph()
        {
            return new Graph(graphCount++);
        }

        public Graph Clone(int ID)
        {
            return GetGraph(ID)?.CloneWithId(graphCount++);
        }

        public Graph GetGraph(int ID)
        {
            return graphs.Where(e => e.Id == ID).FirstOrDefault();
        }
    }
}
