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
using Main.ViewModel;
using ClassIerarchyLib;

namespace Main.View
{
    /// <summary>
    /// Interaction logic for ChangeUnitWindow.xaml
    /// </summary>
    public partial class ChangeUnitWindow : Window
    {
        ChangeWindowVM ViewModel { get; set; }
        public ChangeUnitWindow(NewCustomHashTable<string, Person> collection)
        {
            ChangeWindowVM viewModel = new(collection);
            ViewModel = viewModel;
            this.DataContext = viewModel;
            InitializeComponent();
        }

        private void ChangeUnitButton_Click(object sender, RoutedEventArgs e)
        {
            bool isChanged = ViewModel.ChangeUnit();
            if (isChanged) 
            {
                DialogResult = true;
                this.Close();
                return;
            }
            else 
            {
                MessageBox.Show("Value hasn't been changed");
                return;
            }
        }
    }
}
