using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClassIerarchyLib
{
    public class NewCustomHashTable<TKey, TVal> : CustomHashTable<TKey, TVal>
    {
        //Auto-realizable collection property
        public string collection_name { get; set; }
        //Indexator
        public override TVal this[TKey key]
        {
            get
            {
                return base[key];
            }
            set
            {
                OnCollectionRefChanged(this, new CollectionHandlerEventArgs<TVal>(this.collection_name, $"Indexator[{key}]", value));
                base[key] = value;
            }
        }
        //Delegates & events
        public delegate void CollectionHandler(NewCustomHashTable<TKey, TVal>? sender, CollectionHandlerEventArgs<TVal> args);
        public event CollectionHandler CollectionCountChanged;
        public event CollectionHandler CollectionRefChanged;
        //Event agents
        public void OnCollectionCountChanged(NewCustomHashTable<TKey, TVal>? sender, CollectionHandlerEventArgs<TVal> args) 
        {
            if(CollectionCountChanged != null)
                CollectionCountChanged(sender, args);
        }
        public void OnCollectionRefChanged(NewCustomHashTable<TKey, TVal>? sender, CollectionHandlerEventArgs<TVal> args)
        {
            if (CollectionRefChanged != null)
                CollectionRefChanged(sender, args);
        }
        //Event process-methods
        private void HandleCollectionCountChanged(NewCustomHashTable<TKey, TVal>? sender, CollectionHandlerEventArgs<TVal> args) 
        {
            Console.WriteLine($"Collection: {collection_name} method {args.method_name} called");
        }
        private void HandleCollectionRefChanged(NewCustomHashTable<TKey, TVal>? sender, CollectionHandlerEventArgs<TVal> args)
        {
            Console.WriteLine($"Collection: {collection_name} method {args.method_name} called");
        }
        //Constructor
        public NewCustomHashTable(int capacity) : base(capacity) 
        {
            CollectionCountChanged += HandleCollectionCountChanged;
            CollectionRefChanged += HandleCollectionRefChanged;
        }
        //Methods overrides
        public override void Add(TKey key, TVal sample) 
        {
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs<TVal>(this.collection_name, "Add", sample));
            base.Add(key, sample);
        }
        public override void Add(KeyValuePair<TKey, TVal> sample) 
        {
            //OnCollectionCountChanged(this, new CollectionHandlerEventArgs<TVal>(this.collection_name, "AddPair", sample.Value));
            base.Add(sample);
        }
        public override bool Remove(TKey key) 
        {
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs<TVal>(this.collection_name, $"Remove({key})", base[key]));
            return base.Remove(key);
        }
        public override bool Remove(KeyValuePair<TKey, TVal> pair_sample) 
        {
            //OnCollectionCountChanged(this, new CollectionHandlerEventArgs<TVal>(this.collection_name, "RemovePair", base[pair_sample.Key]));
            return base.Remove(pair_sample);
        }
    }
    public class CollectionHandlerEventArgs<TVal> : EventArgs
    {
        public string collection_name { get; set;}
        public string method_name { get; set;}
        public TVal? item_link { get; set; }

        public CollectionHandlerEventArgs(string collection, string method, TVal sample) 
        {
            collection_name = collection;
            method_name = method;
            item_link = sample;
        }
        public override string ToString()
        {
            string information = "";
            information += $"collection_name:{collection_name}\n";
            information += $"method_name:{method_name}\n";
            if (item_link != null)
                information += $"item-link->type:{item_link.GetType().FullName} |-> value:{item_link}\n";
            else
                information += $"item-link->type:null |-> value:null\n";
            return information;
        }
    }
}
