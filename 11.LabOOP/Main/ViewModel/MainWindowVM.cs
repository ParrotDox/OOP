using ClassIerarchyLib;
using Main.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Collections.Specialized;
using System.ComponentModel;
using Main.Model;
using Main.View;
using Main.Command;
using System.Runtime.CompilerServices;

namespace Main.ViewModel
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        //Parent
        MainWindow Parent {  get; set; }
        //Observable Collections for UI ListBoxes
        public ObservableCollection<Person> _collection;
        public ObservableCollection<Person> Collection
        {  
            get { return _collection; }
            set 
            { 
                _collection = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<JournalEntry> Journal { get; set; }
        public CollectionContainer? Container { get; set; }

        //ICommands
        public ICommand SaveFileCommand { get; set; }
        public ICommand LoadFileCommand { get; set; }
        public ICommand CreateEmptyCollectionCommand { get; set; }
        public ICommand CreateAutoCollectionCommand { get; set; }
        public ICommand AddUnitCommand { get; set; }
        public ICommand ChangeUnitCommand { get; set; }
        public ICommand DeleteUnitCommand { get; set; }
        public ICommand UseLINQCommand { get; set; }
        //INotify
        public event PropertyChangedEventHandler? PropertyChanged;
        public MainWindowVM(MainWindow parent)
        {
            Parent = parent;

            Collection = new ObservableCollection<Person>();
            Journal = new ObservableCollection<JournalEntry>();
            Container = null;

            SaveFileCommand = new RelayCommand(SaveIntoFile, CanSaveIntoFile);
            LoadFileCommand = new RelayCommand(LoadFile, CanLoadFile);
            CreateEmptyCollectionCommand = new RelayCommand(CreateEmptyCollection, CanCreateEmptyCollection);
            CreateAutoCollectionCommand = new RelayCommand(CreateAutoCollection, CanCreateAutoCollection);
            AddUnitCommand = new RelayCommand(AddUnit, CanAddUnit);
            ChangeUnitCommand = new RelayCommand(ChangeUnit, CanChangeUnit);
            DeleteUnitCommand = new RelayCommand(DeleteUnit, CanDeleteUnit);
            UseLINQCommand = new RelayCommand(UseLINQ, CanUseLINQ);
        }

        //INotify OnPropertyChanged
        public void OnPropertyChanged([CallerMemberName] string param = "") 
        {
            if(PropertyChanged != null) 
            {
                PropertyChanged(this, new PropertyChangedEventArgs(param));
            }
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
                AskQuantity window = new();
                window.Owner = Parent;
                window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                if(window.ShowDialog() == true) 
                {
                    int quantity = window.ViewModel.PackedValue;

                    Container = new CollectionContainer();
                    Container.Collection.CollectionName = "Random Collection";
                    for (int i = 0; i < quantity; i++)
                    {
                        Person randomPerson = ReturnRandomPerson();
                        Container.AddUnit(randomPerson.Key, randomPerson);
                    }
                    UpdateData();
                }
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
        //AddUnit
        public void AddUnit(object? param)
        {
            if (Container != null)
            {
                AddUnitWindow window = new();
                window.Owner = Parent;
                window.WindowStartupLocation= WindowStartupLocation.CenterOwner;
                if (window.ShowDialog() == true) 
                {
                    PersonInput? data = window.ViewModel.PackedData;
                    int typeIndex = window.ViewModel.typeIndex;

                    if (Container.Collection.ContainsKey(data.Key)) 
                    {
                        MessageBox.Show("Unit with specified key is already exists");
                        return;
                    }

                    string key = data.Key;
                    Person value = UnpackData(data, typeIndex);

                    Container.AddUnit(key, value);
                    UpdateData();
                }
            }
            else
            {
                MessageBox.Show("Container is null.\nNo collection to add");
                return;
            }
        }
        public bool CanAddUnit(object? param) 
        { 
            return true; 
        }
        //ChangeUnit
        public void ChangeUnit(object? param) 
        {
            if (Container != null)
            {
                ChangeUnitWindow window = new(Container.Collection);
                window.Owner = Parent;
                window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                window.ShowDialog();
                UpdateData();
            }
            else
            {
                MessageBox.Show("Container is null.\nNo collection to redact");
                return;
            }
        }
        public bool CanChangeUnit(object? param) 
        {
            return true;
        }
        //DeleteUnit
        public void DeleteUnit(object? param) 
        {
            if (Container != null)
            {
                DeleteUnitWindow window = new(Container.Collection);
                window.Owner = Parent;
                window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                window.ShowDialog();
                UpdateData();
            }
            else
            {
                MessageBox.Show("Container is null.\nNo collection to redact");
                return;
            }
        }
        public bool CanDeleteUnit(object? param) 
        {
            return true;
        }
        //UseLINQ
        public void UseLINQ(object? param) 
        {
            if (Container != null)
            {
                LINQWindow window = new(Collection);
                window.Owner = Parent;
                window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                window.ShowDialog();
                Collection = window.ViewModel.SortedCollection;
            }
            else
            {
                MessageBox.Show("Container is null.\nNo collection to use LINQ");
                return;
            }
        }
        public bool CanUseLINQ(object? param) 
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
        //Used to unpack data from AddUnitWindow. Converts PersonInput type to Person type
        private Person UnpackData(PersonInput data, int typeIndex) 
        {
            Person? unpackedUnit;
            switch (typeIndex)
            {
                case 0:
                    {
                        Person unit = new();
                        unit.Key = data.Key;
                        unit.Name = data.Name;
                        unit.Age = int.Parse(data.Age);
                        unit.Residence = data.Residence;
                        unit.link.Notes = data.Notes;
                        unit.link.Data = data.Data;
                        unpackedUnit = unit;
                        break;
                    }
                case 1:
                    {
                        Employee unit = new();
                        unit.Key = data.Key;
                        unit.Name = data.Name;
                        unit.Age = int.Parse(data.Age);
                        unit.Residence = data.Residence;
                        unit.link.Notes = data.Notes;
                        unit.link.Data = data.Data;
                        EmployeeInput extendedData = (EmployeeInput)data;
                        unit.Experience = int.Parse(extendedData.Experience);
                        unit.Salary = int.Parse(extendedData.Salary);
                        unpackedUnit = unit;
                        break;
                    }
                case 2:
                    {
                        Engineer unit = new();
                        unit.Key = data.Key;
                        unit.Name = data.Name;
                        unit.Age = int.Parse(data.Age);
                        unit.Residence = data.Residence;
                        unit.link.Notes = data.Notes;
                        unit.link.Data = data.Data;
                        EngineerInput extendedData = (EngineerInput)data;
                        unit.Experience = int.Parse(extendedData.Experience);
                        unit.Salary = int.Parse(extendedData.Salary);
                        unit.Department = extendedData.Department;
                        unpackedUnit = unit;
                        break;
                    }
                case 3:
                    {
                        Admin unit = new();
                        unit.Key = data.Key;
                        unit.Name = data.Name;
                        unit.Age = int.Parse(data.Age);
                        unit.Residence = data.Residence;
                        unit.link.Notes = data.Notes;
                        unit.link.Data = data.Data;
                        AdminInput extendedData = (AdminInput)data;
                        unit.Experience = int.Parse(extendedData.Experience);
                        unit.Salary = int.Parse(extendedData.Salary);
                        unit.HeadOffice = extendedData.HeadOffice;
                        unpackedUnit = unit;
                        break;
                    }
                default: 
                    {
                        MessageBox.Show("Can't unpack data.\nReturning null person");
                        unpackedUnit = null;
                        break;
                    }
            }
            return unpackedUnit;
        }
    }
}
