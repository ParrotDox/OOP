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
        private Journal? journal;
        public NewCustomHashTable<string, Person>? Persons { get { return persons; } set { persons = value; } }
        public Journal? Journal { get { return journal; } set { journal = value; } }
        public CollectionContainer()
        {
            Persons = null;
            Journal = null;
        }
        public CollectionContainer(int size)
        {
            Persons = new NewCustomHashTable<string, Person>(size);
            Journal = new Journal();
            Persons.CollectionCountChanged += (sender, args) =>
            { Journal.AddEntry(new JournalEntry(args.collection_name, args.method_name, ((Person)(args.item_link)).Key)); };
            Persons.CollectionRefChanged += (sender, args) =>
            { Journal.AddEntry(new JournalEntry(args.collection_name, args.method_name, ((Person)(args.item_link)).Key)); };
        }
        public CollectionContainer(NewCustomHashTable<string, Person> personsSample, Journal journalSample)
        {
            Persons = personsSample;
            Journal = journalSample;
            Persons.CollectionCountChanged += (sender, args) =>
            { Journal.AddEntry(new JournalEntry(args.collection_name, args.method_name, ((Person)(args.item_link)).Key)); };
            Persons.CollectionRefChanged += (sender, args) =>
            { Journal.AddEntry(new JournalEntry(args.collection_name, args.method_name, ((Person)(args.item_link)).Key)); };
        }
    }
}
