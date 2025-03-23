using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    class MainViewModel
    {
        public ThreadViewModel threadViewModel { get; set; }
        public MainViewModel()
        {
            threadViewModel = new ThreadViewModel();
        }
    }
}
