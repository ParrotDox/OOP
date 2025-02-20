using ClassIerarchyLib;

NewCustomHashTable<string, Person> alpha = new NewCustomHashTable<string, Person>(100);
NewCustomHashTable<string, Person> beta = new NewCustomHashTable<string, Person>(100);
alpha.collection_name = "Alpha";
beta.collection_name = "Beta";

//Subscribing journal1 on countEvent and RefEvent
Journal<Person> journal1 = new Journal<Person>();
alpha.CollectionCountChanged +=
    (sender, args) => journal1.AddEntry(new JournalEntry<Person>(args.collection_name, args.method_name, args.item_link));
alpha.CollectionRefChanged +=
    (sender, args) => journal1.AddEntry(new JournalEntry<Person>(args.collection_name, args.method_name, args.item_link));

//Subscribing journal2 on RefEvent
Journal<Person> journal2 = new Journal<Person>();
alpha.CollectionRefChanged +=
    (sender, args) => journal2.AddEntry(new JournalEntry<Person>(args.collection_name, args.method_name, args.item_link));
beta.CollectionRefChanged +=
    (sender, args) => journal2.AddEntry(new JournalEntry<Person>(args.collection_name, args.method_name, args.item_link));

//Doing some hashTable activity
//Filling alpha table with 3 samples
for(int i = 0; i < 3; i++) 
{
    Person person = new Person();
    person.RandomInit();
    alpha.Add(person.ToString(), person);
}
//Initialize alpha table cell at index "QWERTY"
Person person2 = new Person();
person2.RandomInit();
alpha["QWERTY"] = person2;
//Removing value at table cell at index "QWERTY"
alpha.Remove("QWERTY");
//Removing value at table cell at index "QWERTY" (Cell is empty)
alpha.Remove("QWERTY");
alpha["QWERTY"] = null;
//Filling beta table with 3 samples
for (int i = 0; i < 3; i++)
{
    Person person = new Person();
    person.RandomInit();
    beta.Add(person.ToString(), person);
}
//Initialize beta table cell at index "MyKEY"
Person person3 = new Person();
person3.RandomInit();
beta["MyKEY"] = person3;
//Changing beta table cell at index "MyKEY"
Person person4 = new Person();
person4.RandomInit();
beta["MyKEY"] = person4;
//Printing results
Console.WriteLine("<-----JOURNAL ONE (COUNT - HT1 / REF - HT1)----->");
journal1.PrintEntries();
Console.WriteLine("<-----JOURNAL TWO (COUNT - NONE / REF - HT1 & HT2)----->");
journal2.PrintEntries();