// See https://aka.ms/new-console-template for more information
using HRM.Domain;
using HRM.Persistence;
using HRM.Persistence.Repositories.Base;

HRMDBContext rawContext = new HRMDBContext();
UnitOfWork context = new UnitOfWork(rawContext);
//RepositoryBase<Candidate> baseContext = new RepositoryBase<Candidate>(rawContext);
var candidates = context.Candidate.GetInterviewed().Result;
//var rawdata = rawContext.Candidates.ToList();
//var basedata = baseContext.GetAllAsync();

foreach (var candidate in candidates)
{
    Console.WriteLine(candidate.Passport.Name);
}