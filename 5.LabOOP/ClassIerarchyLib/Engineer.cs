using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIerarchyLib
{
    public class Engineer : Employee
    {
        public static List<string> rndDepartments = new List<string> { "Department_of_aqua_technologies", "Department_of_food_production", "Department_of_space_production" };
        Random rnd = new Random();
        protected string __department__;
        public string __Department__
        {
            get
            {
                return __department__;
            }
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Department field is empty!");
                }
                __department__ = value;
            }
        }

        public Engineer() : base()
        {
            __department__ = "Undefined";
        }
        public Engineer(string name, int age, string residence, int id, int experience, int salary, string department) : base(name, age, residence, id, experience, salary)
        {
            __department__ = department;
        }
        public Engineer(Engineer copySample) : base(copySample)
        {
            this.__department__ = copySample.__department__;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Department: {__department__}");
        }
        public override void Init()
        {
            bool departmentFlag;
            departmentFlag = false;

            base.Init();
        inputMark:
            try
            {
                if (!departmentFlag)
                {
                    string inputString = "";
                    Console.WriteLine("Input department:");
                    inputString = Console.ReadLine();
                    __Department__ = inputString;
                    rndDepartments.Add(inputString);
                    departmentFlag = true;
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

            __Department__ = rndDepartments[rnd.Next(0, rndDepartments.Count)];
        }
        public override bool Equals(object obj)
        {
            if (obj is not Engineer)
            {
                return false;
            }
            Engineer sample = (Engineer)obj;
            if (base.Equals(obj) &&
                this.__Department__ == sample.__Department__)
            {
                return true;
            }
            else { return false; }
        }
    }
}
