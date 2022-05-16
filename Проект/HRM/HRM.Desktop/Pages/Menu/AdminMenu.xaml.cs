using HRM.Desktop.Model;
using HRM.Desktop.Pages.AdminNavigation;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HRM.Desktop.Pages.Menu
{
    /// <summary>
    /// Логика взаимодействия для AdminMenu.xaml
    /// </summary>
    public partial class AdminMenu : Page
    {
        readonly MainWindow window;
        Employee loggedUser;
        public AdminMenu(MainWindow mainWindow, Employee user)
        {
            InitializeComponent();
            window = mainWindow;
            loggedUser = user;

            NameLabel.Content = user.Passport.Name == null ? loggedUser.Authorizations.FirstOrDefault().Username : loggedUser.Passport.Name;
        }
        public AdminMenu(MainWindow mainWindow)
        {
            InitializeComponent();
            window = mainWindow;
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            loggedUser = null;
            window.MainFrame.Navigate(new AuthorizationPage(window));
            window.MenuFrame.Navigate(null);
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            window.MainFrame.Navigate(new AdminNavigation.AdminIndex(window));
        }

        private void DepartmentButton_Click(object sender, RoutedEventArgs e)
        {
            window.MainFrame.Navigate(null);
        }

        private void DocumentsButton_Click(object sender, RoutedEventArgs e)
        {
            window.MainFrame.Navigate(null);
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            window.MainFrame.Navigate(new SettingsPage(window));
        }
    }
}
