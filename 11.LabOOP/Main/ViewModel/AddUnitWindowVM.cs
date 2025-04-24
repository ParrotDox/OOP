using ClassIerarchyLib;
using Main.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Main.ViewModel
{
    public class PersonInput 
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Residence { get; set; }
        public PersonInput()
        {
            Key = "None";
            Name = "None";
            Age = 0;
            Residence = "None";
        }
    }
    public class EmployeeInput : PersonInput
    {
        public int Experience {  get; set; }
        public int Salary { get; set; }
        public EmployeeInput() : base()
        {
            Experience = 0;
            Salary = 0;
        }
    }
    public class EngineerInput : EmployeeInput
    {
        public string Department { get; set; }
        public EngineerInput() : base()
        {
            Department = "None";
        }
    }
    public class AdminInput : EmployeeInput
    {
        public string HeadOffice { get; set; }
        public AdminInput() : base()
        {
            HeadOffice = "None";
        }
    }
    public class AddUnitWindowVM : INotifyPropertyChanged
    {
        private PersonInput? _personInput;
        public PersonInput? Input 
        { 
            get 
            { 
                return _personInput; 
            } 
            set 
            {
                _personInput = value;
                OnPropertyChanged();
            } 
        }
        //ICommands
        public ICommand AddUnit {  get; set; }
        public AddUnitWindowVM()
        {
            Input = new PersonInput();
            AddUnit = new RelayCommand(AddUnitToCollection, CanAddUnitToCollection);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        //Changes the input textBoxes at UI, called from code-behind
        public void ChangeInput(int choice) 
        {
            switch (choice) 
            {
                case 0: 
                    {
                        Input = new PersonInput();
                        break;
                    }
                case 1:
                    {
                        Input = new EmployeeInput();
                        break;
                    }
                case 2:
                    {
                        Input = new EngineerInput();
                        break;
                    }
                case 3:
                    {
                        Input = new AdminInput();
                        break;
                    }
            }
        }
        //Add unit to collection
        public void AddUnitToCollection(object? param) 
        {
            
        }
        public bool CanAddUnitToCollection(object? param) 
        {
            return true;
        }
    }
}
