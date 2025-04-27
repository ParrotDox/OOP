using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIerarchyLib
{
    [Serializable]
    public class Employee : Person
    {
        public int Experience { get; set; }
        public int Salary { get; set; }
        public Employee() : base()
        {
            Experience = 0;
            Salary = 0;
        }
        public override void Init()
        {
            bool idFlag, experienceFlag, salaryFlag;
            idFlag = false;
            experienceFlag = false;
            salaryFlag = false;

            base.Init();
        inputMark:
            try
            {
                if (!idFlag)
                {
                    Console.WriteLine("Input id:");
                    int parsedInt;
                    idFlag = Int32.TryParse(Console.ReadLine(), out parsedInt);
                    idFlag = true;
                }
                if (!experienceFlag)
                {
                    Console.WriteLine("Input experience:");
                    int parsedInt;
                    experienceFlag = Int32.TryParse(Console.ReadLine(), out parsedInt);
                    Experience = parsedInt;
                    experienceFlag = true;
                }
                if (!salaryFlag)
                {
                    Console.WriteLine("Input salary:");
                    int parsedInt;
                    salaryFlag = Int32.TryParse(Console.ReadLine(), out parsedInt);
                    Salary = parsedInt;
                    salaryFlag = true;
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                goto inputMark;
            }
        }
        public override void RandomInit()
        {
            base.RandomInit();
            Random rnd = new Random();
            Experience = rnd.Next(2, 85);
            Salary = rnd.Next(20000, 200001);
        }
        public override bool Equals(object obj)
        {
            if (obj is not Employee)
            {
                return false;
            }
            Employee sample = (Employee)obj;
            if (base.Equals(obj) &&
                this.Name == sample.Name &&
                this.Age == sample.Age &&
                this.Residence == sample.Residence)
            {
                return true;
            }
            else { return false; }
        }
        public override object Clone()
        {
            Employee copy = new Employee();
            copy.Name = this.Name;
            copy.Age = this.Age;
            copy.Residence = this.Residence;
            copy.link = new Link(link.Notes, link.Data);
            copy.Experience = this.Experience;
            copy.Salary = this.Salary;
            //key поле является уникальным, но в контексте 11 лаб.
            //требуется найти объект по значению, а не по ссылке
            copy.Key = this.Key;
            return copy;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Experience, Salary);
        }
        public override string ToString()
        {
            //8 fields
            return base.ToString()+$",{Experience},{Salary}";
        }
    }
}
