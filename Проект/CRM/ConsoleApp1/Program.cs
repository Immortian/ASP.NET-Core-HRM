// See https://aka.ms/new-console-template for more information
using CRM.Persistence;

UnitOfWork context = new UnitOfWork(new CRMContext());
var candidates = context.Candidate.GetInterviewed();
foreach (var candidate in candidates)
{
    Console.WriteLine(candidate.Passport.Name);
}