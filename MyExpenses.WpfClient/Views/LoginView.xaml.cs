using MyExpenses.WpfClient.ViewModels;
using System.Windows.Controls;

namespace MyExpenses.WpfClient.Views
{
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = DI.Get<LoginViewModel>();
        }
    }
}
