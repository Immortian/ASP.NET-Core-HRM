// See https://aka.ms/new-console-template for more information
using HRM.Application.Interfaces;
using HRM.Application.WorkLoadDistribution.CreateDistribution;
using HRM.Domain;
using HRM.Persistence;
using HRM.Persistence.Repositories.Base;

HRMDBContext rawContext = new HRMDBContext();
IUnitOfWork context = new UnitOfWork(rawContext);
//RepositoryBase<Candidate> baseContext = new RepositoryBase<Candidate>(rawContext);
var candidates = context.Candidate.GetInterviewed().Result;
//var rawdata = rawContext.Candidates.ToList();
//var basedata = baseContext.GetAllAsync();

//foreach (var candidate in candidates)
//{
//    Console.WriteLine(candidate.Passport.Name);
//}
CreateDistributionCommandHandler distrib = new CreateDistributionCommandHandler(context);
await distrib.Distribute(new CreateDistributionCommand()
{
    MonthlyHours = 9000,
    Options = new List<HRM.Application.WorkLoadDistribution.DistributionOption>
    {
        new HRM.Application.WorkLoadDistribution.DistributionOption()
        {
            StaticHours = 4200,
            DepartmentId = 1,
        },
        new HRM.Application.WorkLoadDistribution.DistributionOption()
        {
            StaticHours = 4800,
            DepartmentId = 2
        }
    }
});