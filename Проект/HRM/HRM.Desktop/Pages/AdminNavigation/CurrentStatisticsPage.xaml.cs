using HRM.Desktop.Model;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace HRM.Desktop.Pages.AdminNavigation
{
    /// <summary>
    /// Логика взаимодействия для CurrentStatisticsPage.xaml
    /// </summary>
    public partial class CurrentStatisticsPage : Page
    {
        readonly MainWindow window;
        Period previousPeriod;
        Period currentPeriod;
        List<Period> periodList;
        public CurrentStatisticsPage(MainWindow mainWindow)
        {
            InitializeComponent();
            window = mainWindow;
            window.currentPage = "PeriodStatisticsPage";
            window.sizeController.CheckWindowSize("PeriodStatisticsPage");
            GeneratePage();

            DataContext = this;
        }
        private void GeneratePage()
        {
            FillPeriodsCB();
            GetPeriodContent();
            GetDepartmentContent();
            GetEmployeeContent();
        }
        private void FillPeriodsCB()
        {
            var periodsResponse = window.client.GetAsync(new Uri("https://localhost:44355/api/Statistics/Period")).Result;
            var periodsResponseContent = (List<Period>)JsonConvert.DeserializeObject(periodsResponse.Content.ReadAsStringAsync().Result, typeof(List<Period>));
            periodList = periodsResponseContent;
            List<string> PeriodCBsource = new List<string>();
            foreach (var i in periodsResponseContent)
            {
                PeriodCBsource.Add(new DateTime(i.Year, i.Month, 1).Date.ToString("Y"));
            }
            PeriodCB.ItemsSource = PeriodCBsource;

            PeriodCB.SelectedIndex = PeriodCB.Items.Count - 1;
        }
        public SeriesCollection PeriodSeriesCollection { get; set; }
        public string[] PeriodChartLabels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        private void GetPeriodContent()
        {
            var periodResponse = window.client.GetAsync(new Uri("https://localhost:44355/api/Statistics/Period/Current")).Result;
            var periodResponseContent = (Period)JsonConvert.DeserializeObject(periodResponse.Content.ReadAsStringAsync().Result, typeof(Period));

            var previousPeriodResponse = window.client.GetAsync(new Uri("https://localhost:44355/api/Statistics/Period/Previous")).Result;
            var previousPeriodResponseContent = (Period)JsonConvert.DeserializeObject(previousPeriodResponse.Content.ReadAsStringAsync().Result, typeof(Period));

            currentPeriod = periodResponseContent;
            CompanyWorkTimeLabel.Content = $"Цель на текущий период: {currentPeriod.TotalWorkLoadHours}";
            previousPeriod = previousPeriodResponseContent;
            if (previousPeriod != null)
            {
                int diff = currentPeriod.TotalWorkLoadHours - previousPeriod.TotalWorkLoadHours;
                if (diff == 0)
                    PeriodStatsLabel.Content = "Столько же часов, сколько и в прошлом месяце";
                else
                    PeriodStatsLabel.Content = $"На {(diff > 0 ? diff + " часов больше, чем в прошлом месяце" : -diff + " часов меньше, чем в прошлом месяце")}";
            }
            else
                PeriodStatsLabel.Content = "Это первый зафиксированный период";
            PeriodCB.SelectedItem = new DateTime(currentPeriod.Year, currentPeriod.Month, 1).Date.ToString("Y");

            PeriodSeriesCollection = new SeriesCollection();
            var PeriodList = periodList.Where(x => x.PeriodId <= currentPeriod.PeriodId).ToArray();
            PeriodSeriesCollection.Add(new LineSeries() { Values = new ChartValues<double>(PeriodList.Select(x => (Double)(x.TotalWorkLoadHours)).ToArray()), PointGeometry = DefaultGeometries.Circle, Title = null });
            //YFormatter = value => value.ToString("");
            PeriodChartLabels = new string[PeriodList.Length];

            for (int i = 0; i < PeriodList.Length; i++)
            {
                PeriodChartLabels[i] = "" + new DateTime(PeriodList[i].Year, PeriodList[i].Month, 1).Date.ToString("Y");
            }

            PeriodLineChart.Series = PeriodSeriesCollection;
        }
        private void GetPeriodContent(Period period)
        {
            currentPeriod = period;
            CompanyWorkTimeLabel.Content = $"Цель на период: {currentPeriod.TotalWorkLoadHours}";
            if (previousPeriod != null)
            {
                int diff = currentPeriod.TotalWorkLoadHours - previousPeriod.TotalWorkLoadHours;
                if (diff == 0)
                    PeriodStatsLabel.Content = "Столько же часов, сколько и в прошлом месяце";
                else
                    PeriodStatsLabel.Content = $"На {(diff > 0 ? diff + " часов больше, чем в прошлом месяце" : -diff + " часов меньше, чем в прошлом месяце")}";
            }
            else
                PeriodStatsLabel.Content = "Это первый зафиксированный период";



            PeriodSeriesCollection = new SeriesCollection();
            var PeriodList = periodList.Where(x => x.PeriodId <= currentPeriod.PeriodId).ToArray();
            PeriodSeriesCollection.Add(new LineSeries() { Values = new ChartValues<double>(PeriodList.Select(x => (Double)(x.TotalWorkLoadHours)).ToArray()), PointGeometry = DefaultGeometries.Circle, Title = null });
            //YFormatter = value => value.ToString("");
            PeriodChartLabels = new string[PeriodList.Length];
            for (int i = 0; i < PeriodList.Length; i++)
            {
                PeriodChartLabels[i] = "" + new DateTime(PeriodList[i].Year, PeriodList[i].Month, 1).Date.ToString("Y");
            }
            PeriodLineChart.Series = PeriodSeriesCollection;
        }
        public SeriesCollection DepartmentSeriesCollection { get; set; }
        private void GetDepartmentContent()
        {
            var currentDepWorkLoadResponse = window.client.GetAsync(new Uri("https://localhost:44355/api/Statistics/WorkLoad/Department/" + currentPeriod.PeriodId)).Result;
            var periodResponseContent = (List<DepartmentWorkLoad>)JsonConvert.DeserializeObject(currentDepWorkLoadResponse.Content.ReadAsStringAsync().Result, typeof(List<DepartmentWorkLoad>));


            var current_Department_Work_Loads = periodResponseContent;
            DepartmentSeriesCollection = new SeriesCollection();
            foreach (var dep in current_Department_Work_Loads)
            {
                DepartmentSeriesCollection.Add(new PieSeries() { Title = dep.Department.Direction, Values = new ChartValues<ObservableValue> { new ObservableValue(dep.WorkLoad) } });
            }
            DepartmentPie.Series = DepartmentSeriesCollection;
            var STDDepartmentWorkLoad = current_Department_Work_Loads.Sum(x => x.WorkLoad);
            STDDepartmentLabel.Content = $"Средняя занятость отдела:{STDDepartmentWorkLoad / (current_Department_Work_Loads.Count == 0 ? 1 : current_Department_Work_Loads.Count)}";
        }
        private void GetEmployeeContent()
        {
            var currentEmpWorkLoadResponse = window.client.GetAsync(new Uri("https://localhost:44355/api/Statistics/WorkLoad/Employee/" + currentPeriod.PeriodId)).Result;
            var periodResponseContent = (List<EmployeeWorkLoad>)JsonConvert.DeserializeObject(currentEmpWorkLoadResponse.Content.ReadAsStringAsync().Result, typeof(List<EmployeeWorkLoad>));

            var current_Employee_Work_Loads = periodResponseContent;
            EmployeeDG.ItemsSource = current_Employee_Work_Loads;
            var STDEmployeeWorkLoad = current_Employee_Work_Loads.Sum(x => x.WorkLoadHours);
            STDEmployeeLabel.Content = $"Средняя занятость сотрудника:{STDEmployeeWorkLoad / (current_Employee_Work_Loads.Count == 0 ? 1 : current_Employee_Work_Loads.Count)}";
        }

        private void PeriodCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Id = PeriodCB.SelectedIndex + 1;
            var periodResponse = window.client.GetAsync(new Uri(@"https://localhost:44355/api/Statistics/Period/" + Id)).Result;
            var periodResponseContent = (Period)JsonConvert.DeserializeObject(periodResponse.Content.ReadAsStringAsync().Result, typeof(Period));

            GetPeriodContent(periodResponseContent);
            GetDepartmentContent();
            GetEmployeeContent();
        }
    }
}
