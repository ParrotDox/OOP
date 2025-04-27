using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Model
{
    //Used as a identificator to show input contentControl at AddUnitWindow and ChangeUnitWindow
    public class PersonInput
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Residence { get; set; }
        public string Notes { get; set; }
        public string Data { get; set; }
        public PersonInput()
        {
            Key = "";
            Name = "";
            Age = "";
            Residence = "";
            Notes = "";
            Data = "";
        }
    }
    public class EmployeeInput : PersonInput
    {
        public string Experience { get; set; }
        public string Salary { get; set; }
        public EmployeeInput() : base()
        {
            Experience = "";
            Salary = "";
        }
    }
    public class EngineerInput : EmployeeInput
    {
        public string Department { get; set; }
        public EngineerInput() : base()
        {
            Department = "";
        }
    }
    public class AdminInput : EmployeeInput
    {
        public string HeadOffice { get; set; }
        public AdminInput() : base()
        {
            HeadOffice = "";
        }
    }
    //Used as a identificator to show empty contentControl at ChangeUnitWindow
    public class NullInput : PersonInput
    {
        public NullInput() : base()
        {

        }
    }

    //Used as a identificator to show different contentControl at DeleteUnitWindow
    public class DeleteInput 
    {
        public string? Key { get; set; }
        public string? Name {  get; set; }
        public string? Age { get; set; }
        public string Residence { get; set; }
        public DeleteInput()
        {
            
        }
    }
    public class DeleteInputNotFound :DeleteInput
    {
        public DeleteInputNotFound()
        {
            
        }
    }
    public class DeleteInputFound : DeleteInput
    {
        public DeleteInputFound()
        {
            
        }
    }
}
