using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M6_Lab
{
    public interface IGraph : IDrawable, ICloneable
    {
        void Print();
        void Revise(int ID);
    }
}
