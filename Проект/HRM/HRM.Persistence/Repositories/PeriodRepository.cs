using HRM.Application.BuisnessLogic;
using HRM.Domain;
using HRM.Persistence.Repositories.Base;

namespace HRM.Persistence.Repositories
{
    public class PeriodRepository : RepositoryBase<Period>, IPeriodRepository
    {
        public PeriodRepository(HRMDBContext context) : base(context)
        {

        }

        public Period Next()
        {
            if (!context.Periods.Any())
                return CreateFirst();
            Period last = context.Periods.OrderBy(x => x.PeriodId).LastOrDefault();
            Period next = new Period()
            {
                Year = last.Month == 12 ? last.Year + 1 : last.Year,
                Month = last.Month == 12 ? 1 : last.Month + 1
            };
            return next;
        }
        public Period CreateFirst()
        {
            Period first = new Period()
            {
                Year = DateTime.Now.Year,
                Month = DateTime.Now.Month
            };
            return first;
        }

        public Period GetPeriodByDate(DateTime date)
        {
            return context.Periods.Where(x => x.Year == date.Year && x.Month == date.Month).FirstOrDefault();
        }

        public DateTime GetDateFromPeriod(Period period)
        {
            return new DateTime(period.Year, period.Month, 1);
        }
    }
}
