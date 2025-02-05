using ClassIerarchyLib;



PersonList personList = new PersonList(5);

foreach (Person p in personList) 
{
    Console.WriteLine(p.GetInfo());
}