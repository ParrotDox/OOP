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
    public class Person : IInit, IComparable, IComparable<Person>, ICloneable
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
            __Name__ = "Undefined";
            __Age__ = 16;
            __Residence__ = "Undefined";
            lnk = new Link("0", "None");
        }
        public Person(string name, int age, string residence) 
        {
            __Name__ = name;
            __Age__ = age;
            __Residence__ = residence;
            lnk = new Link(age.ToString(), name + " lives in " + residence);
        }
        public Person(string name, int age, string residence, Link lnkSample)
        {
            __Name__ = name;
            __Age__ = age;
            __Residence__ = residence;
            lnk = lnkSample;
        }
        //Этот метод копирования проводит копирование только ЗНАЧИМЫХ полей, ссылочное поле не затронуто, создается по-умолчанию новый экзепляр Link через базовый конструктор
        public Person(Person copySample)
        {
            this.__Name__ = copySample.__Name__;
            this.__Age__ = copySample.__Age__;
            this.__Residence__ = copySample.__Residence__;
            this.lnk = new Link();
        }

        public virtual void Show() 
        {
            Console.WriteLine($"Name: {__Name__}");
            Console.WriteLine($"Age: {__Age__}");
            Console.WriteLine($"Residence: {__Residence__}");
            Console.WriteLine($"Notes: {lnk.notes}");
            Console.WriteLine($"Data: {lnk.data}");
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
            return this.__Age__.CompareTo(obj.__Age__);
        }
        public int CompareTo(object? obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException("Object link is null");
            }
            if (obj is not Person other)
            {
                throw new ArgumentException("Object is not a Person");
            }

            return this.__Age__.CompareTo(other.__Age__);
        }
        //Реализация метода из ICloneable, глубокое копирование
        public virtual object Clone() 
        {
            return new Person(this.__Name__, this.__Age__, this.__Residence__, new Link(lnk.data, lnk.notes));
        }
        //Метод поверхностного копирования
        public virtual object ShallowCopy() 
        {
            return this.MemberwiseClone();
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
    public class PersonComparer : IComparer<object>
    {
        public int Compare(object x, object y)
        {
            if (x is Person personX && y is Person personY)
            {
                return personX.__Age__.CompareTo(personY.__Age__);
            }
            if (x == null) return y == null ? 0 : -1;
            if (y == null) return 1;
            throw new ArgumentException("Both objects must be of type Person.");
        }
    }
}
