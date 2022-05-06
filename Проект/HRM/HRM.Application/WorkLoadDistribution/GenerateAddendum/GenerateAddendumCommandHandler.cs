using HRM.Application.Interfaces;
using HRM.Domain;
using DocumentFormat.OpenXml.Packaging;
using System.Text.RegularExpressions;

namespace HRM.Application.WorkLoadDistribution.GenerateAddendum
{
    public class GenerateAddendumCommandHandler
    {
        private IUnitOfWork unitOfWork;
        public GenerateAddendumCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task GenerateAddendum(GenerateAddendumCommand request)
        {
            Period currentPeriod = await unitOfWork.Period.GetByIdAsync(request.PeriodId);
            Employee currentEmployee = await unitOfWork.Employee.GetByIdAsync(request.EmployeeId);
            Department currentDepartment = await unitOfWork.Department.GetByIdAsync((int)currentEmployee.DepartmentId);
            Domain.CompanyData values = await unitOfWork.CompanyData.FirstAsync();

            byte[] prefabData = null;
            if (unitOfWork.File.GetByIdAsync(1).Result != null)
                prefabData = unitOfWork.File.GetByIdAsync(1).Result.Data;

            string fileName = currentEmployee.Passport.Surname + " " + unitOfWork.Period.GetDateFromPeriod(currentPeriod).ToString("Y") + ".docx";
            string path = @"wwwroot/files/" + fileName;

            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                await stream.WriteAsync(prefabData, 0, prefabData.Length);
            }

            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(path, true))
            {
                string docText = null;
                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }
                Regex regexInterview = new Regex(@"InterviewDate");
                Regex regexPeriod = new Regex(@"PeriodDate");
                Regex regexCName = new Regex(@"CompanyName");
                Regex regexDName = new Regex(@"DirectorName");
                Regex regexFullName = new Regex(@"FullName");
                Regex regexDepartment = new Regex(@"Department");
                Regex regexWorkLoad = new Regex(@"WorkLoad");
                Regex regexMPH = new Regex(@"MoneyPerHour");
                Regex regexCAddress = new Regex(@"CompanyAddress");
                Regex regexINN = new Regex(@"INN");
                Regex regexKPP = new Regex(@"KPP");
                Regex regexBIK = new Regex(@"BIK");
                Regex regexPAcc = new Regex(@"PAcc");
                Regex regexCAcc = new Regex(@"CAcc");
                Regex regexBank = new Regex(@"Bank");
                Regex regexPSer = new Regex(@"PSerial");
                Regex regexPNum = new Regex(@"PNumber");
                Regex regexInit = new Regex(@"InitName");
                Regex regexAddress = new Regex(@"Address");

                docText = regexInterview.Replace(docText, currentEmployee.Interview.Date.ToString("d"));
                docText = regexPeriod.Replace(docText, unitOfWork.Period.GetDateFromPeriod(currentPeriod).ToString("Y"));
                docText = regexCName.Replace(docText, values.CompanyName);
                docText = regexDName.Replace(docText, values.DirectorName);
                docText = regexFullName.Replace(docText, unitOfWork.Employee.GetFullName(currentEmployee));
                docText = regexDepartment.Replace(docText, currentDepartment.Direction);
                docText = regexWorkLoad.Replace(docText, request.WorkLoad.ToString());
                docText = regexMPH.Replace(docText, currentDepartment.TotalMoneyPerHour.ToString());
                docText = regexCAddress.Replace(docText, values.CompanyAddress);
                docText = regexINN.Replace(docText, values.INN);
                docText = regexKPP.Replace(docText, values.KPP);
                docText = regexBIK.Replace(docText, values.BIK);
                docText = regexPAcc.Replace(docText, values.PAcc);
                docText = regexCAcc.Replace(docText, values.CAcc);
                docText = regexBank.Replace(docText, values.Bank);
                docText = regexPSer.Replace(docText, currentEmployee.Passport.PassportSerial.ToString());
                docText = regexPNum.Replace(docText, currentEmployee.Passport.PassportNumber.ToString());
                docText = regexInit.Replace(docText, unitOfWork.Employee.GetInits(currentEmployee));
                docText = regexAddress.Replace(docText, unitOfWork.Employee.GetAddress(currentEmployee));

                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }
            }
            var data = await System.IO.File.ReadAllBytesAsync(path);

            await unitOfWork.File.CreateAsync(
            new Domain.File
            {
                Name = fileName,
                PeriodId = currentPeriod.PeriodId,
                EmployeeId = currentEmployee.EmployeeId,
                DepartmentId = currentEmployee.DepartmentId,
                Data = data
            });
        }
    }
}
