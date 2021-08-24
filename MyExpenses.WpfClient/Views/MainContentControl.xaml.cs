using System.Windows.Controls;

namespace MyExpenses.WpfClient.Views
{
    public partial class MainContentControl : UserControl
    {
        public MainContentControl()
        {
            InitializeComponent();
            DataContext = DI.Application;
        }
    }
}
