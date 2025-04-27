using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ClassIerarchyLib;
using Main.Command;
using Main.View;

namespace Main.ViewModel
{
    public class LINQWindowVM
    {
        LINQWindow Parent {  get; set; }
        public string FindByInput {  get; set; }
        public ObservableCollection<Person> Collection { get; set; }
        public ObservableCollection<Person> SortedCollection { get; set; }
        public ICommand CountUnitsCommand { get; set; }
        public ICommand CountSalaryCommand { get; set; }
        public ICommand FindCustomCommand { get; set; }
        public LINQWindowVM(ObservableCollection<Person> collection, LINQWindow parent)
        {
            Parent = parent;
            FindByInput = "";
            Collection = collection;
            SortedCollection = collection;
            CountUnitsCommand = new RelayCommand(CountUnits, CanCountUnits);
            CountSalaryCommand = new RelayCommand(CountSalary, CanCountSalary);
            FindCustomCommand = new RelayCommand(FindCustom, CanFindCustom);
        }
        public void SortCollectionByName() 
        {
            SortedCollection = new(Collection.OrderBy(x => x.Name));
        }
        public void SortCollectionByAge() 
        {
            SortedCollection = new(Collection.OrderBy(x => x.Age));
        }
        public void SortCollectionByResidence() 
        {
            SortedCollection = new(Collection.OrderBy(x => x.Residence));
        }
        //CountUnits
        public void CountUnits(object? param) 
        {
            MessageBox.Show($"Current count is {Collection.Count()}");
        }
        public bool CanCountUnits(object? param) 
        {
            return true;
        }
        //CountSalary
        public void CountSalary(object? param)
        {
            MessageBox.Show($"Current count is {Collection.OfType<Employee>().Sum(e => e.Salary)}");
        }
        public bool CanCountSalary(object? param)
        {
            return true;
        }
        //FindCustom
        public void FindCustom(object? param) 
        {
            string? name = null;
            int? age = null;
            string? residence = null;
            bool isValidInput = CheckIsInputCorrect(FindByInput, out name, out age, out residence);
            if (isValidInput) 
            {
                ObservableCollection<Person> query;
                var filtered = Collection
                .Where(p => name == null || p.Name == name)
                .Where(p => age == null || p.Age == age)
                .Where(p => residence == null || p.Residence == residence);
                query = new(filtered);
                ResultLINQWindow window = new(query);
                window.Owner = Parent;
                window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                window.Show();
            }
            else 
            {
                MessageBox.Show("Input is not correct");
            }
        }
        public bool CanFindCustom(object? param) 
        {
            return true;
        }
        private bool CheckIsInputCorrect(string input, out string? name, out int? age, out string? residence) 
        {
            name = null;
            age = null;
            residence = null;

            string[] parts = input.Split(';');

            if(parts.Length >= 1) 
            {
                Dictionary<string, string> dict = new();

                foreach(var part in parts) 
                {
                    string[] kv = part.Split('=');
                    if(kv.Length == 2) 
                    {
                        dict[kv[0].ToLower()] = kv[1];
                    }
                }

                bool isAnyPropertyValidated = false;
                //Validation
                if (dict.ContainsKey("name")) 
                {
                    name = dict["name"];
                    isAnyPropertyValidated = true;
                }
                if (dict.ContainsKey("age"))
                {
                    int parsed;
                    bool canParse = int.TryParse(dict["age"], out parsed);
                    age = canParse ? parsed : null;
                    isAnyPropertyValidated = true;
                }
                if (dict.ContainsKey("residence")) 
                {
                    residence = dict["residence"];
                    isAnyPropertyValidated = true;
                }
                return isAnyPropertyValidated;
            }
            else 
            {
                MessageBox.Show("Input is incorrect");
                return false;
            }
        }
    }
}
