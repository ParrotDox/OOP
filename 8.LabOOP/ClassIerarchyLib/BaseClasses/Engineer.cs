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
        private string _department;
        public string Department
        {
            get
            {
                return _department;
            }
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Department field is empty!");
                }
                _department = value;
            }
        }

        public Engineer() : base()
        {
            _department = "Undefined";
        }
        public Engineer(string name, int age, string residence, int id, int experience, int salary, string department ) : base(name, age, residence, id, experience, salary)
        {
            _department = department;
        }
        public Engineer(string name, int age, string residence, int id, int experience, int salary, string department, Link link_sample) : base(name, age, residence, id, experience, salary, link_sample)
        {
            _department = department;
        }
        public Engineer(Engineer copy_sample) : base(copy_sample)
        {
            this._department = copy_sample._department;
        }
        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Department: {Department}");
        }
        public override string GetInfo()
        {
            string msg = base.GetInfo();
            msg += $"Department: {Department}\n";
            return msg;
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
        public override object Clone()
        {
            return new Engineer(this.Name, this.Age, this.Residence, this.Id, this.Experience, this.Salary, this.Department, new Link(link.data, link.notes));
        }
    }
}
