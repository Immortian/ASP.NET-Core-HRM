using HRM.Application.BuisnessLogic.Base;
using HRM.Domain;

namespace HRM.Application.BuisnessLogic
{
    public interface IPeriodRepository : IRepositoryBase<Period>
    {
        Period Next();
        Period CreateFirst();
        Period GetPeriodByDate(DateTime date);
        public DateTime GetDateFromPeriod(Period period);
    }
}
