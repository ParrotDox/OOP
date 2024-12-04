using ClassIerarchyLib;
using System.Security.AccessControl;


//[2 PART - Comparable]

Console.WriteLine("[2 PART - Comparable]");

//В List добавим 10 объектов, объекты получим из static массива класса QueryMaker 
List<Person> people = new List<Person>();
QueryMaker.InitCompanyArray();
foreach(Person person in QueryMaker.company)
    people.Add(person);
Console.WriteLine("---[LIST BEFORE SORTING]---");
foreach (Person person in people) 
{
    person.Show();
    Console.WriteLine("-");
}

people.Sort();
Console.WriteLine("---[LIST AFTER SORTING BY AGE]---");
foreach (Person person in people)
{
    person.Show();
    Console.WriteLine("-");
}

Console.WriteLine("Press enter to continue...");
Console.ReadLine();
Console.Clear();

//[3 PART - Comparer]

Console.WriteLine("[3 PART - Comparer]");

//В List добавим 10 объектов, объекты получим из static массива класса QueryMaker 
List<Person> people2 = new List<Person>();
QueryMaker.InitCompanyArray();
foreach (Person person in QueryMaker.company)
    people2.Add(person);
Console.WriteLine("---[LIST BEFORE SORTING]---");
foreach (Person person in people2)
{
    person.Show();
    Console.WriteLine("-");
}

people2.Sort(new SortByName());
Console.WriteLine("---[LIST AFTER SORTING BY NAME]---");
foreach (Person person in people2)
{
    person.Show();
    Console.WriteLine("-");
}

Console.WriteLine("Press enter to continue...");
Console.ReadLine();
Console.Clear();
//[5 PART - BINARY SEARCH]

Console.WriteLine("[5 PART - BINARY SEARCH]");

//Создаем новый список из 20 элементов
people = new List<Person>();
QueryMaker.InitCompanyArray();
foreach (Person person in QueryMaker.company)
    people.Add(person);
QueryMaker.InitCompanyArray();
foreach (Person person in QueryMaker.company)
    people.Add(person);

//Перед Бинарным поиском сортируем список!!!
people.Sort(new SortByName());
for(int i = 0; i < people.Count; ++i)
{
    Console.WriteLine($"Index: {i}");
    people[i].Show();
    Console.WriteLine("-");
}
//Результат записываем в переменную типа int, это позиция элемента в списке
string key = "Nikita";
int indexOfElem = QueryMaker.BinarySearch(people, key);
Console.WriteLine($"{key} is at the {indexOfElem} index");

Console.WriteLine("Press enter to continue...");
Console.ReadLine();
Console.Clear();

//[6 PART - IInit List]

Console.WriteLine("[6 PART - IInit List]");

List<IInit> inits = new List<IInit>();

//В List добавим 6 объектов, 3 Создадим самостоятельно, 3 создадим случайно
Console.WriteLine("---[MANUAL INIT]---");
Person temp = new Person();
for (int i = 0; i < 2; i++) 
{
    temp.Init();
    inits.Add(temp);
    temp.Show();
    Console.WriteLine('-');
    temp = new Engineer();
}

//Тут новый класс
Link tempLnk = new Link();
tempLnk.Init();
tempLnk.Show();
inits.Add(tempLnk);

Console.WriteLine("---[RANDOM INIT]---");
temp = new Employee();
for(int i = 0; i < 2; i++) 
{
    temp.RandomInit();
    inits.Add(temp);
    temp.Show();
    Console.WriteLine('-');
    temp = new Admin();
}

//Тут новый класс
tempLnk = new Link();
tempLnk.RandomInit();
tempLnk.Show();
inits.Add(tempLnk);

Console.WriteLine("---[RESULT IINIT LIST]---");
foreach (object init in inits) 
{
    Person x = init as Person;
    if (x != null)
        x.Show();
    else 
    {
        Link y = (Link)init;
        y.Show();
    }
    Console.WriteLine('-');
}  

Console.WriteLine("Press enter to continue...");
Console.ReadLine();
Console.Clear();

//[7 PART - Cloneable]
//Here I create two Persons and do deep copy
Console.WriteLine("[7 PART - Cloneable]");
Console.WriteLine("---[DEEP COPY]---");
Person person1 = new Person("Danil", 25, "Russia");
Person person2 = (Person)person1.Clone();

Console.WriteLine($"Person1 notes: {person1.lnk.notes}");
Console.WriteLine($"Person2 notes: {person2.lnk.notes}");
person1.lnk.notes = "I changed person1 notes";
Console.WriteLine($"Person1 notes after changes: {person1.lnk.notes}");
Console.WriteLine($"Person2 notes after changes: {person2.lnk.notes}");
Console.WriteLine("As you can see, person changed notes while person1 didn't due to deep Copying\n");

//Here I create two Persons and do shallow copy
Console.WriteLine("---[SHALLOW COPY]---");
Person person3 = new Person("Nastya", 21, "PolskaRepublic");
Person person4 = (Person)person3.ShallowCopy();

Console.WriteLine($"Person3 notes: {person3.lnk.notes}");
Console.WriteLine($"Person4 notes: {person4.lnk.notes}");
person2.lnk.notes = "I changed person3 notes";
Console.WriteLine($"Person3 notes after changes: {person3.lnk.notes}");
Console.WriteLine($"Person4 notes after changes: {person4.lnk.notes}");
Console.WriteLine("As you can see, person3 also changed the note field due to Shallow Copying");

//[ADDITIONAL PART]
Console.WriteLine("[ADDITIONAL PART]");
QueryMaker.InitCompanyArray();

Console.WriteLine("[SHOW QUANTITY OF CLASS OBJECTS]");
QueryMaker.ShowCompanyArray();
QueryMaker.ShowQuantityOfClassObjects();
Console.WriteLine("Press enter to continue...");
Console.ReadLine();
Console.Clear();

Console.WriteLine("[SHOW QUANTITY OF ENGINEERS OF CHOSEN DEPARTMENT]");
QueryMaker.ShowCompanyArray();
QueryMaker.ShowQuantityOfEngineersOfChosenDepartment();
Console.WriteLine("Press enter to continue...");
Console.ReadLine();
Console.Clear();

Console.WriteLine("[CALCULATE TOTAL SALARY BY CLASS OBJECT]");
QueryMaker.ShowCompanyArray();
QueryMaker.CalcTotalSalaryToPayByClassObject();
Console.WriteLine("Press enter to continue...");
Console.ReadLine();
Console.Clear();

Console.WriteLine("Press enter to continue...");
Console.ReadLine();
Console.Clear();