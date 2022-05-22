using HRM.Desktop.Commands;
using HRM.Desktop.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
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

namespace HRM.Desktop.Pages.AdminNavigation
{
    /// <summary>
    /// Логика взаимодействия для CreateNextPeriodPage.xaml
    /// </summary>
    public partial class CreateNextPeriodPage : Page
    {
        readonly MainWindow window;
        public List<DistributionOption> options;
        Period currentPeriod;
        Period newPeriod;
        public CreateNextPeriodPage(MainWindow mainWindow)
        {
            InitializeComponent();
            window = mainWindow;
            GetPeriodContext();
            
            options = new List<DistributionOption>();
            //options.Add(new DistributionOption());
            DepartmentContextDG.ItemsSource = options;
            var activeEmployeeResponse = window.client.GetAsync(new Uri("https://localhost:44355/api/Employee")).Result;
            var activeEmployeeResponseContent = (List<Employee>)JsonConvert.DeserializeObject(activeEmployeeResponse.Content.ReadAsStringAsync().Result, typeof(List<Employee>));

            int EmployeeCount = activeEmployeeResponseContent.Count;
            WorkLoadSlider.Minimum = EmployeeCount * 4 * 22; //4 часа в день, 22 рабочих дня в среднем в месяц
            WorkLoadSlider.Maximum = EmployeeCount * 10 * 22; //10 часов
            WorkLoadSlider.TickFrequency = EmployeeCount * 22 * 0.25; //Интервал по 15 минут рабочего времени в день
            WorkLoadSlider.SmallChange = EmployeeCount * 22 * 0.25;
            WorkLoadSlider.IsSnapToTickEnabled = true;
        }
        private int WorkDays(Period period)
        {
            int workDays = DateTime.DaysInMonth(period.Year, period.Month);
            for (int i = 1; i < DateTime.DaysInMonth(period.Year, period.Month) - 1; i++)
            {
                DateTime dt = new DateTime(period.Year, period.Month, i);
                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                    workDays--;
            }
            return workDays;
        }
        private void GetPeriodContext()
        {
            var currentPeriodResponse = window.client.GetAsync(new Uri("https://localhost:44355/api/Statistics/Period/Current")).Result;
            var currentPeriodResponseContent = (Period)JsonConvert.DeserializeObject(currentPeriodResponse.Content.ReadAsStringAsync().Result, typeof(Period));

            var nextPeriodResponse = window.client.GetAsync(new Uri("https://localhost:44355/api/Statistics/Period/Next")).Result;
            var nextPeriodResponseContent = (Period)JsonConvert.DeserializeObject(nextPeriodResponse.Content.ReadAsStringAsync().Result, typeof(Period));


            currentPeriod = currentPeriodResponseContent;
            newPeriod = nextPeriodResponseContent;
            if (newPeriod == null)
            {
                newPeriod = new Period();
                newPeriod.Year = currentPeriod.Month == 12 ? currentPeriod.Year + 1 : currentPeriod.Year;
                newPeriod.Month = currentPeriod.Month == 12 ? 1 : currentPeriod.Month + 1;
            }
            else
            {
                StatisticsButton.IsEnabled = true;
                DocumentsButton.IsEnabled = true;
                CreatePeriodButton.IsEnabled = false;
            }
            WorkLoadSlider.Value = newPeriod.TotalWorkLoadHours == 0 ? currentPeriod.TotalWorkLoadHours : newPeriod.TotalWorkLoadHours;
            NextPeriodLabel.Content = new DateTime(newPeriod.Year, newPeriod.Month, 1).Date.ToString("Y");
        }

        private void StatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            window.MainFrame.Navigate(new CurrentStatisticsPage(window));
        }

        private void CreatePeriodButton_Click(object sender, RoutedEventArgs e)
        {
            int WorkLoad = (int)Math.Round(WorkLoadSlider.Value);

            if (options.Count != 0)
            {
                var departmnetResponse = window.client.GetAsync(new Uri("https://localhost:44355/api/Statistics/Department")).Result;

                List<Department> departments = (List<Department>)JsonConvert.DeserializeObject(departmnetResponse.Content.ReadAsStringAsync().Result, typeof(List<Department>));
                foreach (var opt in options)
                {
                    opt.DepartmentId = departments.Where(x => x.Direction == opt.DepartmentTitle).Select(x => x.DepartmentId).FirstOrDefault();

                    var stringContent = new StringContent(JsonConvert.SerializeObject(new Commands.CreateDistributionCommand { MonthlyHours = WorkLoad, Options = options }), Encoding.UTF8, "application/json");
                    var distributionResponse = window.client.PostAsync(new Uri("https://localhost:44355/api/Distribution"), stringContent).Result;
                    if (distributionResponse.StatusCode != System.Net.HttpStatusCode.OK)
                        window.error.DBError();
                }
            }
            else
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(new Commands.CreateDistributionCommand { MonthlyHours = WorkLoad, Options = null }), Encoding.UTF8, "application/json");
                var distributionResponse = window.client.PostAsync(new Uri("https://localhost:44355/api/Distribution"), stringContent).Result;
                if (distributionResponse.StatusCode != System.Net.HttpStatusCode.OK)
                    window.error.DBError();
            }
        }

        private void DocumentsButton_Click(object sender, RoutedEventArgs e)
        {
            window.error.CheckSettingsSaved();
        }
    }
    public class DepartmentCollection : ObservableCollection<Department>
    {
    }
}
