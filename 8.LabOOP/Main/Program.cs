using ClassIerarchyLib;

NewCustomHashTable<string, Person> alpha = new NewCustomHashTable<string, Person>(100);
NewCustomHashTable<string, Person> beta2 = new NewCustomHashTable<string, Person>(100);
alpha.collection_name = "Alpha";
beta2.collection_name = "Beta";

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
beta2.CollectionRefChanged +=
    (sender, args) => journal2.AddEntry(new JournalEntry<Person>(args.collection_name, args.method_name, args.item_link));

//Doing some hashTable activity to fill journals
for(int i = 0; i < 3; i++) 
{
    Person person = new Person();
    person.RandomInit();
    alpha.Add(person.ToString(), person);
}
Person person2 = new Person();
person2.RandomInit();
alpha["QWERTY"] = person2;
alpha.Remove("QWERTY");

//Printing results
Console.WriteLine("<-----JOURNAL ONE (COUNT - HT1 / REF - HT1)----->");
journal1.PrintEntries();
Console.WriteLine("<-----JOURNAL TWO (COUNT - NONE / REF - HT1 & HT2)----->");
journal2.PrintEntries();