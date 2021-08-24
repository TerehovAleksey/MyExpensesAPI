using MyExpenses.WpfClient.Helpers;
using System.Windows;
using System.Windows.Input;

namespace MyExpenses.WpfClient.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly Window _window;

        private const int RESIZE_BORDER = 6;
        private const int OUTER_MARGIN_SIZE = 10;
        private const int WINDOW_RADIUS = 10;
        private const int TITLE_HEIGHT = 32;
        private double WINDOW_MIN_HEIGHT = 400;
        private double WINDOW_MIN_WIDTH = 400;
        private const int INNER_CONTENT_PADDING = 0;
        private const string TITLE = "MONEYNET";

        private readonly WindowDockPosition dockPosition;

        public int TitleHeight => TITLE_HEIGHT;
        public string Title => TITLE;

        private bool Bordless => _window.WindowState == WindowState.Maximized || dockPosition != WindowDockPosition.Undocked;
        private int ResizeBorder => Bordless ? 0 : RESIZE_BORDER;
        private int OuterMarginSize => _window.WindowState == WindowState.Maximized ? 0 : OUTER_MARGIN_SIZE;
        private int WindowRadius => _window.WindowState == WindowState.Maximized ? 0 : WINDOW_RADIUS;
        public GridLength TitleHeightGridLength => new GridLength(TitleHeight + ResizeBorder);
        public Thickness ResizeBorderThickness => new Thickness(ResizeBorder + OuterMarginSize);
        public Thickness OuterMarginSizeThickness => new Thickness(OuterMarginSize);
        public Thickness InnerContentPadding => new Thickness(INNER_CONTENT_PADDING);
        public CornerRadius WindowCornerRadius => new CornerRadius(WindowRadius);
        public double WindowMinimumHeight => WINDOW_MIN_HEIGHT;
        public double WindowMinimumWidth => WINDOW_MIN_WIDTH;

        public ICommand MinimizeCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand MenuCommand { get; set; }

        public MainWindowViewModel(Window window)
        {
            _window = window;
            _window.StateChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OuterMarginSizeThickness));
                OnPropertyChanged(nameof(WindowCornerRadius));
            };

            MinimizeCommand = new RelayCommand(() => window.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => window.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => window.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(_window, GetMousePosition()));

            //fix window resize
            var resizer = new WindowResizer(_window);
        }

        private Point GetMousePosition()
        {
            var position = Mouse.GetPosition(_window);
            return new Point(position.X + _window.Left, position.Y + _window.Top);
        }
    }
}
