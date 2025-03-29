using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Main
{
    class MainViewModel
    {
        public ThreadViewModel threadViewModel { get; set; }
        private bool _isProcessing;
        public bool IsProcessing 
        {
            get 
            {
                return threadViewModel.IsRunning();
            }
            set 
            {
                IsProcessing = value;
            }
        } 
        public MainViewModel()
        {
            threadViewModel = new ThreadViewModel();
        }
        
        public void WindowClosing(object sender, CancelEventArgs e) 
        {
            if (IsProcessing) 
            {
                e.Cancel = true;
                MessageBox.Show("Stop all threads before \nclosing the programm!", "Alert", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }
    }
}
