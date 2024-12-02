﻿using System;
using System.Security.AccessControl;

//[Классы профессий в компании]
namespace ClassIerarchyLib
{
    //Древо наследования:
    /* Person --- Employee ---|-- Engineer
     *                        |-- Admin
     */
    public class Person
    {
        public static string[] rndNames = new string[] { "Jacky", "Johny", "Marigold", "Elizabeth", "Horo", "Danil", "Nikita", "Egor", "Sergey", "Vlad", "Andrew", "Maksim", "Oleg", "Anna", "Maddie" };
        public static string[] rndResidences = new string[] { "Visim", "Krohalevka", "Serebryanskiy_Proezd", "Tsum", "Yralskaya", "Sadoviy", "Ivanovskaya", "Takayama_Street", "Night_Street", "Waterfall_street" };
        protected string __name__;
        protected string __Name__ 
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
        protected int __Age__ 
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
        protected string __Residence__ 
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

        public Person() 
        {
            __name__ = "Undefined";
            __age__ = 16;
            __residence__ = "Undefined";
        }
        public Person(string name, int age, string residence) 
        {
            __name__ = name;
            __age__ = age;
            __residence__ = residence;
        }
        public Person(Person copySample)
        {
            this.__name__ = copySample.__name__;
            this.__age__ = copySample.__age__;
            this.__residence__ = copySample.__residence__;
        }

        public virtual void Show() 
        {
            Console.WriteLine($"Name: {__name__}");
            Console.WriteLine($"Age: {__age__}");
            Console.WriteLine($"Residence: {__residence__}");
        }
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
    }
    public class Employee : Person
    {
        protected int __id__;
        protected int __Id__ 
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
        protected int __Experience__
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
        public static string[] rndDepartments = new string[] { "Department_of_aqua_technologies", "Department_of_food_production", "Department_of_space_production" };
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
            
            __Department__ = rndDepartments[rnd.Next(0, rndDepartments.Length)];
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
        public static string[] rndHeadOffices = new string[] { "Central_Perm_Office", "Central_night_street_office", "Central_space_office" };
        protected string __headOffice__;
        protected string __HeadOffice__ 
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
            __HeadOffice__ = rndHeadOffices[rnd.Next(0, rndHeadOffices.Length)];
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