// See https://aka.ms/new-console-template for more information
using HRM.Application.Authorization.Registration;
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
await TestRegistrationAsync();


async Task TestDistributeAsync()
{
    CreateDistributionCommandHandler distrib = new CreateDistributionCommandHandler(context);
    await distrib.Distribute(new CreateDistributionCommand()
    {
        MonthlyHours = 16000,
        Options = new List<HRM.Application.WorkLoadDistribution.DistributionOption>
        {
            new HRM.Application.WorkLoadDistribution.DistributionOption()
            {
                StaticHours = 7200,
                DepartmentId = 1,
            },
            new HRM.Application.WorkLoadDistribution.DistributionOption()
            {
                StaticHours = 8800,
                DepartmentId = 2
            }
        }
    });
}

async Task TestRegistrationAsync()
{
    RegistrationCommandHandler reg = new RegistrationCommandHandler(context);
    await reg.Registration(new RegistrationCommand()
    {
        Username = "admin",
        Password = "12345",
        AuthCode = "05f04074-a0d2-4abd-a039-72e3e8336f40"
    });
    await context.Authorization.GetAllAsync();
}