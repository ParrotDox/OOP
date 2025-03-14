using ClassIerarchyLib;
using System;
using System.Diagnostics;

Queue<List<Person>> factoryQueue = new Queue<List<Person>>();

List<Person> workshop1 = new List<Person>()
{
    new Person("Kamel", 28, "Russia"),
    new Employee("Lamel", 30, "New_York", 1001440, 6, 35000),
    new Employee("Mars", 32, "Spain", 1401004, 12, 50000),
    new Employee("Max", 25, "Sweden", 2223221, 6, 45000),
    new Admin("Karoline", 38, "America", 9905632, 20, 80000, "American District"),
    new Engineer("Pit", 33, "England", 3342109, 12, 75000, "IT tech department"),
    new Engineer("Cromwell", 55, "France", 7379901, 22, 90000, "IT tech department")
};
List<Person> workshop2 = new List<Person>()
{
    new Person("Mark", 48, "India"),
    new Employee("Maddie", 25, "Britain", 7008420, 5, 25000),
    new Employee("Jim", 35, "Spain", 1441014, 10, 40000),
    new Employee("Churchill", 55, "Britain", 2331125, 10, 85000),
    new Admin("Retsuko", 25, "Japan", 5605452, 6, 60000, "Japan District"),
    new Engineer("Morell", 31, "Brazil", 5542109, 7, 55000, "Car tech department"),
    new Engineer("Cromwell", 55, "France", 7379901, 22, 90000, "IT tech department"),
    new Employee("Kamel", 28, "Russia", 1008895, 10, 35000)
};

factoryQueue.Enqueue(workshop1);
factoryQueue.Enqueue(workshop2);

//Five queries
//a)
//SELECT Employees that work more than 5 years
var experienced_workers = from workshop in factoryQueue
                          from person in workshop
                          where person is Employee && ((Employee)person).Experience > 10
                          select person;
//SELECT -> extension method
var experienced_workers_with_extensions = factoryQueue
    .SelectMany(workshop => workshop)
    .Where(person => person is Employee emp && emp.Experience > 10);
//b)
//UNION Employees that are the same
var same_employees = (from workshop in factoryQueue
                     select workshop).First().
                     Union((from workshop in factoryQueue select workshop).Last());
//UNION -> extension
var same_employees_with_extensions = factoryQueue.First().Except(factoryQueue.Last());
//c)
//Agregation SUM salaries
var salary_summary = (from workshop in factoryQueue
                      from person in workshop
                      where person is Employee
                      select ((Employee)person).Salary).Sum();
//Agregation SUM -> extension
var salary_summary_with_extension = factoryQueue
    .SelectMany(workshop => workshop)
    .Where(person => person is Employee emp)
    .Sum(emp => ((Employee)emp).Salary);
//d)
//GroupBy residence
var group_by_residence = from workshop in factoryQueue
                         from person in workshop
                         group person by person.Residence;
//GroupBy -> extension
var group_by_residence_with_extension = factoryQueue
    .SelectMany(workshop => workshop).GroupBy(person => person.Residence);
//e)
//Let
var let_info = from workshop in factoryQueue
               from person in workshop
               let info = $"{person.Name} lives in {person.Residence}"
               select new { person, info };
//Let with extension
var let_info_with_extension = factoryQueue.
    SelectMany(workshop => workshop)
    .Select(person => new { person.Name, info = $"{person.Name} lives in {person.Residence}" });
//f)
//Join Person ~ Employee by name (Find persons which names are the same like in the list)
List<string> names_for_join = new List<string> { "Mark", "Retsuko" };
var join_by_name = from workshop in factoryQueue
                   from person in workshop
                   join name in names_for_join on person.Name equals name
                   select new { Name = name, Residence = person.Residence };
//Join -> extension
var join_by_name_with_extension = factoryQueue
    .SelectMany(workshop => workshop)
    .Join(names_for_join, person => person.Name, name => name, (person, name) => new { Name = name, Residence = person.Residence });

//Measuring time of no-extension and extension methods
Stopwatch timer_no_extension = new();
Stopwatch timer_extension = new();

List<Person> tempList = factoryQueue.SelectMany(workshop => workshop).ToList();
int n = tempList.Count();

var query2 = factoryQueue.
    SelectMany(workshop => workshop).
    Where(person => person.Age > 20);

timer_no_extension.Start();
for (int i = 0; i < n; ++i) 
{
    Person person = tempList[i];
    if(person.Age > 20) 
    {
        
    }
}
timer_no_extension.Stop();

timer_extension.Start();
foreach (var item in query2) 
{
    
}
timer_extension.Stop();
//CUSTOM LINQ METHODS
NewCustomHashTable<string, Person> customTable = new(100);
for (int i = 0; i < 100; i++) 
{
    Person temp = new();
    temp.RandomInit();
    customTable.Add(temp.ToString(), temp);
}
var filter_by_age = customTable.WhereCustom(pair => pair.Value.Age > 30);
double totalSalary = customTable.AggregateCustom(pair => pair.Value.Age, (sum, x) => sum + x);
var filter_by_ascending_age = customTable.OrderByCustom(pair => pair.Value.Age);


//Results
// a) Experienced workers (experience > 10 years)
Console.WriteLine("Experienced Workers:");
foreach (var worker in experienced_workers)
{
    Console.WriteLine($"{worker.Name}, {((Employee)worker).Experience} years of experience");
}
Console.WriteLine(new string('-', 40));
Console.WriteLine("Experienced Workers EXTENSION:");
foreach (var worker in experienced_workers_with_extensions)
{
    Console.WriteLine($"{worker.Name}, {((Employee)worker).Experience} years of experience");
}
Console.WriteLine(new string('-', 40));

// b) Find same employees in both workshops
Console.WriteLine("Same Employees in Both Workshops:");
foreach (var employee in same_employees)
{
    Console.WriteLine($"{employee.Name}, {employee.Residence}");
}
Console.WriteLine(new string('-', 40));
Console.WriteLine("Same Employees in Both Workshops EXTENSION:");
foreach (var employee in same_employees_with_extensions)
{
    Console.WriteLine($"{employee.Name}, {employee.Residence}");
}
Console.WriteLine(new string('-', 40));

// c) Salary summary
Console.WriteLine($"Total Salary of All Employees: {salary_summary}");
Console.WriteLine(new string('-', 40));
Console.WriteLine($"Total Salary of All Employees EXTENSION: {salary_summary_with_extension}");
Console.WriteLine(new string('-', 40));

// d) Group by residence
Console.WriteLine("Group by Residence:");
foreach (var group in group_by_residence)
{
    Console.WriteLine($"Residence: {group.Key}");
    foreach (var person in group)
    {
        Console.WriteLine($" - {person.Name}");
    }
}
Console.WriteLine(new string('-', 40));
Console.WriteLine("Group by Residence EXTENSION:");
foreach (var group in group_by_residence_with_extension)
{
    Console.WriteLine($"Residence: {group.Key}");
    foreach (var person in group)
    {
        Console.WriteLine($" - {person.Name}");
    }
}
Console.WriteLine(new string('-', 40));

// e) Using let (creating info objects based on employees)
Console.WriteLine("Person Info:");
foreach (var item in let_info)
{
    Console.WriteLine(item.info);
}
Console.WriteLine(new string('-', 40));
Console.WriteLine("Person Info EXTENSION:");
foreach (var item in let_info_with_extension)
{
    Console.WriteLine(item.info);
}
Console.WriteLine(new string('-', 40));

// f) Join employee with same names -> create new object {Name = first_emp.name, Residence = second_emp.residence}
Console.WriteLine("People with Matching Names:");
foreach (var person in join_by_name)
{
    Console.WriteLine($"Name: {person.Name}, Residence: {person.Residence}");
}
Console.WriteLine(new string('-', 40));
Console.WriteLine("People with Matching Names EXTENSION:");
foreach (var person in join_by_name_with_extension)
{
    Console.WriteLine($"Name: {person.Name}, Residence: {person.Residence}");
}
Console.WriteLine(new string('-', 40));

// CUSTOM LINQ METHODS
Console.WriteLine("Filtered by Age > 30:");
foreach (var pair in filter_by_age)
{
    Console.WriteLine($"{pair.Key}: {pair.Value.Name}, Age: {pair.Value.Age}");
}
Console.WriteLine(new string('-', 40));

Console.WriteLine($"Total Sum of Ages: {totalSalary}");
Console.WriteLine(new string('-', 40));

Console.WriteLine("Sorted by Age in Ascending Order:");
foreach (var pair in filter_by_ascending_age)
{
    Console.WriteLine($"{pair.Key}: {pair.Value.Name}, Age: {pair.Value.Age}");
}
Console.WriteLine(new string('-', 40));

Console.WriteLine("Elapsed ticks ( 1 - no_extension | 2 - extension):");
Console.WriteLine($"Elapsed ticks: {timer_no_extension.Elapsed.Ticks}");
Console.WriteLine($"Elapsed ticks: {timer_extension.Elapsed.Ticks}");