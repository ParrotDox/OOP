using Main.ViewModel;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for DeleteUnitWindow.xaml
    /// </summary>
    public partial class DeleteUnitWindow : Window
    {
        DeleteWindowVM ViewModel { get; set; }
        public DeleteUnitWindow(NewCustomHashTable<string, Person> collection)
        {
            DeleteWindowVM viewModel = new(collection);
            ViewModel = viewModel;
            this.DataContext = viewModel;
            InitializeComponent();
        }

        private void DeleteUnitButton_Click(object sender, RoutedEventArgs e)
        {
            bool isDeleted = ViewModel.DeleteUnit();
            if (isDeleted)
            {
                DialogResult = true;
                this.Close();
                return;
            }
            else
            {
                MessageBox.Show("Value hasn't been deleted");
                return;
            }
        }
    }
}
