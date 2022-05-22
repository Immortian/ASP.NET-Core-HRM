using HRM.Desktop.Model;
using System.Windows;
using System.Windows.Controls;

namespace HRM.Desktop.Pages.AdminNavigation
{
    /// <summary>
    /// Логика взаимодействия для AdminIndex.xaml
    /// </summary>
    public partial class AdminIndex : Page
    {
        readonly MainWindow window;
        Employee loggedUser;
        public AdminIndex(MainWindow mainWindow, Employee user)
        {
            InitializeComponent();
            window = mainWindow;
            loggedUser = user;
            window.currentPage = "AdminPages";
            window.sizeController.CheckWindowSize("AdminIndex");
            HLabel.Content = $"Здравтвуйте {(user.Passport.Name)}";
        }
        public AdminIndex(MainWindow mainWindow)
        {
            InitializeComponent();
            window = mainWindow;
            window.currentPage = "AdminPages";
            window.sizeController.CheckWindowSize("AdminIndex");
        }

        private void CurrentStatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            window.MainFrame.Navigate(new CurrentStatisticsPage(window));
        }

        private void CurrentDocumentsButton_Click(object sender, RoutedEventArgs e)
        {
            window.error.CheckSettingsSaved();
            window.MainFrame.Navigate(new DocumentDownloadPage());
        }

        private void NextPeriodOrganizerButton_Click(object sender, RoutedEventArgs e)
        {
            window.MainFrame.Navigate(new CreateNextPeriodPage(window));
        }
    }
}
