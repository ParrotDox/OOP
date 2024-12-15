using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIerarchyLib
{
    public class Admin : Employee
    {
        public static List<string> rndHeadOffices = new List<string> { "Central_Perm_Office", "Central_night_street_office", "Central_space_office" };
        protected string __headOffice__;
        public string __HeadOffice__
        {
            get
            {
                return __headOffice__;
            }
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Head office field is empty!");
                }
                __headOffice__ = value;
            }
        }

        public Admin() : base()
        {
            __HeadOffice__ = "Undefined";
        }
        public Admin(string name, int age, string residence, int id, int experience, int salary, string headOffice) : base(name, age, residence, id, experience, salary)
        {
            __HeadOffice__ = headOffice;
        }
        public Admin(Admin copySample) : base(copySample)
        {
            this.__HeadOffice__ = copySample.__HeadOffice__;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Head_Office: {__HeadOffice__}");
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
                    __HeadOffice__ = inputString;
                    rndHeadOffices.Add(inputString);
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
            __HeadOffice__ = rndHeadOffices[rnd.Next(0, rndHeadOffices.Count)];
        }
        public override bool Equals(object obj)
        {
            if (obj is not Admin)
            {
                return false;
            }
            Admin sample = (Admin)obj;
            if (base.Equals(obj) &&
                this.__HeadOffice__ == sample.__HeadOffice__)
            {
                return true;
            }
            else { return false; }
        }
    }
}
