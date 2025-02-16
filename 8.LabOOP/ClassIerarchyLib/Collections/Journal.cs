using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIerarchyLib
{
    public class Journal<TVal>
    {
        private List<JournalEntry<TVal>> _entries;
        public Journal() 
        {
            _entries = new List<JournalEntry<TVal>>();
        }
        public void AddEntry(JournalEntry<TVal> entry) 
        {
            _entries.Add(entry);
        }
        public void PrintEntries() 
        {
            foreach(JournalEntry<TVal> entry in _entries) 
            {
                Console.WriteLine(entry.ToString());
            }
        }
    }
    public class JournalEntry<TVal>
    {
        public string collection_name {  get; set; }
        public string method_name { get; set; }
        public TVal item_link { get; set; }
        public JournalEntry(string collection, string method, TVal sample)
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
