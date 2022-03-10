// See https://aka.ms/new-console-template for more information
using CRM.Domain;
using CRM.Persistence;
using CRM.Persistence.Repositories.Base;

CRMDBContext rawContext = new CRMDBContext();
UnitOfWork context = new UnitOfWork(rawContext);
//RepositoryBase<Candidate> baseContext = new RepositoryBase<Candidate>(rawContext);
var candidates = context.Candidate.GetInterviewed();
//var rawdata = rawContext.Candidates.ToList();
//var basedata = baseContext.GetAllAsync();

foreach (var candidate in candidates)
{
    Console.WriteLine(candidate.Passport.Name);
}