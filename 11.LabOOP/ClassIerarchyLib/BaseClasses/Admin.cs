using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIerarchyLib
{
    [Serializable]
    public class Admin : Employee
    {
        public static List<string> random_head_offices = new List<string> { "Central_Perm_Office", "Central_night_street_office", "Central_space_office" };
        private string _headOffice;
        public string HeadOffice
        {
            get
            {
                return _headOffice;
            }
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Head office field is empty!");
                }
                _headOffice = value;
            }
        }

        public Admin() : base()
        {
            HeadOffice = "Undefined";
        }
        public Admin(string name, int age, string residence, int id, int experience, int salary, string head_office) : base(name, age, residence, id, experience, salary)
        {
            HeadOffice = head_office;
        }
        public Admin(string name, int age, string residence, int id, int experience, int salary, string head_office, Link link_sample) : base(name, age, residence, id, experience, salary, link_sample)
        {
            HeadOffice = head_office;
        }
        public Admin(Admin copy_sample) : base(copy_sample)
        {
            this.HeadOffice = copy_sample.HeadOffice;
        }
        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Head_Office: {HeadOffice}");
        }
        public override void Init()
        {
            bool headOfficeFlag;
            headOfficeFlag = false;

            base.Init();
        inputMark:
            try
            {
                if (!headOfficeFlag)
                {
                    Console.WriteLine("Input head office:");
                    string inputString = "";
                    inputString = Console.ReadLine();
                    HeadOffice = inputString;
                    random_head_offices.Add(inputString);
                    headOfficeFlag = true;
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
            HeadOffice = random_head_offices[rnd.Next(0, random_head_offices.Count)];
        }
        public override bool Equals(object obj)
        {
            if (obj is not Admin)
            {
                return false;
            }
            Admin sample = (Admin)obj;
            if (base.Equals(obj) &&
                this.HeadOffice == sample.HeadOffice)
            {
                return true;
            }
            else { return false; }
        }
        public override object Clone()
        {
            return new Admin(this.Name, this.Age, this.Residence, this.Id, this.Experience, this.Salary, this.HeadOffice, new Link(link.Data, link.Notes));
        }
    }
}
