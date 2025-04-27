using Main.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Main.View
{
    /// <summary>
    /// Interaction logic for LINQWindow.xaml
    /// </summary>
    public partial class LINQWindow : Window
    {
        public LINQWindowVM ViewModel { get; set; }
        public LINQWindow(ObservableCollection<Person> collection)
        {
            LINQWindowVM viewModel = new(collection, this);
            this.DataContext = viewModel;
            this.ViewModel = viewModel;
            InitializeComponent();
        }

        private void FilterByNameButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SortCollectionByName();
            DialogResult = true;
        }

        private void FilterByAgeButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SortCollectionByAge();
            DialogResult = true;
        }

        private void FilterByResidenceButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SortCollectionByResidence();
            DialogResult = true;
        }
    }
}
