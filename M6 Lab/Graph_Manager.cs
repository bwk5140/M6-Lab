using System;
using System.Collections.Generic;

namespace M6_Lab
{
    public class Graph_Manager
    {
        List<Graph> graphs = new List<Graph>();


        public object Clone(int ID)
        {
            Graph returnVal = null;

            foreach (Graph val in graphs)
            {
                if (val.getID() == ID)
                {
                    returnVal = (Graph)val.Clone();
                    break;
                }
            }
            return returnVal;
        }

        public Graph getGraph(int ID)
        {
            Graph returnVal = null;

            foreach (Graph val in graphs)
            {
                if (val.getID() == ID)
                {
                    returnVal = val;
                    break;
                }
            }
            return returnVal;
        }
    }

}