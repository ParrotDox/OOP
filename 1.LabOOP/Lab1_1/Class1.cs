using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Human
    {
        public int age;
        public string name;
        public string description;
        public int salary;
        private int oklad;
        private int coef;

        public Human() 
        {
            age = 0;
            name = "";
            description = "";
            coef = 0;
        }
        public Human(int a, string n, string d, int c, int ok)
        {
            age = a;
            name = n;
            description = d;
            salary = oklad * c;
            coef = c;
            oklad = ok;
        }

    }
}
