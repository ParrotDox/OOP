using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIerarchyLib
{
    public class Employee : Person
    {
        protected static List<int> _id_storage = new List<int>();
        private int _id;
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value < 0 && !_id_storage.Contains(value))
                {
                    throw new ArgumentException("Id exception!");
                }
                _id = value;
            }
        }
        private int _experience;
        public int Experience
        {
            get
            {
                return _experience;
            }
            set
            {
                if (value < 2 || value > 84)
                {
                    throw new ArgumentException("Experience must be between 2 and 84");
                }
                _experience = value;
            }
        }
        private int _salary;
        public int Salary
        {
            get
            {
                return _salary;
            }
            set
            {
                if (value < 20000 || value > 200000)
                {
                    throw new ArgumentException("Salary must be between 20'000 and 200'000");
                }
                _salary = value;
            }
        }

        public Employee() : base()
        {
            _id = -1;
            Experience = 2;
            Salary = 20000;
        }
        public Employee(string name, int age, string residence, int id, int experience, int salary) : base(name, age, residence)
        {
            Id = id;
            _id_storage.Add(id);
            Experience = experience;
            Salary = salary;
        }
        public Employee(string name, int age, string residence, int id, int experience, int salary, Link link_sample) : base(name, age, residence, link_sample)
        {
            Id = id;
            _id_storage.Add(Id);
            Experience = experience;
            Salary = salary;
        }
        public Employee(Employee copy_sample) : base(copy_sample)
        {
            Id = genId();
            _id_storage.Add(Id);
            Experience = copy_sample._experience;
            Salary = copy_sample._salary;
        }
        //Возврат ссылки на базовый экземпляр класса (3 задание 11 лаб.)
        public Person basePerson() 
        {
            return (Person)this.Clone();
        }
        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"Experience: {Experience}");
            Console.WriteLine($"Salary: {Salary}");
        }
        public override string GetInfo()
        {
            string msg = base.GetInfo();
            msg += $"Id: {Id}\n";
            msg += $"Experience: {Experience}\n";
            msg += $"Salary: {Salary}\n";
            return msg;
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
                    Id = parsedInt;
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
            Id = genId();
            _id_storage.Add(Id);
            Experience = rnd.Next(2, 85);
            Salary = rnd.Next(20000, 200001);
        }
        //Сравнивает 2 объекта по содержимому, но не по ключу
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
        //Заметка: клонируются уникальные поля!
        public override object Clone()
        {
            Employee copy = new Employee();
            copy.Name = this.Name;
            copy.Age = this.Age;
            copy.Residence = this.Residence;
            copy.link = new Link(link.notes, link.data);
            copy.Experience = this.Experience;
            copy.Salary = this.Salary;
            //id поле является уникальным, но в контексте 11 лаб.
            //требуется найти объект по значению, а не по ссылке
            copy._id = this.Id;
            //key поле является уникальным, но в контексте 11 лаб.
            //требуется найти объект по значению, а не по ссылке
            copy.key = this.key;
            return copy;
        }
        private int genId() 
        {
            int id = 0;
            while (_id_storage.Contains(id)) 
            {
                ++id;
            }
            return id;
        }
        //Переопределение GetHashCode для формирования хэш-кода по значениям полей
        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Experience, Salary, Id);
        }
    }
}
