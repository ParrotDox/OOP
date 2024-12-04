using ClassIerarchyLib;
using System.Security.AccessControl;

List<Person> people = new List<Person> { new Person(), new Person(), new Person() };
foreach (Person person in people)
{ 
    person.RandomInit();
    person.Show();
}
people.Sort();
foreach (Person person in people) 
{
    person.Show();
}
