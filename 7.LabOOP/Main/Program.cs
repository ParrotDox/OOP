using ClassIerarchyLib;



PersonHashTable myHashTable = new PersonHashTable(1000);
Person p = new Person();    //In HashTable
Person notForContained = new Person();  //Not in HashTable
KeyValuePair<string, Person> pair = new KeyValuePair<string, Person>("KeyThree", p);  //In HashTable
p.RandomInit();

Console.WriteLine("____Add____");
myHashTable.Add("KeyOne", p);
myHashTable.Add(p.ToString(), p);
myHashTable.Add(pair);
Console.WriteLine("____Count____");
Console.WriteLine(myHashTable.Count);
Console.WriteLine("____Keys____");
Console.WriteLine(myHashTable.ContainsKey("KeyOne"));
Console.WriteLine(myHashTable.ContainsKey(p.ToString()));
Console.WriteLine(myHashTable.ContainsKey("KeyThree"));
Console.WriteLine("____Contains____");
Console.WriteLine(myHashTable.Contains(p));
Console.WriteLine(myHashTable.Contains(notForContained) + " (must be false)");
Console.WriteLine(myHashTable.Contains(pair));
Console.WriteLine("____Try_Get_Value____");
Person toOut;
Console.WriteLine(myHashTable.TryGetValue("KeyOne", out toOut));
Console.WriteLine(toOut.GetInfo());
Console.WriteLine(myHashTable.TryGetValue(p.ToString(), out toOut));
Console.WriteLine(toOut.GetInfo());
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
