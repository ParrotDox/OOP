using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassIerarchyLib;

namespace Main.ViewModel
{
    public class ResultLINQWindowVM
    {
        public ObservableCollection<Person> Query {  get; set; }
        public ResultLINQWindowVM(ObservableCollection<Person> query)
        {
            Query = query;
        }
    }
}
