using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIerarchyLib
{
    [Serializable]
    public class Journal
    {
        private List<JournalEntry> _entries;
        public List<JournalEntry> Entries { get { return _entries; } set { _entries = value; } }
        public Journal() 
        {
            _entries = new List<JournalEntry>();
        }
        public void AddEntry(JournalEntry entry) 
        {
            _entries.Add(entry);
        }
    }
    [Serializable]
    public class JournalEntry
    {
        public string CollectionName {  get; set; }
        public string MethodName { get; set; }
        public string SampleKey { get; set; }
        public JournalEntry()
        {
            CollectionName = "";
            MethodName = "";
        }
        public JournalEntry(string collection, string method, string sampleKey)
        {
            CollectionName = collection;
            MethodName = method;
            SampleKey = sampleKey;
        }
        public override string ToString()
        {
            return $"{CollectionName},{MethodName},{SampleKey}";
        }
    }
}
