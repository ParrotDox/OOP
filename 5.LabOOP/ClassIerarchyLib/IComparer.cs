using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIerarchyLib
{
    public interface IComparer
    {
        //Реализация метода прописывается в отдельном классе от классовой-иерархии
        public int Compare(object? x, object? y);
    }
}
