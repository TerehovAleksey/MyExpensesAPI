using MyExpenses.WpfClient.ViewModels;
using System.Windows.Controls;
namespace MyExpenses.WpfClient.Views
{
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
            DataContext = DI.Get<SettingsViewModel>();
        }
    }
}
