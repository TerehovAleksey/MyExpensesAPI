using MyExpenses.WpfClient.ViewModels;
using System.Windows;

namespace MyExpenses.WpfClient
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = DI.Get<MainWindowViewModel>();
        }
    }
}
