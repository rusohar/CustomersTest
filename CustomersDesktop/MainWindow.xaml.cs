using CustomersDesktop.ViewModels;
using CustomersDesktop.Views;
using System.Windows;

namespace CustomersDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}
