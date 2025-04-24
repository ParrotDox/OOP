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
using System.Collections.Specialized;
using Main.View;
using System.ComponentModel;

namespace Main.ViewModel
{
    public class MainWindowVM
    {
        //Observable Collections for UI ListBoxes
        public static ObservableCollection<Person> Collection { get; set; }
        public static ObservableCollection<JournalEntry> Journal { get; set; }
        public CollectionContainer? Container { get; set; }

        //ICommands
        public ICommand SaveFileCommand { get; set; }
        public ICommand LoadFileCommand { get; set; }
        public ICommand CreateEmptyCollectionCommand { get; set; }
        public ICommand CreateAutoCollectionCommand { get; set; }
        public MainWindowVM()
        {
            Collection = new ObservableCollection<Person>();
            Journal = new ObservableCollection<JournalEntry>();
            Container = null;

            SaveFileCommand = new RelayCommand(SaveIntoFile, CanSaveIntoFile);
            LoadFileCommand = new RelayCommand(LoadFile, CanLoadFile);
            CreateEmptyCollectionCommand = new RelayCommand(CreateEmptyCollection, CanCreateEmptyCollection);
            CreateAutoCollectionCommand = new RelayCommand(CreateAutoCollection, CanCreateAutoCollection);
        }
        //Save
        public void SaveIntoFile(object? param) 
        {
            if (Container != null) 
            {
                FileService.SaveIntoFile(Container.Collection, Container.Journal);
            }
            else 
            {
                MessageBox.Show("Container is null.\nNothing to save");
            }
        }
        public bool CanSaveIntoFile(object? param)
        {
            return true;
        }
        //Load
        public void LoadFile(object? param)
        {
            if (Container != null)
            {
                MessageBoxResult option = MessageBox.Show("Container isn't empty.\nDo you want to erase all unsaved data?", "Warning", MessageBoxButton.YesNo);
                if (option == MessageBoxResult.Yes)
                {
                    CollectionContainer? gotResult = CreateFileBasedContainer();
                    if (gotResult != null) 
                    {
                        Container = gotResult;
                        UpdateData();
                    }
                    return;
                }
                else 
                {
                    return;
                }
            }
            else
            {
                CollectionContainer? gotResult = CreateFileBasedContainer();
                if (gotResult != null)
                {
                    Container = gotResult;
                    UpdateData();
                }
                return;
            }
        }
        public bool CanLoadFile(object? param)
        {
            return true;
        }
        //CreateEmptyCollection
        public void CreateEmptyCollection(object? param)
        {
            if (Container == null)
            {
                Container = new CollectionContainer();
                Container.Collection.CollectionName = "Empty Collection";
                UpdateData();
            }
            else
            {
                MessageBoxResult option = MessageBox.Show("Container isn't empty.\nDo you want to erase all unsaved data?", "Warning", MessageBoxButton.YesNo);
                if (option == MessageBoxResult.Yes)
                {
                    Container = null;
                    CreateEmptyCollection(param);
                }
                else { return; }
            }
        }
        public bool CanCreateEmptyCollection(object? param)
        {
            return true;
        }
        //CreateAutoCollection
        public void CreateAutoCollection(object? param)
        {
            if(Container == null) 
            {
                Container = new CollectionContainer();
                Container.Collection.CollectionName = "Random Collection";
                for (int i = 0; i < 10; i++) 
                {
                    Person randomPerson = ReturnRandomPerson();
                    Container.AddUnit(randomPerson.Key, randomPerson);
                }
                UpdateData();
            }
            else 
            {
                MessageBoxResult option = MessageBox.Show("Container isn't empty.\nDo you want to erase all unsaved data?", "Warning",MessageBoxButton.YesNo);
                if (option == MessageBoxResult.Yes) 
                {
                    Container = null;
                    CreateAutoCollection(param);
                }
                else { return; }
            }
        }
        public bool CanCreateAutoCollection(object? param)
        {
            return true;
        }

        private Person ReturnRandomPerson() 
        {
            Random rnd = new Random();
            Person blank;
            switch (rnd.Next(0, 4))
            {
                case 0:
                    {
                        blank = new Person();
                        blank.RandomInit();
                        break;
                    }
                case 1:
                    {
                        blank = new Employee();
                        blank.RandomInit();
                        break;
                    }
                case 2:
                    {
                        blank = new Engineer();
                        blank.RandomInit();
                        break;
                    }
                case 3:
                    {
                        blank = new Admin();
                        blank.RandomInit();
                        break;
                    }
                default: 
                    {
                        blank = new Person();
                        blank.RandomInit();
                        break;
                    }
            }
            return blank;
        }
        //Used to add equal data to every collection and container. Takes data from the APP!
        private void UpdateData() 
        {
            if (Container == null) 
            {
                MessageBox.Show("Can't update data: container is null");
                return;
            }
            else 
            {
                Collection.Clear();
                Journal.Clear();
                foreach (KeyValuePair<string, Person> pair in Container.Collection)
                {
                    Collection.Add(pair.Value);
                }
                foreach (JournalEntry entry in Container.Journal.Entries)
                {
                    Journal.Add(entry);
                }
            }
        }
        //Used to create container based on file data
        private CollectionContainer? CreateFileBasedContainer() 
        {
            NewCustomHashTable<string, Person>? collection = new();
            Journal? journal = new();
            FileService.LoadFile(out collection, out journal);
            if(collection == null || journal == null) 
            {
                MessageBox.Show("Failed to create collection container.\nLoadFile returned null");
                return null;
            }
            CollectionContainer container = new(collection, journal);
            return container;
        }
    }
}
