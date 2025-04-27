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

namespace Main.View
{
    /// <summary>
    /// Interaction logic for AskQuantity.xaml
    /// </summary>
    public partial class AskQuantity : Window
    {
        public AddQuantityWindowVM ViewModel { get; set; }
        public AskQuantity()
        {
            AddQuantityWindowVM viewModel = new();
            this.DataContext = viewModel;
            ViewModel = viewModel;
            InitializeComponent();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.CheckIsInputCorrect()) 
            {
                ViewModel.PackValue();
                this.DialogResult = true;
                this.Close();
            }
            else 
            {
                MessageBox.Show("Current value is wrong.\nPlease input correct value");
                return;
            }
        }
    }
}
