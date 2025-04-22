using ClassIerarchyLib;
using Main.Service;
using Main.Model;
using Main.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Main.ViewModel
{
    //Classes for observable collections (Used to dynamic UI update)
    public class CollectionItem
    {
        public string Key { get; set; }
        public Person Value { get; set; }
        public CollectionItem(string key, Person value)
        {
            Key = key;
            Value = value;
        }
    }
    public class JournalItem
    {
        public string CollectionName { get; set; }
        public string MethodName { get; set; }
        string SampleKey { get; set; }
        public JournalItem(string collectionName, string methodName, string sampleKey)
        {
            CollectionName = collectionName;
            MethodName = methodName;
            SampleKey = sampleKey;
        }
    }
    public class MainWindowVM
    {
        //Observable Collections for UI ListBoxes
        private ObservableCollection<CollectionItem> _collection;
        private ObservableCollection<JournalItem> _journal;
        public ObservableCollection<CollectionItem> Collection { get { return _collection; } set { _collection = value; } }
        public ObservableCollection<JournalItem> Journal { get { return _journal; } set { _journal = value; } }
        //File module
        private FileService fileService;
        //ICommands
        public ICommand CreateAuto {  get; set; }
        public ICommand LoadCollection { get; set; }
        public MainWindowVM()
        {
            _collection = new ObservableCollection<CollectionItem>();
            _journal = new ObservableCollection<JournalItem>();
            fileService = new FileService();
            CreateAuto = new RelayCommand(CreateAutoCollection, CanCreateAutoCollection);
            LoadCollection = new RelayCommand(LoadCollectionFromFile, CanLoadCollectionFromFile);
        }
        //Get collection and journal data from Service to ObservableCollections
        public void ProcessFileServiceData() 
        {
            //Clear Observable collections to contain new data
            _collection.Clear();
            _journal.Clear();

            //Get collection and journal links
            NewCustomHashTable<string, Person> collectionLink = fileService.Container.Persons;
            Journal journalLink = fileService.Container.Journal;

            //Shallow copy all data from collections to observable collections
            foreach(KeyValuePair<string, Person> pair in collectionLink) 
            {
                CollectionItem unit = new(pair.Key, pair.Value);
                _collection.Add(unit);
            }
            foreach (JournalEntry entry in journalLink.Entries) 
            {
                JournalItem unit = new(entry.CollectionName, entry.MethodName, entry.SampleKey);
                _journal.Add(unit);
            }
        }
        //Create auto collection sized by 10
        public void CreateAutoCollection(object? param) 
        {
            //Create empty collection sized by 10 and empty journal
            NewCustomHashTable<string, Person> collection = new(10);
            Journal journal = new();

            //Create container and subscibe journal method to collection events
            CollectionContainer container = new(collection, journal);

            //Fill Collection with 10 random persons
            for (int i = 0; i < 10; i++)
            {
                string key = Guid.NewGuid().ToString(); //Rnd key
                Person person = new Person();
                person.RandomInit();
                collection.Add(key, person);
            }

            //Connect container to fileService
            fileService.Container = container;

            //Ask user to save container as a file
            if (!fileService.FileSave()) 
            {
                fileService.Container = null;
                MessageBox.Show("Creation has been interrupted!", "Alert", MessageBoxButton.OK);
                return; 
            }

            //Process container data and depict it at UI
            ProcessFileServiceData();
        }
        public bool CanCreateAutoCollection(object? param) 
        {
            return true;
        }
        //Load collection from the file
        public void LoadCollectionFromFile(object? param) 
        {
            if (!fileService.FileOpen()) 
            {
                MessageBox.Show("Creation has been interrupted!", "Alert", MessageBoxButton.OK);
                return;
            }
            ProcessFileServiceData();
        }
        public bool CanLoadCollectionFromFile(object? param) 
        {
            return true;
        }
    }
}
