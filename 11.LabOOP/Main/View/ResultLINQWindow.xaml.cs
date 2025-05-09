﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClassIerarchyLib;
using Main.ViewModel;

namespace Main.View
{
    /// <summary>
    /// Interaction logic for ResultLINQWindow.xaml
    /// </summary>
    public partial class ResultLINQWindow : Window
    {
        public ResultLINQWindow(ObservableCollection<Person> query)
        {
            ResultLINQWindowVM viewModel = new(query);
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
