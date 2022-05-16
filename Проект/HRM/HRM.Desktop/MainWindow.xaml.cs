using HRM.Desktop.Handlers;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;

namespace HRM.Desktop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Timer timer;
        public string currentPage = "";
        public ExeptionsAndMessenges error;
        public WindowSizeController sizeController;
        public readonly HttpClient client = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
            error = new ExeptionsAndMessenges(this);
            timer = new Timer(this);
            sizeController = new WindowSizeController(this);
            MainFrame.Navigate(new Pages.AuthorizationPage(this));
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            timer.StartupTimer();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            timer.ResetTimer();
        }
    }
}
