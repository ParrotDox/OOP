using ClassIerarchyLib;
using System;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;

//[Классы профессий в компании]
namespace ClassIerarchyLib
{
    [Serializable]
    public class Link : IInit
    {
        //Zondbi list
        public List<string> random_list = new List<string> { "Wa wobbo wabba wabba wabby wobbo wabba wa", "Wobba wobby wabba-wabba wabby wo-wo wabba wa", "Bra..., a a a ins" };
        public string Notes { get; set; }
        public string Data { get; set; }
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

    [JsonDerivedType(typeof(Person))]
    [JsonDerivedType(typeof(Employee))]
    [JsonDerivedType(typeof(Engineer))]
    [JsonDerivedType(typeof(Admin))]

    [XmlInclude(typeof(Person))]
    [XmlInclude(typeof(Employee))]
    [XmlInclude(typeof(Engineer))]
    [XmlInclude(typeof(Admin))]

    [Serializable]
    public class Person : IInit,  IComparable<Person>, ICloneable
    {
        protected static List<string> key_storage = new List<string>();
        public static string[] random_names = new string[] { "Jacky", "Johny", "Marigold", "Elizabeth", "Horo", "Danil", "Nikita", "Egor", "Sergey", "Vlad", "Andrew", "Maksim", "Oleg", "Anna", "Maddie" };
        public static string[] random_residences = new string[] { "Visim", "Krohalevka", "Serebryanskiy_Proezd", "Tsum", "Yralskaya", "Sadoviy", "Ivanovskaya", "Takayama_Street", "Night_Street", "Waterfall_street" };
        public string Key { get; set; }
        public string Name { get; set; }
        public int Age { get;set; }
        public string Residence { get; set; }
        public Link? link;

        public Person() 
        {
            Key = "None";
            Name = "Undefined";
            Age = 0;
            Residence = "Undefined";
            link = new Link("0", "None");
        }
        public Person(Person copy_sample)
        {
            this.Key = genKey();
            key_storage.Add(Key);
            this.Name = copy_sample.Name;
            this.Age = copy_sample.Age;
            this.Residence = copy_sample.Residence;
            this.link = new Link();
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
        public virtual void RandomInit() 
        {
            Random rnd = new Random();
            Key = genKey();
            key_storage.Add(Key);
            Name = random_names[rnd.Next(0, random_names.Length)];
            Age = rnd.Next(16, 61);
            Residence = random_residences[rnd.Next(0, random_residences.Length)];
        }
        public virtual void RandomInit(string keySample)
        {
            Random rnd = new Random();
            Key = keySample;
            key_storage.Add(Key);
            Name = random_names[rnd.Next(0, random_names.Length)];
            Age = rnd.Next(16, 61);
            Residence = random_residences[rnd.Next(0, random_residences.Length)];
        }
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
        public virtual object Clone() 
        {
            Person copy = new Person();
            copy.Name = this.Name;
            copy.Age = this.Age;
            copy.Residence = this.Residence;
            copy.link = new Link(this.link.Data, this.link.Notes);
            //key поле является уникальным, но в контексте 11 лаб.
            //требуется найти объект по значению, а не по ссылке
            copy.Key = this.Key;
            //copy.key = genKey();
            //keyStorage.Add(key);
            return copy;
        }
        public virtual object ShallowCopy() 
        {
            Person copy = new Person();
            copy.Key = this.Key;
            //copy.key = genKey();
            //keyStorage.Add(key);
            copy.Name = this.Name;
            copy.Age = this.Age;
            copy.Residence = this.Residence;
            copy.link = this.link;
            return copy;
        }
        public override string ToString()
        {
            return $"{Key},{Name},{Age},{Residence},";
        }
        private string genKey()
        {
            string key = Guid.NewGuid().ToString();
            while (key_storage.Contains(key))
            {
                key = Guid.NewGuid().ToString();
            }
            return key;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Age, Residence, Key);
        }
    }

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
    public class SortByName : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}
