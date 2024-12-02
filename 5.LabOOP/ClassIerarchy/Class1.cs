using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIerarchyLib
{
    class Person
    {
        protected string __name__
        {
            get
            {
                return __name__;
            }
            set
            {
                __name__ = value;
            }
        }
        protected int __age__;
        protected string __residence__;
    }
    class Employee : Person 
    {
        
    }
    class Engineer : Employee 
    {
        
    }
    class Admin : Employee 
    {
        
    }
}
