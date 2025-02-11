﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIerarchyLib
{
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
                if (value < 2 || value > 84)
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
            __Id__ = 1;
            __Experience__ = 2;
            __Salary__ = 20000;
        }
        public Employee(string name, int age, string residence, int id, int experience, int salary) : base(name, age, residence)
        {
            __Id__ = id;
            __Experience__ = experience;
            __Salary__ = salary;
        }
        public Employee(string name, int age, string residence, int id, int experience, int salary, Link lnkSample) : base(name, age, residence, lnkSample)
        {
            __Id__ = id;
            __Experience__ = experience;
            __Salary__ = salary;
        }
        public Employee(Employee copySample) : base(copySample)
        {
            this.__Id__ = copySample.__id__;
            this.__Experience__ = copySample.__experience__;
            this.__Salary__ = copySample.__salary__;
        }
        
        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Id: {__Id__}");
            Console.WriteLine($"Experience: {__Experience__}");
            Console.WriteLine($"Salary: {__Salary__}");
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
        public override object Clone()
        {
            return new Employee(this.__Name__, this.__Age__, this.__Residence__,this.__Id__ ,this.__Experience__, this.__Salary__, new Link(lnk.data, lnk.notes));
        }
    }
}
