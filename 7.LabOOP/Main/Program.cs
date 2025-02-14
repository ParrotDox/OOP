using ClassIerarchyLib;
using System.Linq;

void GenerateObjects(int quantity, CustomHashTable<string, Person> table) 
{
    CustomHashTable<string, Person> temp = table;
    for (int i = 0; i < quantity; i++)
    {
        string gen_key = "";
        for (int c = 0; c < 5; c++)
        {
            char[] letters = new char[] { 'a', 'b', 'C', 'Q', 'z' };
            Random rnd = new Random();
            int letter_index = rnd.Next(0, 5);
            gen_key += letters[letter_index];
        }
        Person person = new Person();
        person.RandomInit();
        temp.Add(gen_key, person);
    }
}
CustomHashTable<string, Person> example_hash_table = new CustomHashTable<string, Person>(100);

Console.WriteLine("Generating hashTable by Add(key, sample) method <100 objects>:");
GenerateObjects(100, example_hash_table);
Console.WriteLine("--- --- --- --- --- ---");
Console.WriteLine($"HashTable Count: {example_hash_table.Count}");
Console.WriteLine("Getting key list:");
List<string> keys  = (List<string>)example_hash_table.Keys;
Console.WriteLine($"List<string> keys has {keys.Count} keys");
Console.WriteLine("Getting vals list:");
List<Person> vals = (List<Person>)example_hash_table.Values;
Console.WriteLine($"List<string> vals has {vals.Count} vals");
Console.WriteLine("--- --- --- --- --- ---");
Console.WriteLine("Adding KeyPairValue(customHashCode, customPerson):");
Person customPerson = new Person(); customPerson.RandomInit();
KeyValuePair<string, Person> pair = new KeyValuePair<string, Person>("customHashCode", customPerson);
example_hash_table.Add(pair);
Console.WriteLine($"Is the pair in the hashTable?: {example_hash_table.Contains(pair)}");
Console.WriteLine($"Is the key(customHashCode) in the hashTable?: {example_hash_table.ContainsKey("customHashCode")}");
Console.WriteLine($"Is the value(customPerson) in the hashTable?: {example_hash_table.Contains((Person)customPerson.Clone())}");
Console.WriteLine("--- --- --- --- --- ---");
Console.WriteLine($"Removing by key(customHashCode): {example_hash_table.Remove("customHashCode")} <- Should be true");
Console.WriteLine("Adding pair again...");
example_hash_table.Add(pair);
Console.WriteLine($"Removing by pair(pair): {example_hash_table.Remove(pair)} <- Should be true");
Console.WriteLine($"Does hashTable contains KeyPairValue(customHashCode, customPerson)?: {example_hash_table.Contains(pair)}");
Console.WriteLine("--- --- --- --- --- ---");
Console.WriteLine("Adding KeyPairValue(keyToCopy, personToGet):");
Person personToGet = new Person();
personToGet.RandomInit();
KeyValuePair<string, Person> pair2 = new KeyValuePair<string, Person>("keyToCopy", personToGet);
example_hash_table.Add(pair2);
Console.WriteLine("Person empty gets a sample after TryGetValue method completion:");
Person empty;
Console.WriteLine($"TryGetValue method successful?: {example_hash_table.TryGetValue("keyToCopy", out empty)}");
Console.WriteLine($"Person empty info:\n{empty.GetInfo()}");
Console.WriteLine($"Deleting Person empty...");
Console.WriteLine($"Is removal succeed? {example_hash_table.Remove("keyToCopy")}");
Console.WriteLine("--- --- --- --- --- ---");
Console.WriteLine("Creating arrayEnough<100 cells>, arrayTooShort<50 cells>, arrayNull<null>...");
KeyValuePair<string, Person>[] arrayEnough = new KeyValuePair<string, Person>[100];
KeyValuePair<string, Person>[] arrayTooShort = new KeyValuePair<string, Person>[50];
KeyValuePair<string, Person>[] arrayNull = null;
Console.WriteLine("Calling copyToMethods:");
try 
{
    Console.WriteLine("hashTable CopyTo arrayEnough...");
    example_hash_table.CopyTo(arrayEnough, 0);
    Console.WriteLine("... ... ...");
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine(ex.Message);
}
try
{
    Console.WriteLine("hashTable CopyTo arrayTooShort...");
    example_hash_table.CopyTo(arrayTooShort, 0);
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine("... ... ...");
}
try
{
    Console.WriteLine("hashTable CopyTo arrayNull...");
    example_hash_table.CopyTo(arrayNull, 0);
}
catch (ArgumentNullException ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine("... ... ...");
}
Console.WriteLine("--- --- --- --- --- ---");
Console.WriteLine("Calling clear method...");
example_hash_table.Clear();
Console.WriteLine($"HashTable Count: {example_hash_table.Count}");
Console.WriteLine("--- --- --- --- --- ---");
Console.WriteLine("Generating hashTable by Add(key, sample) method <10 objects>:");
GenerateObjects(10, example_hash_table);
Console.WriteLine("Using foreach using custom iterator:");
foreach(KeyValuePair<string, Person> kvp  in example_hash_table) 
{
    Console.WriteLine(kvp.Value.GetInfo());
}
Console.WriteLine("--- --- --- --- --- ---");
Console.WriteLine("Creating new hashTable of size 1 and filling it with 10 elements:");
CustomHashTable<string, Person> new_hash_table = new CustomHashTable<string, Person>(1);
GenerateObjects(10, new_hash_table);
Console.WriteLine($"new hashTable count: {new_hash_table.Count}");
foreach (KeyValuePair<string, Person> kvp in new_hash_table)
{
    Console.WriteLine(kvp.Value.GetInfo());
}