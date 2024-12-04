using ClassIerarchyLib;

namespace UnitTestsCodeCoverage
{
    public class UnitTests
    {
        //LinkTests
        [Fact]
        public void Constructor_Link()
        {
            //Arrange
            Link link = new Link();
            Link link2 = new Link("DataSamples");
            Link link3 = new Link("DataSamples", "NoteSample");
            //Act

            //Assert
            Assert.NotNull(link);
            Assert.NotNull(link2);
            Assert.NotNull(link3);
        }
        [Fact]
        public void Link_RandomInit()
        {
            //Arrange
            Link link = new Link("DataSamples");
            //Act
            link.RandomInit();
            //Assert
            Assert.NotNull(link.data);
            Assert.NotNull(link.notes);
        }

        //PersonTests
        [Fact]
        public void Constructor_Person()
        {
            //Arrange
            Person person = new Person();
            Person person2 = new Person("Name", 25, "Residence");
            Person person3 = new Person("Name", 25, "Residence", new Link("Data", "Notes"));
            Person person4 = new Person(person);
            //Act

            //Assert
            Assert.NotNull(person);
            Assert.NotNull(person2);
            Assert.NotNull(person3);
            Assert.NotNull(person4);
            Assert.NotSame(person.lnk, person4.lnk);
        }
        [Fact]
        public void Person_RandomInit()
        {
            //Arrange
            Person person = new Person();
            //Act
            person.RandomInit();
            //Assert
            Assert.NotNull(person.__Name__);
            Assert.NotNull(person.__Age__);
            Assert.NotNull(person.__Residence__);
            Assert.NotNull(person.lnk);
        }
        [Fact]
        public void Person_Equals() 
        {
            //Arrange
            Person person = new Person("Name", 25, "Residence");
            Person person2 = new Person("Name", 25, "Residence");
            Person person3 = new Person();
            //Act
            bool checkOne = person.Equals(25);
            bool checkTwo = person2.Equals(person3);
            bool checkThree = person2.Equals(person);
            //Assert
            Assert.Equal(false, checkOne);
            Assert.Equal(false, checkTwo);
            Assert.Equal(true, checkThree);
        }
        [Fact]
        public void Person_CompareTo()
        {
            //Arrange
            Person person = new Person("Name", 25, "Residence");
            Person person2 = new Person("Name", 25, "Residence");
            Person person3 = new Person("Name", 19, "Residence");
            //Act
            int checkOne = person.CompareTo(person2);
            int checkTwo = person2.CompareTo(person3);
            int checkThree = person3.CompareTo(person2);
            //Assert
            Assert.Equal(0, checkOne);
            Assert.Equal(1, checkTwo);
            Assert.Equal(-1, checkThree);
        }
        [Fact]
        public void Person_Clone()
        {
            //Arrange
            Person person = new Person("Name", 25, "Residence");
            //Act
            Person personClone = (Person)person.Clone();
            //Assert
            Assert.Equal(person.__Name__, personClone.__Name__);
            Assert.Equal(person.__Age__, personClone.__Age__);
            Assert.Equal(person.__Residence__, personClone.__Residence__);
            Assert.NotSame(person.lnk, personClone.lnk);
        }
        [Fact]
        public void Person_ShallowCopy()
        {
            //Arrange
            Person person = new Person("Name", 25, "Residence");
            //Act
            Person personClone = (Person)person.ShallowCopy();
            //Assert
            Assert.Equal(person.__Name__, personClone.__Name__);
            Assert.Equal(person.__Age__, personClone.__Age__);
            Assert.Equal(person.__Residence__, personClone.__Residence__);
            Assert.Same(person.lnk, personClone.lnk);
        }

        //EmployeeTests
        [Fact]
        public void Constructor_Employee()
        {
            //Arrange
            Employee person = new Employee();
            Employee person2 = new Employee("Name", 25, "Residence", 123, 8, 30005);
            Employee person3 = new Employee("Name", 25, "Residence", 123, 8, 30005);
            Employee person4 = new Employee(person);
            //Act

            //Assert
            Assert.NotNull(person);
            Assert.NotNull(person2);
            Assert.NotNull(person3);
            Assert.NotNull(person4);
            Assert.NotSame(person.lnk, person4.lnk);
        }
        [Fact]
        public void Employee_RandomInit()
        {
            //Arrange
            Employee person = new Employee();
            //Act
            person.RandomInit();
            //Assert
            Assert.NotNull(person.__Name__);
            Assert.NotNull(person.__Age__);
            Assert.NotNull(person.__Residence__);
            Assert.NotNull(person.lnk);
            Assert.NotNull(person.__Id__);
            Assert.NotNull(person.__Experience__);
            Assert.NotNull(person.__Salary__);
        }
        [Fact]
        public void Employee_Equals()
        {
            //Arrange
            Employee person = new Employee("Name", 25, "Residence", 123, 8, 30005);
            Employee person2 = new Employee("Name", 25, "Residence", 123, 8, 30005);
            Employee person3 = new Employee();
            //Act
            bool checkOne = person.Equals(25);
            bool checkTwo = person2.Equals(person3);
            bool checkThree = person2.Equals(person);
            //Assert
            Assert.Equal(false, checkOne);
            Assert.Equal(false, checkTwo);
            Assert.Equal(true, checkThree);
        }

        //EngineerTests
        [Fact]
        public void Constructor_Engineer()
        {
            //Arrange
            Engineer person = new Engineer();
            Engineer person2 = new Engineer("Name", 25, "Residence", 123, 8, 30005, "Cabbage_Department");
            Engineer person3 = new Engineer("Name", 25, "Residence", 123, 8, 30005, "Cabbage_Department");
            Engineer person4 = new Engineer(person);
            //Act

            //Assert
            Assert.NotNull(person);
            Assert.NotNull(person2);
            Assert.NotNull(person3);
            Assert.NotNull(person4);
            Assert.NotSame(person.lnk, person4.lnk);
        }
        [Fact]
        public void Engineer_RandomInit()
        {
            //Arrange
            Engineer person = new Engineer();
            //Act
            person.RandomInit();
            //Assert
            Assert.NotNull(person.__Name__);
            Assert.NotNull(person.__Age__);
            Assert.NotNull(person.__Residence__);
            Assert.NotNull(person.lnk);
            Assert.NotNull(person.__Id__);
            Assert.NotNull(person.__Experience__);
            Assert.NotNull(person.__Salary__);
            Assert.NotNull(person.__Department__);
        }
        [Fact]
        public void Engineer_Equals()
        {
            //Arrange
            Engineer person = new Engineer("Name", 25, "Residence", 123, 8, 30005, "Cabbage_Department");
            Engineer person2 = new Engineer("Name", 25, "Residence", 123, 8, 30005, "Cabbage_Department");
            Engineer person3 = new Engineer();
            //Act
            bool checkOne = person.Equals(25);
            bool checkTwo = person2.Equals(person3);
            bool checkThree = person2.Equals(person);
            //Assert
            Assert.Equal(false, checkOne);
            Assert.Equal(false, checkTwo);
            Assert.Equal(true, checkThree);
        }

        //AdminTests
        [Fact]
        public void Constructor_Admin()
        {
            //Arrange
            Admin person = new Admin();
            Admin person2 = new Admin("Name", 25, "Residence", 123, 8, 30005, "Cabbage_Office");
            Admin person3 = new Admin("Name", 25, "Residence", 123, 8, 30005, "Cabbage_Office");
            Admin person4 = new Admin(person);
            //Act

            //Assert
            Assert.NotNull(person);
            Assert.NotNull(person2);
            Assert.NotNull(person3);
            Assert.NotNull(person4);
            Assert.NotSame(person.lnk, person4.lnk);
        }
        public void Admin_RandomInit()
        {
            //Arrange
            Admin person = new Admin();
            //Act
            person.RandomInit();
            //Assert
            Assert.NotNull(person.__Name__);
            Assert.NotNull(person.__Age__);
            Assert.NotNull(person.__Residence__);
            Assert.NotNull(person.lnk);
            Assert.NotNull(person.__Id__);
            Assert.NotNull(person.__Experience__);
            Assert.NotNull(person.__Salary__);
            Assert.NotNull(person.__HeadOffice__);
        }
        [Fact]
        public void Admin_Equals()
        {
            //Arrange
            Admin person = new Admin("Name", 25, "Residence", 123, 8, 30005, "Cabbage_Office");
            Admin person2 = new Admin("Name", 25, "Residence", 123, 8, 30005, "Cabbage_Office");
            Admin person3 = new Admin();
            //Act
            bool checkOne = person.Equals(25);
            bool checkTwo = person2.Equals(person3);
            bool checkThree = person2.Equals(person);
            //Assert
            Assert.Equal(false, checkOne);
            Assert.Equal(false, checkTwo);
            Assert.Equal(true, checkThree);
        }

        //SortByAgeTests
        [Fact]
        public void SortByAge_Compare() 
        {
            //Arrange
            bool flag = true;
            int prevAge = 0;
            List<Person> people = new List<Person>();
            QueryMaker.InitCompanyArray();
            foreach (Person person in QueryMaker.company)
                people.Add(person);
            //Act
            people.Sort(new SortByAge());
            foreach(Person person in people) 
            {
                if(prevAge > person.__Age__)
                    flag = false;
                else
                    prevAge = person.__Age__;
            }
            //Assert
            Assert.Equal(true, flag);
        }
        [Fact]
        public void SortByAge_CompareNull()
        {
            //Arrange
            bool flag = true;
            int prevAge = 0;
            List<Person> people = new List<Person>();
            QueryMaker.InitCompanyArray();
            foreach (Person person in QueryMaker.company)
                people.Add(person);
            people.Add(null);
            people.Add(null);
            //Act

            //Assert
            Assert.Throws<InvalidOperationException>(() => people.Sort(new SortByAge()));
        }

        //SortByNameTests
        [Fact]
        public void SortByName_Compare()
        {
            //Arrange
            bool flag = true;
            string prevName = "";
            List<Person> people = new List<Person>();
            QueryMaker.InitCompanyArray();
            foreach (Person person in QueryMaker.company)
                people.Add(person);
            //Act
            people.Sort(new SortByName());
            foreach (Person person in people)
            {
                int comparation = string.Compare(prevName, person.__Name__);
                if(comparation > 0) 
                {
                    flag = false;
                }
                prevName = person.__Name__;
            }
            //Assert
            Assert.Equal(true, flag);
        }
        [Fact]
        public void SortByName_CompareNull()
        {
            //Arrange
            bool flag = true;
            int prevAge = 0;
            List<Person> people = new List<Person>();
            QueryMaker.InitCompanyArray();
            foreach (Person person in QueryMaker.company)
                people.Add(person);
            people.Add(null);
            people.Add(null);
            //Act

            //Assert
            Assert.Throws<InvalidOperationException>(() => people.Sort(new SortByName()));
        }

        //BinarySearchTests
        [Fact]
        public void QueryLib_BinarySearch() 
        {
            //Arrange
            Person person = new Person();
            Person person1 = new Person("Bob", 23, "HOME");
            Person person2 = new Person("Ariral", 23, "HOME");
            Person person3 = new Person("Margaret", 26, "HOME");
            Person person4 = new Person("Nile", 19, "HOME");
            Person person5 = new Person("Crocodile", 23, "HOME");
            Person person6 = new Person("Niko", 55, "HOME");
            List<Person>  people1 = new List<Person>();
            people1.Add(person);
            people1.Add(person);
            people1.Add(person);
            people1.Add(person2);
            people1.Add(person2);
            people1.Add(person5);
            people1.Add(person4);
            people1.Add(person6);
            people1.Add(person4);
            List<Person>  people2 = new List<Person>();
            people2.Add(person2);
            people2.Add(person2);
            people2.Add(person1);
            people2.Add(person3);
            people2.Add(person);
            people2.Add(person5);
            people2.Add(person4);
            people2.Add(person4);
            people2.Add(person6);
            List<Person>  people3 = new List<Person>();
            people3.Add(person);
            people3.Add(person);
            people3.Add(person);
            people3.Add(person3);
            people3.Add(person);
            people3.Add(person5);
            people3.Add(person4);
            people3.Add(person4);
            people3.Add(person6);
            int checkOne;
            int checkTwo;
            int checkThree;
            int checkFour;
            //Act
            people1.Sort(new SortByName());
            people2.Sort(new SortByName());
            people3.Sort(new SortByName());
            checkOne = QueryMaker.BinarySearch(people1, "Nile");
            checkTwo = QueryMaker.BinarySearch(people2, "Ariral");
            checkThree = QueryMaker.BinarySearch(people3, "Niko");
            checkFour = QueryMaker.BinarySearch(people1, "---nfnfnnnfg");
            //Assert
            Assert.NotNull(checkOne);
            Assert.NotEqual(-1, checkOne);
            Assert.NotNull(checkTwo);
            Assert.NotEqual(-1, checkTwo);
            Assert.NotNull(checkThree);
            Assert.NotEqual(-1, checkThree);
            Assert.NotNull(checkFour);
            Assert.Equal(-1, checkFour);
        }
    }
}