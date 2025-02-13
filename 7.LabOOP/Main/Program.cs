using ClassIerarchyLib;



PersonHashTable myHashTable = new PersonHashTable(1000);
Person p = new Person();    //In HashTable
Person p2 = new Person();
Person notForContained = new Person();  //Not in HashTable
KeyValuePair<string, Person> pair = new KeyValuePair<string, Person>("KeyThree", p);  //In HashTable
p.RandomInit();
p2.RandomInit();

Console.WriteLine("____Add____");
myHashTable.Add("KeyOne", p);
myHashTable.Add(p.ToString(), p);
myHashTable.Add(pair);
Console.WriteLine("____Count____");
Console.WriteLine(myHashTable.Count);
Console.WriteLine("____ContainsKey____");
Console.WriteLine(myHashTable.ContainsKey("KeyOne"));
Console.WriteLine(myHashTable.ContainsKey(p.ToString()));
Console.WriteLine(myHashTable.ContainsKey("KeyThree"));
Console.WriteLine("____Contains____");
Console.WriteLine(myHashTable.Contains(p));
Console.WriteLine(myHashTable.Contains(notForContained) + " (must be false)");
Console.WriteLine(myHashTable.Contains(pair));
Console.WriteLine("____Try_Get_Value____");
Person toOut;
Console.WriteLine("TryGetValue(string val, out)");
Console.WriteLine(myHashTable.TryGetValue("KeyOne", out toOut));
Console.WriteLine(toOut.GetInfo());
Console.WriteLine("TryGetValue(Person val.ToString(), out)");
Console.WriteLine(myHashTable.TryGetValue(p.ToString(), out toOut));
Console.WriteLine(toOut.GetInfo());
Console.WriteLine("TryGetValue(string val, out)");
Console.WriteLine(myHashTable.TryGetValue("KeyThree", out toOut));
Console.WriteLine(toOut.GetInfo());
Console.WriteLine("____Remove____");
myHashTable.Remove("KeyOne");
myHashTable.Remove(p.ToString());
myHashTable.Remove("KeyThree");
myHashTable.Remove("NotAKey");
Console.WriteLine("____Contains(After_Remove)____");
Console.WriteLine(myHashTable.ContainsKey("KeyOne"));
Console.WriteLine(myHashTable.ContainsKey("KeyTwo"));
Console.WriteLine(myHashTable.ContainsKey("KeyThree"));
Console.WriteLine("____Count(After_Remove)____");
Console.WriteLine(myHashTable.Count);
Console.WriteLine("____Add(Another)____");
myHashTable.Add("KeyOne", p);
myHashTable.Add(p.ToString(), p);
myHashTable.Add(pair);
Console.WriteLine("____Count(After_Add)____");
Console.WriteLine(myHashTable.Count);
Console.WriteLine("____Clear____");
myHashTable.Clear();
Console.WriteLine("____Count(After_Clear)____");
Console.WriteLine(myHashTable.Count);
Console.WriteLine("____Add(Another)____");
myHashTable.Add("KeyOne", p);
myHashTable.Add(p.ToString(), p);
myHashTable.Add(pair);
myHashTable.Add("GGGBBB", p2);
Console.WriteLine("____CopyTo____");
KeyValuePair<string, Person>[] key_array = new KeyValuePair<string, Person>[6];
myHashTable.CopyTo(key_array, 0);
Console.WriteLine("____Foreach_key_array____");
foreach(KeyValuePair<string, Person> key in key_array) 
{
    if(key.Value == null) 
    {
        Console.WriteLine("None");
    }
    else
    {
        Console.WriteLine(key.Value.Name);
    }
}
Console.WriteLine("____Foreach_HASHTABLE____");
foreach(KeyValuePair<string, Person> point in myHashTable) 
{
    Console.WriteLine(point.Value.Name);
}
Console.WriteLine("____Values____");
List<Person> list_val;
list_val = (List<Person>)myHashTable.Values;
foreach(Person per in list_val) 
{
    Console.WriteLine(per.GetInfo());
}
Console.WriteLine("____Keys____");
List<string> list_key;
list_key = (List<string>)myHashTable.Keys;
foreach (string k in list_key)
{
    Console.WriteLine(k);
}

Console.WriteLine("****PersonList part****");
Console.WriteLine("****Creating and count****");
PersonList myList = new PersonList();
Console.WriteLine(myList.Count);
Console.WriteLine("****Init 5 elements****");
Person p3;
for(int i = 0; i < 5; i++) 
{
    Person temp = new Person();
    temp.RandomInit();
    myList.Add(temp);

    p = (Person)temp.Clone();
}
Console.WriteLine("****Show 5 elements****");
foreach (Person person in myList) 
{
    Console.WriteLine(person.GetInfo());
}
Console.WriteLine("****Contains element****");
Console.WriteLine($"Does contain element: {myList.Contains(p)}");
Console.WriteLine("****Counting before removal****");
Console.WriteLine(myList.Count);
Console.WriteLine("****Remove element****");
myList.Remove(p);
Console.WriteLine($"Does contain element: {myList.Contains(p)}");
Console.WriteLine("****Counting after removal****");
Console.WriteLine(myList.Count);
Console.WriteLine("****CopyTo elements****");
Person[] prsArr = new Person[5];
myList.CopyTo(prsArr, 0);
Console.WriteLine("****Printing Array****");
foreach (Person prs in prsArr) 
{
    if(prs == null) 
    {
        Console.WriteLine("\nNULL\n"); continue;
    }
    Console.WriteLine(prs.GetInfo());
}
Console.WriteLine("****Clear list****");
myList.Clear();
Console.WriteLine("****Counting after clear****");
Console.WriteLine(myList.Count);