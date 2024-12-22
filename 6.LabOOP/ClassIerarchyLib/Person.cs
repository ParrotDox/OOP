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
    public class Person : IInit,  IComparable<Person>, ICloneable
    {
        protected static List<int> keyStorage = new List<int>();
        protected int key;
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
            key = -1;
            __Name__ = "Undefined";
            __Age__ = 16;
            __Residence__ = "Undefined";
            lnk = new Link("0", "None");
        }
        public Person(string name, int age, string residence) 
        {
            key = genKey();
            keyStorage.Add(key);
            __Name__ = name;
            __Age__ = age;
            __Residence__ = residence;
            lnk = new Link(age.ToString(), name + " lives in " + residence);
        }
        public Person(string name, int age, string residence, Link lnkSample)
        {
            key = genKey();
            keyStorage.Add(key);
            __Name__ = name;
            __Age__ = age;
            __Residence__ = residence;
            lnk = lnkSample;
        }
        //Этот метод копирования проводит копирование только ЗНАЧИМЫХ полей, ссылочное поле не затронуто, создается по-умолчанию новый экзепляр Link через базовый конструктор
        public Person(Person copySample)
        {
            this.key = genKey();
            keyStorage.Add(key);
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
            key = genKey();
            keyStorage.Add(key);
            __Name__ = rndNames[rnd.Next(0, rndNames.Length)];
            __Age__ = rnd.Next(16, 61);
            __Residence__ = rndResidences[rnd.Next(0, rndResidences.Length)];
        }
        //Сравнивает 2 объекта по содержимому, но не по ключу
        public override bool Equals(object obj) 
        {
            if (obj is not Person)
            {
                return false;
            }
            Person sample = (Person)obj;
            if (this.__Name__ == sample.__Name__ &&
                this.__Age__ == sample.__Age__ &&
                this.__Residence__ == sample.__Residence__ &&
                this.GetHashCode() == sample.GetHashCode()) 
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
        //Реализация метода из ICloneable, глубокое копирование
        //Заметка: клонируются уникальные поля!
        public virtual object Clone() 
        {
            Person copy = new Person();
            copy.__Name__ = this.__Name__;
            copy.__Age__ = this.__Age__;
            copy.__Residence__ = this.__Residence__;
            copy.lnk = new Link(this.lnk.data, this.lnk.notes);
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
            copy.__Name__ = this.__Name__;
            copy.__Age__ = this.__Age__;
            copy.__Residence__ = this.__Residence__;
            copy.lnk = this.lnk;
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
            while (keyStorage.Contains(key))
            {
                ++key;
            }
            return key;
        }
        //Переопределение GetHashCode для формирования хэш-кода по значениям полей
        public override int GetHashCode()
        {
            return HashCode.Combine(__Name__, __Age__, __Residence__, key);
        }
    }
    //Реализация метода Compare из IComparer по полю Age
    //Не знаю почему, но только если будет указано явно пространство System, то тогда данная сортировка сработает для ArrayList
    public class SortObjects : System.Collections.IComparer
    {
        public int Compare(object? x, object? y) 
        {
            if(x == null || y == null)
                throw new ArgumentNullException("ERROR: Null link");
            if(x is Person xP && y is Person yP) 
            {
                return xP.__Age__.CompareTo(yP.__Age__);
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
