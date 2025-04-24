using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIerarchyLib
{
    [Serializable]
    public class Engineer : Employee
    {
        public static List<string> rndDepartments = new List<string> { "Department_of_aqua_technologies", "Department_of_food_production", "Department_of_space_production" };
        public string Department { get;set; }
        public Engineer() : base()
        {
            Department = "Undefined";
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
                    Department = inputString;
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
            Random rnd = new Random();
            Department = rndDepartments[rnd.Next(0, rndDepartments.Count)];
        }
        public override bool Equals(object obj)
        {
            if (obj is not Engineer)
            {
                return false;
            }
            Engineer sample = (Engineer)obj;
            if (base.Equals(obj) &&
                this.Department == sample.Department)
            {
                return true;
            }
            else { return false; }
        }
        public override string ToString()
        {
            return base.ToString() + $"{Department},IamEngi";
        }
    }
}
