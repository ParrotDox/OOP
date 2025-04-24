using ClassIerarchyLib;
using Main.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Model
{
    public class CollectionContainer
    {
        public NewCustomHashTable<string, Person> Collection { get; set; }
        public Journal? Journal { get; set; }
        //Ctor for new collection creation
        public CollectionContainer()
        {
            Collection = new(100);
            Journal = new Journal();
            Collection.CollectionCountChanged += (sender, args) =>
            { Journal.AddEntry(new JournalEntry(args.collection_name, args.method_name, ((Person)(args.item_link)).Key)); };
            Collection.CollectionRefChanged += (sender, args) =>
            { Journal.AddEntry(new JournalEntry(args.collection_name, args.method_name, ((Person)(args.item_link)).Key)); };
        }
        public CollectionContainer(int size)
        {
            Collection = new NewCustomHashTable<string, Person>(size);
            Journal = new Journal();
            Collection.CollectionCountChanged += (sender, args) =>
            { Journal.AddEntry(new JournalEntry(args.collection_name, args.method_name, ((Person)(args.item_link)).Key)); };
            Collection.CollectionRefChanged += (sender, args) =>
            { Journal.AddEntry(new JournalEntry(args.collection_name, args.method_name, ((Person)(args.item_link)).Key)); };
        }
        //Ctor for file open
        public CollectionContainer(NewCustomHashTable<string, Person> collection, Journal journal)
        {
            this.Collection = collection;
            Journal = journal;
            this.Collection.CollectionCountChanged += (sender, args) =>
            { Journal.AddEntry(new JournalEntry(args.collection_name, args.method_name, ((Person)(args.item_link)).Key)); };
            this.Collection.CollectionRefChanged += (sender, args) =>
            { Journal.AddEntry(new JournalEntry(args.collection_name, args.method_name, ((Person)(args.item_link)).Key)); };
        }
        public void AddUnit(string key, Person unit) 
        {
            Collection.Add(key, unit);
        }
    }
}
