using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.Model;
using Main.View;
using Main.Command;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ClassIerarchyLib;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Main.ViewModel
{
    public class ChangeWindowVM : INotifyPropertyChanged
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
        private PersonInput _input;
        public PersonInput Input 
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
        public NewCustomHashTable<string,Person> Collection { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        
        public ChangeWindowVM(NewCustomHashTable<string, Person> collection)
        {
            Input = new NullInput();
            Collection = collection;
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public bool ChangeUnit() 
        {
            if (CanUseData()) 
            {
                Person value;
                bool gotValue = Collection.TryGetValue(_userKey, out value);
                if (gotValue)
                {
                    switch (value)
                    {
                        case Admin:
                            {
                                Admin admin = new();
                                admin.Key = Input.Key;
                                admin.Name = Input.Name;
                                admin.Age = int.Parse(Input.Age);
                                admin.Residence = Input.Residence;
                                admin.link.Notes = Input.Notes;
                                admin.link.Data = Input.Data;
                                AdminInput extendedInput = (AdminInput)Input;
                                admin.Experience = int.Parse(extendedInput.Experience);
                                admin.Salary = int.Parse(extendedInput.Salary);
                                admin.HeadOffice = extendedInput.HeadOffice;
                                Collection.Remove(_userKey);
                                Collection.Add(admin.Key, admin);
                                return true;
                            }
                        case Engineer:
                            {
                                Engineer engineer = new();
                                engineer.Key = Input.Key;
                                engineer.Name = Input.Name;
                                engineer.Age = int.Parse(Input.Age);
                                engineer.Residence = Input.Residence;
                                engineer.link.Notes = Input.Notes;
                                engineer.link.Data = Input.Data;
                                EngineerInput extendedInput = (EngineerInput)Input;
                                engineer.Experience = int.Parse(extendedInput.Experience);
                                engineer.Salary = int.Parse(extendedInput.Salary);
                                engineer.Department = extendedInput.Department;
                                Collection.Remove(_userKey);
                                Collection.Add(engineer.Key, engineer);
                                return true;
                            }
                        case Employee:
                            {
                                Employee employee = new();
                                employee.Key = Input.Key;
                                employee.Name = Input.Name;
                                employee.Age = int.Parse(Input.Age);
                                employee.Residence = Input.Residence;
                                employee.link.Notes = Input.Notes;
                                employee.link.Data = Input.Data;
                                EmployeeInput extendedInput = (EmployeeInput)Input;
                                employee.Experience = int.Parse(extendedInput.Experience);
                                employee.Salary = int.Parse(extendedInput.Salary);
                                Collection.Remove(_userKey);
                                Collection.Add(employee.Key, employee);
                                return true;
                            }
                        case Person:
                            {
                                Person person = new();
                                person.Key = Input.Key;
                                person.Name = Input.Name;
                                person.Age = int.Parse(Input.Age);
                                person.Residence = Input.Residence;
                                person.Residence = Input.Residence;
                                person.link.Notes = Input.Notes;
                                Collection.Remove(_userKey);
                                Collection.Add(person.Key, person);
                                return true;
                            }
                        default: 
                            {
                                return false;
                            }
                    }
                }
                else 
                {
                    MessageBox.Show("Value hasn't been got");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Data format is wrong.\nPlease check all fields");
                return false;
            }
        }
        private PersonInput SetInputType() 
        {
            if (_userKey == null) 
            {
                return new NullInput();
            }

            Person person;
            bool gotValue = Collection.TryGetValue(_userKey, out person);

            if (gotValue) 
            {
                switch (person) 
                {
                    case Admin: 
                        {
                            AdminInput unit = new();
                            unit.Key = person.Key;
                            unit.Name = person.Name;
                            unit.Age = person.Age.ToString();
                            unit.Residence = person.Residence;
                            Admin extendedUnit = (Admin)person;
                            unit.Experience = extendedUnit.Experience.ToString();
                            unit.Salary = extendedUnit.Salary.ToString();
                            unit.HeadOffice = extendedUnit.HeadOffice;
                            return unit;
                        }
                    case Engineer:
                        {
                            EngineerInput unit = new();
                            unit.Key = person.Key;
                            unit.Name = person.Name;
                            unit.Age = person.Age.ToString();
                            unit.Residence = person.Residence;
                            Engineer extendedUnit = (Engineer)person;
                            unit.Experience = extendedUnit.Experience.ToString();
                            unit.Salary = extendedUnit.Salary.ToString();
                            unit.Department = extendedUnit.Department;
                            return unit;
                        }
                    case Employee:
                        {
                            EmployeeInput unit = new();
                            unit.Key = person.Key;
                            unit.Name = person.Name;
                            unit.Age = person.Age.ToString();
                            unit.Residence = person.Residence;
                            Employee extendedUnit = (Employee)person;
                            unit.Experience = extendedUnit.Experience.ToString();
                            unit.Salary = extendedUnit.Salary.ToString();
                            return unit;
                        }
                    case Person:
                        {
                            PersonInput unit = new();
                            unit.Key = person.Key;
                            unit.Name = person.Name;
                            unit.Age = person.Age.ToString();
                            unit.Residence = person.Residence;
                            return unit;
                        }
                    default: 
                        {
                            return new NullInput();
                        }
                }
            }
            else 
            {
                return new NullInput();
            }
        }
        private bool CanUseData()
        {
            switch (Input)
            {
                case NullInput: 
                    {
                        return false;
                    }
                case AdminInput:
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
                case EngineerInput:
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
                case EmployeeInput:
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
                case PersonInput:
                    {
                        bool isKeyCorrect = Input.Key.Length > 0 ? true : false;
                        bool isNameCorrect = Input.Name.Length > 0 ? true : false;
                        bool isAgeCorrect = Input.Age.Length > 0 && int.TryParse(Input.Age, out _) && int.Parse(Input.Age) > 0 ? true : false;
                        bool isResidenceCorrect = Input.Residence.Length > 0 ? true : false;

                        return isKeyCorrect && isNameCorrect && isAgeCorrect && isResidenceCorrect;
                    }
            }
            MessageBox.Show("Can't check type of input to return\nboolean result");
            return false;
        }
    }
}
