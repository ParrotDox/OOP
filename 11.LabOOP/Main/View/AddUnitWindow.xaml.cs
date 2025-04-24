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
using Main.ViewModel;
namespace Main.View
{
    /// <summary>
    /// Interaction logic for AddUnitWindow.xaml
    /// </summary>
    public partial class AddUnitWindow : Window
    {
        AddUnitWindowVM ViewModel {  get; set; }
        public AddUnitWindow()
        {
            ViewModel = new AddUnitWindowVM();
            this.DataContext = ViewModel;
            InitializeComponent();
        }

        private void UserChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int userChoice = UserChoice.SelectedIndex;
            ViewModel.ChangeInput(userChoice);
        }

        private void ButtonAddUnit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
