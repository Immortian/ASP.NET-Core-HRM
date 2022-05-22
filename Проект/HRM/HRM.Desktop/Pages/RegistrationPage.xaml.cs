using System;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
namespace HRM.Desktop.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        readonly MainWindow window;

        public RegistrationPage(MainWindow window)
        {
            InitializeComponent();
            this.window = window;
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            window.MainFrame.GoBack();
        }

        private void Registrate_Button_Click(object sender, RoutedEventArgs e)
        {
            var pass = PasswordPB.Password == PasswordPB_Copy.Password ? PasswordPB.Password : "";
            if (pass == "")
                MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                var login = UsernameTB.Text;
                var verification = VerificationTB.Text;
                var regContent = new StringContent(JsonConvert.SerializeObject(new Commands.RegistrationCommand { Password = pass, Username = login, AuthCode = verification }), Encoding.UTF8, "application/json");
                var employeeResponse = window.client.PostAsync(new Uri("https://localhost:44355/api/Authorization/Registration"), regContent).Result;
                if (employeeResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Регистрация завершена", "Успех", MessageBoxButton.OK);
                    window.MainFrame.Navigate(new AuthorizationPage(window));
                }
                else
                    MessageBox.Show("Недействительный идентификатор", "Ошибка регистрации", MessageBoxButton.OK);
            }
        }
    }
}
