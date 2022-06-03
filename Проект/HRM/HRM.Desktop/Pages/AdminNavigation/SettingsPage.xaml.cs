using HRM.Desktop.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace HRM.Desktop.Pages.AdminNavigation
{
    /// <summary>
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        MainWindow window;
        CompanyData companyData;
        public SettingsPage(MainWindow mainWindow)
        {
            InitializeComponent();
            window = mainWindow;

            var companyDataResponse = window.client.GetAsync(new Uri("https://localhost:44355/api/CompanyData")).Result;
            var companyDataResponseContent = (CompanyData)JsonConvert.DeserializeObject(companyDataResponse.Content.ReadAsStringAsync().Result, typeof(CompanyData));
            companyData = companyDataResponseContent;
            CompanyNameTB.Text = companyData.CompanyName;
            DirectorTB.Text = companyData.DirectorName;
            AddressTB.Text = companyData.CompanyAddress;
            INNTB.Text = companyData.INN;
            KPPTB.Text = companyData.KPP;
            BIKTB.Text = companyData.BIK;
            PaymentAccTB.Text = companyData.PAcc;
            CorrAccTB.Text = companyData.CAcc;
            BankTB.Text = companyData.Bank;
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            window.MainFrame.GoBack();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            companyData.CompanyName = CompanyNameTB.Text;
            companyData.DirectorName = DirectorTB.Text;
            companyData.CompanyAddress = AddressTB.Text;
            companyData.INN = INNTB.Text;
            companyData.KPP = KPPTB.Text;
            companyData.BIK = BIKTB.Text;
            companyData.PAcc = PaymentAccTB.Text;
            companyData.CAcc = CorrAccTB.Text;
            companyData.Bank = BankTB.Text;
            var companyContent = new StringContent(JsonConvert.SerializeObject(
                new Commands.FillCompanyDataCommand
                {
                    Bank = companyData.Bank,
                    BIK = companyData.BIK,
                    PAcc = companyData.PAcc,
                    INN = companyData.INN,
                    KPP = companyData.KPP,
                    CAcc = companyData.CAcc,
                    CompanyAddress = companyData.CompanyAddress,
                    CompanyName = companyData.CompanyName,
                    DirectorName = companyData.DirectorName
                }), Encoding.UTF8, "application/json");
            var companyDataResponse = window.client.PostAsync(new Uri("https://localhost:44355/api/Statistics/CompanyData"), companyContent).Result;
        }

        private void OpenDialogButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fileDialog = new FolderBrowserDialog();
            fileDialog.ShowDialog();
            if (fileDialog.SelectedPath != "")
                FilePathTB.Text = fileDialog.SelectedPath;
        }
    }
}
