using ClassIerarchyLib;
using Main.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Main.ViewModel
{
    public class DeleteWindowVM : INotifyPropertyChanged
    {
        private string? _userKey;
        public string? UserKey
        {
            get { return _userKey; }
            set
            {
                _userKey = value;
                Input = SetInputType();
                OnPropertyChanged();
            }
        }
        private DeleteInput _input;
        public DeleteInput Input
        {
            get
            {
                return _input;
            }
            set
            {
                _input = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public NewCustomHashTable<string, Person> Collection { get; set; }
        public DeleteWindowVM(NewCustomHashTable<string, Person> collection)
        {
            Input = new DeleteInput();
            Collection = collection;
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public bool DeleteUnit() 
        {
            bool isInputTypeCorrect = CanDeleteUnit();
            if (isInputTypeCorrect) 
            {
                bool isRemoved = Collection.Remove(_userKey);
                if (isRemoved) 
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            }
            else 
            {
                MessageBox.Show("No element with specified key");
                return false;
            }
        }
        private bool CanDeleteUnit() 
        {
            if (Input is DeleteInputFound) 
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
        private DeleteInput SetInputType()
        {
            if (_userKey == null)
            {
                return new DeleteInputNotFound();
            }

            Person person;
            bool gotValue = Collection.TryGetValue(_userKey, out person);

            if (gotValue)
            {
                DeleteInputFound newType = new();
                newType.Key = person.Key;
                newType.Name = person.Name;
                newType.Age = person.Age.ToString();
                newType.Residence = person.Residence;
                return newType;
            }
            else
            {
                return new DeleteInputNotFound();
            }
        }
    }
}
