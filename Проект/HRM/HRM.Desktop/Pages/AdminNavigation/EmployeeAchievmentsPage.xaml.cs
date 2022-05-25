using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HRM.Desktop.Commands;
using HRM.Desktop.Model;
using Newtonsoft.Json;

namespace HRM.Desktop.Pages.AdminNavigation
{
    /// <summary>
    /// Логика взаимодействия для EmployeeAchievmentsPage.xaml
    /// </summary>
    public partial class EmployeeAchievmentsPage : Page
    {
        public List<PersonalAchievement> achievements;
        private readonly MainWindow window;
        private List<Period> periodList;
        private List<Employee> employeeList;
        public EmployeeAchievmentsPage(MainWindow window)
        {
            var employeeResponse = window.client.GetAsync(new Uri("https://localhost:44355/api/Employee")).Result;
            var employeeResponseContent = (List<Employee>)JsonConvert.DeserializeObject(employeeResponse.Content.ReadAsStringAsync().Result, typeof(List<Employee>));
            employeeList = employeeResponseContent;
            this.Resources.Add("EmployeeList", employeeList);
            InitializeComponent();
            this.window = window;
            var achievmentsResponse = window.client.GetAsync(new Uri("https://localhost:44355/api/Achievements")).Result;
            var achievmentsResponseContent = (List<PersonalAchievement>)JsonConvert.DeserializeObject(achievmentsResponse.Content.ReadAsStringAsync().Result, typeof(List<PersonalAchievement>));

            achievements = achievmentsResponseContent;
            AchievementsDG.ItemsSource = achievements;

            
            var periodsResponse = window.client.GetAsync(new Uri("https://localhost:44355/api/Statistics/Period")).Result;
            var periodsResponseContent = (List<Period>)JsonConvert.DeserializeObject(periodsResponse.Content.ReadAsStringAsync().Result, typeof(List<Period>));
            periodList = periodsResponseContent;
            List<string> PeriodCBsource = new List<string>();
            foreach (var i in periodList)
            {
                PeriodCBsource.Add(new DateTime(i.Year, i.Month, 1).Date.ToString("Y"));
            }
            PeriodCB.ItemsSource = PeriodCBsource;

            PeriodCB.SelectedIndex = PeriodCB.Items.Count - 1;
        }
        private void AchievementsDG_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            Timer t = new Timer(new TimerCallback(SaveAchiev));
            t.Change(1000, 0);
        }
        private void SaveAchiev(object state)
        {
            ((Timer)state).Dispose();
            foreach (var achiev in achievements)
            {
                if (achiev.PeriodId == 0)
                    Dispatcher.Invoke(() => achiev.PeriodId = PeriodCB.SelectedIndex + 1); //Обращение к UI элементу в дополнительном потоке

                var stringContent = new StringContent(JsonConvert.SerializeObject(
                    new AddOrUpdatePersonalAchievementCommand() 
                    {
                        AchievementId = achiev.AchievementId,
                        Description = achiev.Description, 
                        EmployeeId = achiev.EmployeeId,
                        PeriodId = achiev.PeriodId, 
                        Reward = achiev.Reward
                    }), Encoding.UTF8, "application/json");
                var achievmentsResponse = window.client.PostAsync(new Uri("https://localhost:44355/api/Achievements"), stringContent).Result;

                var getAchievmentsResponse = window.client.GetAsync(new Uri("https://localhost:44355/api/Achievements")).Result;
                var achievmentsResponseContent = (List<PersonalAchievement>)JsonConvert.DeserializeObject(getAchievmentsResponse.Content.ReadAsStringAsync().Result, typeof(List<PersonalAchievement>));

                achievements = achievmentsResponseContent;
                AchievementsDG.ItemsSource = achievements;
            }
        }
    }
    public class EmployeeCollection : ObservableCollection<Employee>
    {
    }
}
