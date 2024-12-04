using System;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Xml.Linq;

//[Классы профессий в компании]
namespace ClassIerarchyLib
{
    //Класс Link предназначен для демонстрации поверхностного и глубокого копирования, он используется в Person классе!
    public class Link : IInit
    {
        //Zondbi list
        public List<string> rndWobboWabbo = new List<string> { "Wa wobbo wabba wabba wabby wobbo wabba wa", "Wobba wobby wabba-wabba wabby wo-wo wabba wa", "Bra..., a a a ins" };
        public string notes = "";
        public string data = "";
        public Link() 
        {
            notes = "None";
            data = "0";
        }
        public Link(string dataSample) 
        {
            data = dataSample;
        }
        public Link(string dataSample, string noteSample) : this(dataSample)
        {
            notes = noteSample;
        }
        public void Show() 
        {
            Console.WriteLine($"Notes: {notes}");
            Console.WriteLine($"Data: {data}");
        }
        public void Init() 
        {
            Console.WriteLine("Your Notes:");
            notes = Console.ReadLine();
            Console.WriteLine("Your Data:");
            data = Console.ReadLine();
        }
        public void RandomInit() 
        {
            Random rnd = new Random();
            notes = rndWobboWabbo[rnd.Next(0, rndWobboWabbo.Count)];
            data = rndWobboWabbo[rnd.Next(0, rndWobboWabbo.Count)];
        }
        
    }
    //Древо наследования:
    /* Person --- Employee ---|-- Engineer
     *                        |-- Admin
     */
    public class Person : IInit, IComparable<Person>, ICloneable
    {
        public static string[] rndNames = new string[] { "Jacky", "Johny", "Marigold", "Elizabeth", "Horo", "Danil", "Nikita", "Egor", "Sergey", "Vlad", "Andrew", "Maksim", "Oleg", "Anna", "Maddie" };
        public static string[] rndResidences = new string[] { "Visim", "Krohalevka", "Serebryanskiy_Proezd", "Tsum", "Yralskaya", "Sadoviy", "Ivanovskaya", "Takayama_Street", "Night_Street", "Waterfall_street" };
        protected string __name__;
        public string __Name__ 
        {
            get
            {
                return __name__;
            }
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Name field is empty!");
                }
                __name__ = value;
            }
        }
        protected int __age__;
        public int __Age__ 
        {
            get 
            {
                return __age__;
            }
            set 
            {
                if (value < 16 || value > 100) 
                {
                    throw new ArgumentException("Age must be from 16 and 100");
                }
                __age__ = value;
            }
        }
        protected string __residence__;
        public string __Residence__ 
        {
            get
            {
                return __residence__;
            }
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Residence field is empty!");
                }
                __residence__ = value;
            }
        }
        //Поле предназначено для демонстрации поверхностного и глубокого копирования
        public Link? lnk;

        public Person() 
        {
            __name__ = "Undefined";
            __age__ = 16;
            __residence__ = "Undefined";
            lnk = new Link("0", "None");
        }
        public Person(string name, int age, string residence) 
        {
            __name__ = name;
            __age__ = age;
            __residence__ = residence;
            lnk = new Link(age.ToString(), name + " lives in " + residence);
        }
        public Person(string name, int age, string residence, Link lnkSample)
        {
            __name__ = name;
            __age__ = age;
            __residence__ = residence;
            lnk = lnkSample;
        }
        //Этот метод копирования проводит копирование только ЗНАЧИМЫХ полей, ссылочное поле не затронуто, создается по-умолчанию новый экзепляр Link через базовый конструктор
        public Person(Person copySample)
        {
            this.__name__ = copySample.__name__;
            this.__age__ = copySample.__age__;
            this.__residence__ = copySample.__residence__;
            this.lnk = new Link();
        }

        public virtual void Show() 
        {
            Console.WriteLine($"Name: {__name__}");
            Console.WriteLine($"Age: {__age__}");
            Console.WriteLine($"Residence: {__residence__}");
        }
        //Реализация Init из кастомного IInit
        public virtual void Init() 
        {
            bool nameFlag, ageFlag, residenceFlag;
            nameFlag = false;
            ageFlag = false;
            residenceFlag = false;
 
            inputMark:
            try
            {
                if (!nameFlag)
                {
                    Console.WriteLine("Input name:");
                    string inputString = "";
                    inputString = Console.ReadLine();
                    __Name__ = inputString;
                    nameFlag = true;
                }
                if (!ageFlag) 
                {
                    Console.WriteLine("Input age:");
                    int parsedInt;
                    nameFlag = Int32.TryParse(Console.ReadLine(), out parsedInt);
                    __Age__ = parsedInt;
                    ageFlag = true;
                }
                if (!residenceFlag)
                {
                    Console.WriteLine("Input residence:");
                    string inputString = "";
                    inputString = Console.ReadLine();
                    __Residence__ = inputString;
                    residenceFlag = true;
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                goto inputMark;
            }
        }
        //Реализация Init из кастомного IInit
        public virtual void RandomInit() 
        {
            Random rnd = new Random();
            __Name__ = rndNames[rnd.Next(0, rndNames.Length)];
            __Age__ = rnd.Next(16, 61);
            __Residence__ = rndResidences[rnd.Next(0, rndResidences.Length)];
        }
        public override bool Equals(object obj) 
        {
            if (obj is not Person)
            {
                return false;
            }
            Person sample = (Person)obj;
            if (this.__Name__ == sample.__Name__ &&
                this.__Age__ == sample.__Age__ &&
                this.__Residence__ == sample.__Residence__) 
            {
                return true;
            }
            else { return false; }
        }
        //Реализация метода CompareTo из IComparable
        //Сравнение идет по полю __age__
        public int CompareTo(Person obj) 
        {
            //if (obj is null) return 1;
            //if (this.__age__ > temp.__age__)
            //    return 1;
            //if (this.__age__ < temp.__age__)
            //    return -1;
            //return 0;
            //Другой вариант кода при сравнении простых полей типа int, string
            return this.__age__.CompareTo(obj.__age__);
        }
        //Реализация метода из ICloneable, глубокое копирование
        public virtual object Clone() 
        {
            return new Person(this.__name__, this.__age__, this.__residence__, new Link(lnk.data, lnk.notes));
        }
        //Метод поверхностного копирования
        public virtual object ShallowCopy() 
        {
            return this.MemberwiseClone();
        }
    }
    public class Employee : Person
    {
        protected int __id__;
        public int __Id__ 
        {
            get
            {
                return __id__;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Id must be positive!");
                }
                __id__ = value;
            }
        }
        protected int __experience__;
        public int __Experience__
        {
            get 
            {
                return __experience__;
            } 
            set 
            {
                if( value < 2 || value > 84) 
                {
                    throw new ArgumentException("Experience must be between 2 and 84");
                }
                __experience__ = value;
            }
        }
        protected int __salary__;
        public int __Salary__ 
        {
            get
            {
                return __salary__;
            }
            set
            {
                if (value < 20000 || value > 200000)
                {
                    throw new ArgumentException("Salary must be between 20'000 and 200'000");
                }
                __salary__ = value;
            }
        }

        public Employee() : base()
        { 
            __id__ = 1;
            __experience__ = 2;
            __salary__ = 20000;
        }
        public Employee(string name, int age, string residence, int id, int experience, int salary) : base(name, age, residence)
        {
            __id__= id;
            __Experience__ = experience;
            __Salary__ = salary;
        }
        public Employee(Employee copySample) : base(copySample)
        {
            this.__id__ = copySample.__id__;
            this.__experience__ = copySample.__experience__;
            this.__salary__ = copySample.__salary__;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Id: {__id__}");
            Console.WriteLine($"Experience: {__experience__}");
            Console.WriteLine($"Salary: {__salary__}");
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
                    __Id__ = parsedInt;
                    idFlag = true;
                }
                if (!experienceFlag)
                {
                    Console.WriteLine("Input experience:");
                    int parsedInt;
                    experienceFlag = Int32.TryParse(Console.ReadLine(), out parsedInt);
                    __Experience__ = parsedInt;
                    experienceFlag = true;
                }
                if (!salaryFlag)
                {
                    Console.WriteLine("Input salary:");
                    int parsedInt;
                    salaryFlag = Int32.TryParse(Console.ReadLine(), out parsedInt);
                    __Salary__ = parsedInt;
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
            __Id__ = rnd.Next(0, 10000000);
            __Experience__ = rnd.Next(2, 85);
            __Salary__ = rnd.Next(20000, 200001);
        }
        public override bool Equals(object obj)
        {
            if (obj is not Employee)
            {
                return false;
            }
            Employee sample = (Employee)obj;
            if (base.Equals(obj) &&
                this.__Name__ == sample.__Name__ &&
                this.__Age__ == sample.__Age__ &&
                this.__Residence__ == sample.__Residence__)
            {
                return true;
            }
            else { return false; }
        }

    }
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
            __headOffice__ = "Undefined";
        }
        public Admin(string name, int age, string residence, int id, int experience, int salary, string headOffice) : base(name, age, residence, id, experience, salary)
        {
            __headOffice__ = headOffice;
        }
        public Admin(Admin copySample) : base(copySample)
        { 
            this.__headOffice__ = copySample.__headOffice__;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Head_Office: {__headOffice__}");
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

    //Реализация метода Compare из IComparer по полю Age
    public class SortByAge : IComparer<Person> 
    {
        public int Compare(Person x, Person y) 
        {
            return x.__Age__.CompareTo(y.__Age__);
        }
    }
    //Реализация метода Compare из IComparer по полю Name
    public class SortByName : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            return x.__Name__.CompareTo(y.__Name__);
        }
    }
}
