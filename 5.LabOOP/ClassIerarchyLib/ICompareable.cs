using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIerarchyLib
{
    public interface IComparable
    {
        //Реализация метода прописана в классах-наследниках.
        int CompareTo(object obj);
    }
}
