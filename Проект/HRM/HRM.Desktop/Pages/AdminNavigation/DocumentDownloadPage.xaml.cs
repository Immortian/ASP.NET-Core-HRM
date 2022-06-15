using HRM.Desktop.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace HRM.Desktop.Pages.AdminNavigation
{
    /// <summary>
    /// Логика взаимодействия для DocumentDownloadPage.xaml
    /// </summary>
    public partial class DocumentDownloadPage : Page
    {
        List<File> DGValues;
        MainWindow window;
        public DocumentDownloadPage(MainWindow mainWindow)
        {
            InitializeComponent();
            window = mainWindow;
            FormatCB.ItemsSource = new List<string>() { "DOC", "DOCX", "PDF", "XML" };

            var periodsResponse = window.client.GetAsync(new Uri("https://localhost:44355/api/Statistics/Period")).Result;
            var periodsResponseContent = (List<Period>)JsonConvert.DeserializeObject(periodsResponse.Content.ReadAsStringAsync().Result, typeof(List<Period>));
            var periodSource = new List<string>();
            periodSource.Add("За все время");
            foreach (var i in periodsResponseContent)
            {
                periodSource.Add(new DateTime(i.Year, i.Month, 1).Date.ToString("Y"));
            }
            PeriodCB.ItemsSource = periodSource;

            DepartmentCB.ItemsSource = new List<string>() { "все отделы", "Техподдержка", "Обслуживание" };
            PeriodCB.SelectedIndex = 0;
            DepartmentCB.SelectedIndex = 0;
            FormatCB.SelectedIndex = 0;

            var addendumResponse = window.client.GetAsync(new Uri("https://localhost:44355/api/Distribution/Files")).Result;
            var addendumResponseContent = (List<File>)JsonConvert.DeserializeObject(addendumResponse.Content.ReadAsStringAsync().Result, typeof(List<File>));
            DGValues = addendumResponseContent;
            FilesDG.ItemsSource = DGValues;
            CountLabel.Content = $"Найдено файлов: {DGValues.Count}";
        }

        private void FilterChanged(object sender, SelectionChangedEventArgs e)
        {
            var tempValues = DGValues;
            if (PeriodCB.SelectedIndex > 0)
                tempValues = tempValues.Where(x => x.PeriodId == PeriodCB.SelectedIndex).ToList();
            if(DepartmentCB.SelectedIndex > 0)
                tempValues = tempValues.Where(x=>x.DepartmentId == DepartmentCB.SelectedIndex).ToList();
            FilesDG.ItemsSource = tempValues;
            if(tempValues != null)
                CountLabel.Content = $"Найдено файлов: {tempValues.Count}";
            
        }
    }
}
