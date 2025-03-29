using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel mVmlnk;
        public MainWindow()
        {
            InitializeComponent();
            MainViewModel mVm = new MainViewModel();
            this.DataContext = mVm;
            mVmlnk = mVm;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mVmlnk.WindowClosing(sender, e);
        }
    }
}