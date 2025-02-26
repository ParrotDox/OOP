using ClassIerarchyLib;
using System;

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
var let_info_ = factoryQueue.
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

//CUSTOM LINQ METHODS
var custom_where = factoryQueue.SelectMany(workshop => workshop).WhereCustom(person => person is Employee emp && emp.Experience > 10);
var custom_aggregate = factoryQueue.SelectMany(workshop => workshop).Where(person => person is Employee).AggregateCustom(emp => ((Employee)emp).Salary, (accumulator, val) => accumulator + val);
var custor_order_by = factoryQueue.SelectMany(workshop => workshop).Where(person => person is Employee).OrderByCustom(emp => ((Employee)emp).Age);

//Results
Console.WriteLine("Experienced Workers:");
foreach (var worker in experienced_workers)
    Console.WriteLine(worker.Name);

Console.WriteLine("\nExperienced Workers (Extensions):");
foreach (var worker in experienced_workers_with_extensions)
    Console.WriteLine(worker.Name);

Console.WriteLine("\nSame Employees (Union):");
foreach (var emp in same_employees)
    Console.WriteLine(emp.Name);

Console.WriteLine("\nSame Employees (Except):");
foreach (var emp in same_employees_with_extensions)
    Console.WriteLine(emp.Name);

Console.WriteLine($"\nTotal Salary: {salary_summary}");
Console.WriteLine($"Total Salary (Extensions): {salary_summary_with_extension}");

Console.WriteLine("\nGroup by Residence:");
foreach (var group in group_by_residence)
{
    Console.WriteLine($"Residence: {group.Key}");
    foreach (var person in group)
        Console.WriteLine($"  {person.Name}");
}

Console.WriteLine("\nGroup by Residence (Extensions):");
foreach (var group in group_by_residence_with_extension)
{
    Console.WriteLine($"Residence: {group.Key}");
    foreach (var person in group)
        Console.WriteLine($"  {person.Name}");
}

Console.WriteLine("\nPeople Info (Let):");
foreach (var info in let_info)
    Console.WriteLine(info.info);

Console.WriteLine("\nPeople Info (Let, Extensions):");
foreach (var info in let_info_)
    Console.WriteLine(info.info);

Console.WriteLine("\nJoined by Name:");
foreach (var item in join_by_name)
    Console.WriteLine($"Name: {item.Name}, Residence: {item.Residence}");

Console.WriteLine("\nJoined by Name (Extensions):");
foreach (var item in join_by_name_with_extension)
    Console.WriteLine($"Name: {item.Name}, Residence: {item.Residence}");

Console.WriteLine("\nCustom Where (Experienced Workers):");
foreach (var worker in custom_where)
    Console.WriteLine(worker.Name);

Console.WriteLine($"\nCustom Aggregate (Total Salary): {custom_aggregate}");

Console.WriteLine("\nCustom Order By (Employees Sorted by Age):");
foreach (var emp in custor_order_by)
    Console.WriteLine($"{emp.Name} - {((Employee)emp).Age} years");