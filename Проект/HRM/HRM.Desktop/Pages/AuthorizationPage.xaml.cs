using HRM.Desktop.Handlers;
using HRM.Desktop.Model;
using HRM.Desktop.Pages.AdminNavigation;
using HRM.Desktop.Pages.Menu;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace HRM.Desktop.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        readonly Timer timer;
        readonly MainWindow window;
        public AuthorizationPage(MainWindow mainWindow)
        {
            window = mainWindow;
            InitializeComponent();
            timer = window.timer;
            window.currentPage = "AuthorizationPage";
            window.sizeController.CheckWindowSize("AuthorizationPage");
            window.Title = "Вход";

            UsernameTB.Text = Properties.Settings.Default.UserName;
            PasswordPB.Password = Properties.Settings.Default.Password;
        }
        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            string login = UsernameTB.Text;
            string pass = PasswordPB.Password;

            if (login == "" || pass == "")
            {
                window.error.LackInputExeption();
                return;
            }
            var loginContent = new StringContent(JsonConvert.SerializeObject(new Commands.LoginCommand { Password = pass, Username = login }), Encoding.UTF8, "application/json");
            var loginResponse = window.client.PostAsync(new Uri("https://localhost:44355/api/Authorization/Login"), loginContent).Result;
            int responseContentCode = (int)JsonConvert.DeserializeObject(loginResponse.Content.ReadAsStringAsync().Result, typeof(int));
            if (responseContentCode != 0)
            {
                var authContent = new StringContent(JsonConvert.SerializeObject(new Commands.LoginCommand { Password = pass, Username = login }), Encoding.UTF8, "application/json");
                var employeeResponse = window.client.PostAsync(new Uri("https://localhost:44355/api/Authorization/Authorize"), authContent).Result;
                var responseContentEmployee = (Employee)JsonConvert.DeserializeObject(employeeResponse.Content.ReadAsStringAsync().Result, typeof(Employee));
                if ((bool)SaveAuthChB.IsChecked)
                {
                    Properties.Settings.Default.UserName = login;
                    Properties.Settings.Default.Password = pass;
                }
                if (responseContentCode == 3)
                {
                    window.MenuFrame.Navigate(new AdminMenu(window));
                    window.MainFrame.Navigate(new AdminIndex(window));
                }
                else if (responseContentCode == 2)
                {
                    window.MenuFrame.Navigate(new AdminMenu(window, responseContentEmployee));
                    window.MainFrame.Navigate(new AdminIndex(window, responseContentEmployee));
                }

                else if (responseContentCode == 1)
                {
                    //window.MenuFrame.NavigationService.Navigate(new UserMenu(window, responseContentEmployee));
                }
            }
            else
                window.error.AuthorizationError();
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }
    }
}
