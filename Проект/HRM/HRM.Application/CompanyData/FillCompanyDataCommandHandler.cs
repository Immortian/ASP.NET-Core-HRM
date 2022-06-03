using HRM.Application.Interfaces;
using System.Text.RegularExpressions;

namespace HRM.Application.CompanyData
{
    public class FillCompanyDataCommandHandler
    {
        private IUnitOfWork unitOfWork;
        public FillCompanyDataCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task AddOrUpdate(FillCompanyDataCommand request)
        {
            var data = new Domain.CompanyData();
            Regex reg = new Regex(@"^[0-9]+$");

            
            if (unitOfWork.CompanyData.GetAllAsync().Result.Count != 0)
                data = unitOfWork.CompanyData.GetAllAsync().Result.FirstOrDefault();
            else
                if (request.CompanyName != null && request.CompanyName.Any())
                    data.CompanyName = request.CompanyName;
            
            
            if (request.CompanyAddress != null && request.CompanyAddress.Any())
                data.CompanyAddress = request.CompanyAddress;
            if (request.DirectorName != null && request.DirectorName.Any())
                data.DirectorName = request.DirectorName;
            if (request.Bank != null && request.Bank.Any())
                data.Bank = request.Bank;

            if (request.INN != null && reg.IsMatch(request.INN))
                data.INN = request.INN;
            if (request.KPP != null && reg.IsMatch(request.KPP))
                data.KPP = request.KPP;
            if (request.BIK != null && reg.IsMatch(request.BIK))
                data.BIK = request.BIK;
            if (request.CAcc != null && reg.IsMatch(request.CAcc))
                data.CAcc = request.CAcc;
            if (request.PAcc != null && reg.IsMatch(request.PAcc))
                data.PAcc = request.PAcc;

            if (unitOfWork.CompanyData.GetAllAsync().Result.Count == 0)
                await unitOfWork.CompanyData.CreateAsync(data);
            else
                await unitOfWork.CompanyData.UpdateAsync(data);
            await unitOfWork.Save();
        }
    }
}
