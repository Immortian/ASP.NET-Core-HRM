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
        List<DGSource> DGValues;
        public DocumentDownloadPage()
        {
            InitializeComponent();
            FormatCB.ItemsSource = new List<string>() { "DOC", "DOCX", "PDF", "XML" };
            PeriodCB.ItemsSource = new List<string>() { "за все время", "апрель 2022", "май 2022" };
            DepartmentCB.ItemsSource = new List<string>() { "все отделы", "Техподдержка", "Обслуживание" };
            PeriodCB.SelectedIndex = 0;
            DepartmentCB.SelectedIndex = 0;
            FormatCB.SelectedIndex = 0;
             DGValues = new List<DGSource>() 
            { 
                new DGSource()
                {
                    MounthOfYear = "апрель 2022",
                    DepartmentDirection = "Техподдержка",
                    EmployeeInits = "Яскунова М.Д."
                },
                new DGSource()
                {
                    MounthOfYear = "май 2022",
                    DepartmentDirection = "Техподдержка",
                    EmployeeInits = "Яскунова М.Д."
                },
                new DGSource()
                {
                    MounthOfYear = "апрель 2022",
                    DepartmentDirection = "Обслуживание",
                    EmployeeInits = "Пузанов Ф.С."
                },
                new DGSource()
                {
                    MounthOfYear = "май 2022",
                    DepartmentDirection = "Обслуживание",
                    EmployeeInits = "Яскунова М.Д."
                },
                new DGSource()
                {
                    MounthOfYear = "апрель 2022",
                    DepartmentDirection = "Техподдержка",
                    EmployeeInits = "Ширяев Ф.А."
                },
                new DGSource()
                {
                    MounthOfYear = "май 2022",
                    DepartmentDirection = "Техподдержка",
                    EmployeeInits = "Ширяев Ф.А."
                },
                new DGSource()
                {
                    MounthOfYear = "апрель 2022",
                    DepartmentDirection = "Обслуживание",
                    EmployeeInits = "Цицина Ю.Ф."
                },
                new DGSource()
                {
                    MounthOfYear = "май 2022",
                    DepartmentDirection = "Обслуживание",
                    EmployeeInits = "Цицина Ю.Ф."
                }
            }.OrderBy(x => x.MounthOfYear)
            .OrderBy(x =>x.DepartmentDirection)
            .OrderBy(x=>x.EmployeeInits).ToList();
            FilesDG.ItemsSource = DGValues;
        }

        private void FilterChanged(object sender, SelectionChangedEventArgs e)
        {
            var tempValues = DGValues;
            if (PeriodCB.SelectedIndex > 0)
                tempValues = tempValues.Where(x => x.MounthOfYear == PeriodCB.SelectedItem.ToString()).ToList();
            if(DepartmentCB.SelectedIndex > 0)
                tempValues = tempValues.Where(x=>x.DepartmentDirection == DepartmentCB.SelectedItem.ToString()).ToList();
            FilesDG.ItemsSource = tempValues;
        }
    }
    internal class DGSource
    {
        public string MounthOfYear { get; set; }
        public string DepartmentDirection { get; set; }
        public string EmployeeInits { get; set; }
    }
}
