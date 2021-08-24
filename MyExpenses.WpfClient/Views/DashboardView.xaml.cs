using MyExpenses.WpfClient.ViewModels;
using System.Windows.Controls;

namespace MyExpenses.WpfClient.Views
{
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            InitializeComponent();
            DataContext = DI.Get<DashboardViewModel>();
        }
    }
}
