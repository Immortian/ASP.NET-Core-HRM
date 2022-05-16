using System.Windows;

namespace HRM.Desktop.Handlers
{
    public class ExeptionsAndMessenges
    {
        readonly MainWindow window;
        public ExeptionsAndMessenges(MainWindow mainWindow)
        {
            window = mainWindow;
        }
        public void TimerExeptions()
        {
            if (window.currentPage == "AuthorizationPage" || window.currentPage == "")
            {
                MessageBox.Show("Причина: длительное бездействие", "Выход из системы", MessageBoxButton.OK, MessageBoxImage.Information);
                window.Close();
            }
            else
            {
                window.Height = 450;
                window.MainFrame.Navigate(new Pages.AuthorizationPage(window));
                window.MenuFrame.Navigate(null);
                window.timer.timer.Start();
                MessageBox.Show("Причина: длительное бездействие", "Выход из системы", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }
        public void LackInputExeption()
        {
            MessageBox.Show("Все поля должны быть заполнены", "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        public void AuthorizationError()
        {
            MessageBox.Show("Пользователь с такими данными не найден", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public void DBError()
        {
            MessageBox.Show("База данных послала вас на юх", "Ошыпка стоп 0х00000000000", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public void CheckSettingsSaved()
        {
            if (Properties.Settings.Default.CompanyName == "" ||
                Properties.Settings.Default.DirectorName == "" ||
                Properties.Settings.Default.CompanyAddress == "" ||
                Properties.Settings.Default.INN == "" ||
                Properties.Settings.Default.KPP == "" ||
                Properties.Settings.Default.BIK == "" ||
                Properties.Settings.Default.PAcc == "" ||
                Properties.Settings.Default.CAcc == "" ||
                Properties.Settings.Default.Bank == "" ||
                Properties.Settings.Default.SaveFilePath == "")
            {
                if (MessageBoxResult.Yes == MessageBox.Show("В настройках приложения заполнены не все данные, необходимые для продолжения. Желаете заполнить их сейчас?", "Необходимые данные не заполнены", MessageBoxButton.YesNo, MessageBoxImage.Error))
                {
                    window.MainFrame.Navigate(new Pages.AdminNavigation.SettingsPage(window));
                }
            }
        }
        public void FileCreactionFailed()
        {
            MessageBox.Show("Ошибка в процессе создания файлов. Закройте файл с шаблоном и попробуйте снова ", "Ошибка в процессе создания файлов", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public void FormatFileLoss()
        {
            MessageBox.Show("Файл с шаблоном потерян", "Нет файла", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
