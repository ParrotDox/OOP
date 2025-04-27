using ClassIerarchyLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Main.Model;
using Main.View;
using Main.Command;

namespace Main.ViewModel
{
    public class AddUnitWindowVM : INotifyPropertyChanged
    {
        //This property is used in databinding, also for containing info
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
        //This property 'll be used to provide main window with packed info
        public PersonInput? PackedData;
        //Counter to understand what type of object is used
        public int typeIndex = 0;
        public AddUnitWindowVM()
        {
            Input = new NullInput();
            PackedData = null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        //Changes the input textBoxes at UI using dataTemplates, called from code-behind
        public void ChangeInput(int choice) 
        {
            switch (choice) 
            {
                case 0: 
                    {
                        Input = new PersonInput();
                        typeIndex = 0;
                        break;
                    }
                case 1:
                    {
                        Input = new EmployeeInput();
                        typeIndex = 1;
                        break;
                    }
                case 2:
                    {
                        Input = new EngineerInput();
                        typeIndex = 2;
                        break;
                    }
                case 3:
                    {
                        Input = new AdminInput();
                        typeIndex = 3;
                        break;
                    }
            }
        }
        //Get and pack unit for extraction
        public void PackData()
        {
            switch (typeIndex) 
            {
                case 0: 
                    {
                        PersonInput pack = new();
                        pack.Key = Input.Key;
                        pack.Name = Input.Name;
                        pack.Age = Input.Age;
                        pack.Residence = Input.Residence;
                        pack.Notes = Input.Notes;
                        pack.Data = Input.Data;
                        PackedData = pack;
                        break;
                    }
                case 1:
                    {
                        EmployeeInput pack = new ();
                        pack.Key = Input.Key;
                        pack.Name = Input.Name;
                        pack.Age = Input.Age;
                        pack.Residence = Input.Residence;
                        pack.Notes = Input.Notes;
                        pack.Data = Input.Data;
                        pack.Experience = ((EmployeeInput)Input).Experience;
                        pack.Salary = ((EmployeeInput)Input).Salary;
                        PackedData = pack;
                        break;
                    }
                case 2:
                    {
                        EngineerInput pack = new();
                        pack.Key = Input.Key;
                        pack.Name = Input.Name;
                        pack.Age = Input.Age;
                        pack.Residence = Input.Residence;
                        pack.Notes = Input.Notes;
                        pack.Data = Input.Data;
                        pack.Experience = ((EngineerInput)Input).Experience;
                        pack.Salary = ((EngineerInput)Input).Salary;
                        pack.Department = ((EngineerInput)Input).Department;
                        PackedData = pack;
                        break;
                    }
                case 3:
                    {
                        AdminInput pack = new();
                        pack.Key = Input.Key;
                        pack.Name = Input.Name;
                        pack.Age = Input.Age;
                        pack.Residence = Input.Residence;
                        pack.Notes = Input.Notes;
                        pack.Data = Input.Data;
                        pack.Experience = ((AdminInput)Input).Experience;
                        pack.Salary = ((AdminInput)Input).Salary;
                        pack.HeadOffice = ((AdminInput)Input).HeadOffice;
                        PackedData = pack;
                        break;
                    }
            }
        }
        public bool CanPackData()
        {
            switch (typeIndex)
            {
                case 0:
                    {
                        bool isKeyCorrect = Input.Key.Length > 0 ? true : false;
                        bool isNameCorrect = Input.Name.Length > 0 ? true : false;
                        bool isAgeCorrect = Input.Age.Length > 0 && int.TryParse(Input.Age, out _) && int.Parse(Input.Age) > 0 ? true : false;
                        bool isResidenceCorrect = Input.Residence.Length > 0 ? true : false;

                        return isKeyCorrect && isNameCorrect && isAgeCorrect && isResidenceCorrect;
                    }
                case 1:
                    {
                        bool isKeyCorrect = Input.Key.Length > 0 ? true : false;
                        bool isNameCorrect = Input.Name.Length > 0 ? true : false;
                        bool isAgeCorrect = Input.Age.Length > 0 && int.TryParse(Input.Age, out _) && int.Parse(Input.Age) > 0 ? true : false;
                        bool isResidenceCorrect = Input.Residence.Length > 0 ? true : false;

                        EmployeeInput extendedUnit = (EmployeeInput)Input;
                        bool isExperienceCorrect = extendedUnit.Experience.Length > 0 &&
                            int.TryParse(extendedUnit.Experience, out _) &&
                            int.Parse(extendedUnit.Experience) > 0 ? true : false;

                        bool isSalaryCorrect = extendedUnit.Salary.Length > 0 &&
                            int.TryParse(extendedUnit.Salary, out _) &&
                            int.Parse(extendedUnit.Salary) > 0 ? true : false;

                        return isKeyCorrect && isNameCorrect && isAgeCorrect && isResidenceCorrect && isExperienceCorrect && isSalaryCorrect;
                    }
                case 2:
                    {
                        bool isKeyCorrect = Input.Key.Length > 0 ? true : false;
                        bool isNameCorrect = Input.Name.Length > 0 ? true : false;
                        bool isAgeCorrect = Input.Age.Length > 0 && int.TryParse(Input.Age, out _) && int.Parse(Input.Age) > 0 ? true : false;
                        bool isResidenceCorrect = Input.Residence.Length > 0 ? true : false;

                        EngineerInput extendedUnit = (EngineerInput)Input;
                        bool isExperienceCorrect = extendedUnit.Experience.Length > 0 &&
                            int.TryParse(extendedUnit.Experience, out _) &&
                            int.Parse(extendedUnit.Experience) > 0 ? true : false;

                        bool isSalaryCorrect = extendedUnit.Salary.Length > 0 &&
                            int.TryParse(extendedUnit.Salary, out _) &&
                            int.Parse(extendedUnit.Salary) > 0 ? true : false;

                        bool isDepartmentCorrect = extendedUnit.Department.Length > 0 ? true : false;

                        return isKeyCorrect && isNameCorrect && isAgeCorrect && isResidenceCorrect && isExperienceCorrect && isSalaryCorrect && isDepartmentCorrect;
                    }
                case 3:
                    {
                        bool isKeyCorrect = Input.Key.Length > 0 ? true : false;
                        bool isNameCorrect = Input.Name.Length > 0 ? true : false;
                        bool isAgeCorrect = Input.Age.Length > 0 && int.TryParse(Input.Age, out _) && int.Parse(Input.Age) > 0 ? true : false;
                        bool isResidenceCorrect = Input.Residence.Length > 0 ? true : false;

                        AdminInput extendedUnit = (AdminInput)Input;
                        bool isExperienceCorrect = extendedUnit.Experience.Length > 0 &&
                            int.TryParse(extendedUnit.Experience, out _) &&
                            int.Parse(extendedUnit.Experience) > 0 ? true : false;

                        bool isSalaryCorrect = extendedUnit.Salary.Length > 0 &&
                            int.TryParse(extendedUnit.Salary, out _) &&
                            int.Parse(extendedUnit.Salary) > 0 ? true : false;

                        bool isHeadOfficeCorrect = extendedUnit.HeadOffice.Length > 0 ? true : false;

                        return isKeyCorrect && isNameCorrect && isAgeCorrect && isResidenceCorrect && isExperienceCorrect && isSalaryCorrect && isHeadOfficeCorrect;
                    }
            }
            MessageBox.Show("Can't check type of input to return\nboolean result");
            return false;
        }
    }
}
