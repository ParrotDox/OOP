using ClassIerarchyLib;
using Main.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.ViewModel
{
    public class MainWindowVM
    {
        //Nested classes for observable collections (Used to dynamic UI update)
        private class CollectionItem 
        {
            public string Key { get; set; }
            public Person Value { get; set; }
            public CollectionItem(string key, Person value)
            {
                Key = key;
                Value = value;
            }
        }
        private class JournalItem 
        {
            public string CollectionName { get; set; }
            public string MethodName { get; set; }
            Person ItemLink { get; set; }
            public JournalItem(string collectionName, string methodName, Person itemLink)
            {
                CollectionName = collectionName;
                MethodName = methodName;
                ItemLink = itemLink;
            }
        }
        //Observable Collections for UI ListBoxes
        ObservableCollection<CollectionItem> _collection;
        ObservableCollection<JournalItem> _journal;
        ObservableCollection<CollectionItem> Collection { get { return _collection; } set { _collection = value; } }
        ObservableCollection<JournalItem> Journal { get { return _journal; } set { _journal = value; } }
        //File module
        private FileService fileService = new FileService();
        public MainWindowVM()
        {

        }
        //Get collection and journal data from Service to ObservableCollections
        public void ProcessFileServiceData() 
        {
            NewCustomHashTable<string, Person> collectionLink = fileService.Container.Persons;
            Journal<Person> journalLink = fileService.Container.Journal;
            foreach(KeyValuePair<string, Person> pair in collectionLink) 
            {
                CollectionItem unit = new(pair.Key, pair.Value);
                _collection.Add(unit);
            }
            foreach (JournalEntry<Person> entry in journalLink.Entries) 
            {
                JournalItem unit = new(entry.collection_name, entry.method_name, entry.item_link);
                _journal.Add(unit);
            }
        }
    }
}
