using ClassIerarchyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Model
{
    public class CollectionContainer
    {
        private NewCustomHashTable<string, Person>? persons;
        private Journal<Person>? journal;
        public NewCustomHashTable<string, Person>? Persons { get { return persons; } set { persons = value; } }
        public Journal<Person>? Journal { get { return journal; } set { journal = value; } }
        public CollectionContainer()
        {
            Persons = null;
            Journal = null;
            Persons.CollectionCountChanged += (sender, args) =>
            { Journal.AddEntry(new JournalEntry<Person>(args.collection_name, args.method_name, args.item_link)); };
            Persons.CollectionRefChanged += (sender, args) =>
            { Journal.AddEntry(new JournalEntry<Person>(args.collection_name, args.method_name, args.item_link)); };
        }
        public CollectionContainer(int size)
        {
            Persons = new NewCustomHashTable<string, Person>(size);
            Journal = new Journal<Person>();
            Persons.CollectionCountChanged += (sender, args) =>
            { Journal.AddEntry(new JournalEntry<Person>(args.collection_name, args.method_name, args.item_link)); };
            Persons.CollectionRefChanged += (sender, args) =>
            { Journal.AddEntry(new JournalEntry<Person>(args.collection_name, args.method_name, args.item_link)); };
        }
        public CollectionContainer(NewCustomHashTable<string, Person> personsSample, Journal<Person> journalSample)
        {
            Persons = personsSample;
            Journal = journalSample;
            Persons.CollectionCountChanged += (sender, args) =>
            { Journal.AddEntry(new JournalEntry<Person>(args.collection_name, args.method_name, args.item_link)); };
            Persons.CollectionRefChanged += (sender, args) =>
            { Journal.AddEntry(new JournalEntry<Person>(args.collection_name, args.method_name, args.item_link)); };
        }
    }
}
