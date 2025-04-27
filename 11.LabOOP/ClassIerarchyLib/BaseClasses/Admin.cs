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
        public string HeadOffice { get; set; }

        public Admin() : base()
        {
            HeadOffice = "Undefined";
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
        public override string ToString()
        {
            //9 fields
            return base.ToString() + $",{HeadOffice}";
        }
    }
}
