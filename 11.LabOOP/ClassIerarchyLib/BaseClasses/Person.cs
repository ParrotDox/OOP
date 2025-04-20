using ClassIerarchyLib;
using System;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Xml.Linq;

//[Классы профессий в компании]
namespace ClassIerarchyLib
{
    //Класс Link предназначен для демонстрации поверхностного и глубокого копирования, он используется в Person классе!
    [Serializable]
    public class Link : IInit
    {
        //Zondbi list
        public List<string> random_list = new List<string> { "Wa wobbo wabba wabba wabby wobbo wabba wa", "Wobba wobby wabba-wabba wabby wo-wo wabba wa", "Bra..., a a a ins" };
        private string _notes = "";
        public string Notes {  get { return _notes; } set { _notes = value; } }
        private string _data = "";
        public string Data { get { return _data; } set { _data = value; } }
        public Link() 
        {
            Notes = "None";
            Data = "0";
        }
        public Link(string dataSample) 
        {
            Data = dataSample;
        }
        public Link(string dataSample, string noteSample) : this(dataSample)
        {
            Notes = noteSample;
        }
        public void Show() 
        {
            Console.WriteLine($"Notes: {Notes}");
            Console.WriteLine($"Data: {Data}");
        }
        public void Init() 
        {
            Console.WriteLine("Your Notes:");
            Notes = Console.ReadLine();
            Console.WriteLine("Your Data:");
            Data = Console.ReadLine();
        }
        public void RandomInit() 
        {
            Random rnd = new Random();
            Notes = random_list[rnd.Next(0, random_list.Count)];
            Data = random_list[rnd.Next(0, random_list.Count)];
        }
        
    }
    //Древо наследования:
    /* Person --- Employee ---|-- Engineer
     *                        |-- Admin
     */
    [Serializable]
    public class Person : IInit,  IComparable<Person>, ICloneable
    {
        protected static List<int> key_storage = new List<int>();
        protected int key;
        public static string[] random_names = new string[] { "Jacky", "Johny", "Marigold", "Elizabeth", "Horo", "Danil", "Nikita", "Egor", "Sergey", "Vlad", "Andrew", "Maksim", "Oleg", "Anna", "Maddie" };
        public static string[] random_residences = new string[] { "Visim", "Krohalevka", "Serebryanskiy_Proezd", "Tsum", "Yralskaya", "Sadoviy", "Ivanovskaya", "Takayama_Street", "Night_Street", "Waterfall_street" };
        private string _name;
        public string Name 
        {
            get
            {
                return _name;
            }
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Name field is empty!");
                }
                _name = value;
            }
        }
        private int _age;
        public int Age 
        {
            get 
            {
                return _age;
            }
            set 
            {
                if (value < 16 || value > 100) 
                {
                    throw new ArgumentException("Age must be from 16 and 100");
                }
                _age = value;
            }
        }
        private string _residence;
        public string Residence 
        {
            get
            {
                return _residence;
            }
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Residence field is empty!");
                }
                _residence = value;
            }
        }
        //Поле предназначено для демонстрации поверхностного и глубокого копирования
        public Link? link;

        public Person() 
        {
            key = -1;
            Name = "Undefined";
            Age = 16;
            Residence = "Undefined";
            link = new Link("0", "None");
        }
        public Person(string name, int age, string residence) 
        {
            key = genKey();
            key_storage.Add(key);
            Name = name;
            Age = age;
            Residence = residence;
            link = new Link(age.ToString(), name + " lives in " + residence);
        }
        public Person(string name, int age, string residence, Link link_sample)
        {
            key = genKey();
            key_storage.Add(key);
            Name = name;
            Age = age;
            Residence = residence;
            link = link_sample;
        }
        //Этот метод копирования проводит копирование только ЗНАЧИМЫХ полей, ссылочное поле не затронуто, создается по-умолчанию новый экзепляр Link через базовый конструктор
        public Person(Person copy_sample)
        {
            this.key = genKey();
            key_storage.Add(key);
            this.Name = copy_sample.Name;
            this.Age = copy_sample.Age;
            this.Residence = copy_sample.Residence;
            this.link = new Link();
        }
        public virtual void Show() 
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"Residence: {Residence}");
            Console.WriteLine($"Notes: {link.Notes}");
            Console.WriteLine($"Data: {link.Data}");
        }
        public virtual string GetInfo() 
        {
            string msg = "";
            msg += $"Name: {Name}\n";
            msg += $"Age: {Age}\n";
            msg += $"Residence: {Residence}\n";
            msg += $"Notes: {link.Notes}\n";
            msg += $"Data: {link.Data}\n";
            return msg;
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
                    Name = inputString;
                    nameFlag = true;
                }
                if (!ageFlag) 
                {
                    Console.WriteLine("Input age:");
                    int parsedInt;
                    nameFlag = Int32.TryParse(Console.ReadLine(), out parsedInt);
                    Age = parsedInt;
                    ageFlag = true;
                }
                if (!residenceFlag)
                {
                    Console.WriteLine("Input residence:");
                    string inputString = "";
                    inputString = Console.ReadLine();
                    Residence = inputString;
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
            key = genKey();
            key_storage.Add(key);
            Name = random_names[rnd.Next(0, random_names.Length)];
            Age = rnd.Next(16, 61);
            Residence = random_residences[rnd.Next(0, random_residences.Length)];
        }
        //Сравнивает 2 объекта по содержимому, но не по ключу (Хотя ключ есть в Хэш-коде)
        public override bool Equals(object obj) 
        {
            if (obj is not Person)
            {
                return false;
            }
            Person sample = (Person)obj;
            if (this.Name == sample.Name &&
                this.Age == sample.Age &&
                this.Residence == sample.Residence &&
                this.GetHashCode() == sample.GetHashCode()) 
            {
                return true;
            }
            else { return false; }
        }
        //Реализация метода CompareTo из IComparable
        //Сравнение идет по полю Age
        public int CompareTo(Person obj) 
        {
            //Полный вариант логики CompareTo
            //if (obj is null) return 1;
            //if (this.Age > temp.Age)
            //    return 1;
            //if (this.Age < temp.Age)
            //    return -1;
            //return 0;
            //Краткий вариант CompareTo
            return this.Age.CompareTo(obj.Age);
        }
        //Реализация метода из ICloneable, глубокое копирование
        //Заметка: клонируются уникальные поля!
        public virtual object Clone() 
        {
            Person copy = new Person();
            copy.Name = this.Name;
            copy.Age = this.Age;
            copy.Residence = this.Residence;
            copy.link = new Link(this.link.Data, this.link.Notes);
            //key поле является уникальным, но в контексте 11 лаб.
            //требуется найти объект по значению, а не по ссылке
            copy.key = this.key;
            //copy.key = genKey();
            //keyStorage.Add(key);
            return copy;
        }
        //Метод поверхностного копирования
        //Заметка: клонируются уникальные поля!
        public virtual object ShallowCopy() 
        {
            Person copy = new Person();
            copy.key = this.key;
            //copy.key = genKey();
            //keyStorage.Add(key);
            copy.Name = this.Name;
            copy.Age = this.Age;
            copy.Residence = this.Residence;
            copy.link = this.link;
            return copy;
        }
        //Переопределение метода ToString для 11 лаб.
        public override string ToString()
        {
            return $"{key}";
        }
        private int genKey()
        {
            int key = 0;
            while (key_storage.Contains(key))
            {
                ++key;
            }
            return key;
        }
        //Переопределение GetHashCode для формирования хэш-кода по значениям полей
        public override int GetHashCode()
        {
            return HashCode.Combine(_name, _age, _residence, key);
        }
    }
    //Реализация метода Compare из IComparer по полю _age
    //Не знаю почему, но только если будет указано явно пространство System, то тогда данная сортировка сработает для ArrayList
    public class SortObjects : System.Collections.IComparer
    {
        public int Compare(object? x, object? y) 
        {
            if(x == null || y == null)
                throw new ArgumentNullException("ERROR: Null link");
            if(x is Person xP && y is Person yP) 
            {
                return xP.Age.CompareTo(yP.Age);
            }
            else 
            {
                throw new ArgumentException("ERROR: Wrong type");
            }
        }
    }
    public class SortByAge : IComparer<Person>
    {
        public int Compare(Person x, Person y) 
        {
            return x.Age.CompareTo(y.Age);
        }
    }
    //Реализация метода Compare из IComparer по полю Name
    public class SortByName : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}
