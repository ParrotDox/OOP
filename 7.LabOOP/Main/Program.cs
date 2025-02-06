using ClassIerarchyLib;



PersonList pl =  new PersonList(5);
Person a = new Person();
a.RandomInit();
Person[] mas = new Person[10];
for (int i = 0; i < mas.Length; i++) 
{
    mas[i] = (Person)(a.Clone());
    Console.WriteLine(mas[i].GetInfo());
}

Console.WriteLine("\n\n-------------------\n\n");
pl.CopyTo(mas, 3);

foreach (Person p in mas) 
{
    Console.WriteLine(p.GetInfo());
}