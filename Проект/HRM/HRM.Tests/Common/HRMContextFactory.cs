using HRM.Domain;
using HRM.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace HRM.Tests.Common
{
    public class HRMContextFactory
    {
        public static HRMDBContext Create()
        {
            var option = new DbContextOptionsBuilder<HRMDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new HRMDBContext(option);
            context.Database.EnsureCreated();

            
            context.ContactData.AddRange(
                new ContactData
                {
                    ContactDataId = 1,
                    Email = "email@mail.em",
                    PhoneNumber = "89998441515"
                },
                new ContactData
                {
                    ContactDataId = 2,
                    Email = "email@mail.em",
                    PhoneNumber = "89998441515"
                },
                new ContactData
                {
                    ContactDataId = 3,
                    Email = "email@mail.em",
                    PhoneNumber = "89998441515"
                });
            context.PassportInfos.AddRange(
                new PassportInfo
                {
                    PassportId = 1,
                    PassportSerial = 9131,
                    PassportNumber = 193413,
                    Name = "Name",
                    Surname = "Surname",
                    State = "State",
                    Country = "Country",
                    City = "City",
                    Street = "Street",
                    House = 1,
                    Buinding = 1,
                    Apartment = 1
                },
                new PassportInfo
                {
                    PassportId = 2,
                    PassportSerial = 9131,
                    PassportNumber = 193413,
                    Name = "Name",
                    Surname = "Surname",
                    State = "State",
                    Country = "Country",
                    City = "City",
                    Street = "Street",
                    House = 1,
                    Buinding = 1,
                    Apartment = 1
                },
                new PassportInfo
                {
                    PassportId = 3,
                    PassportSerial = 9131,
                    PassportNumber = 193413,
                    Name = "Name",
                    Surname = "Surname",
                    State = "State",
                    Country = "Country",
                    City = "City",
                    Street = "Street",
                    House = 1,
                    Buinding = 1,
                    Apartment = 1
                });
            context.Candidates.AddRange(
                new Candidate
                {
                    ContactDataId = 1,
                    PassportId = 1,
                    CandidateId = 1,
                    Education = "Edu",
                    ExpirienseYears = 1
                },
                new Candidate
                {
                    ContactDataId = 2,
                    PassportId = 2,
                    CandidateId = 2,
                    Education = "Edu",
                    ExpirienseYears = 1
                },
                new Candidate
                {
                    ContactDataId = 3,
                    PassportId = 3,
                    CandidateId = 3,
                    Education = "Edu",
                    ExpirienseYears = 1
                });
            context.Departments.AddRange(
                new Department
                {
                    DepartmentId = 1,
                    EmployeeCount = 1,
                    Direction = "Direction",
                    BasicMoneyPerHour = 300,
                    TotalMoneyPerHour = 300
                },
                new Department
                {
                    DepartmentId = 2,
                    EmployeeCount = 1,
                    Direction = "Direction",
                    BasicMoneyPerHour = 300,
                    TotalMoneyPerHour = 300
                });
            context.Interviews.AddRange(
                new Interview
                {
                    InterviewId = 1,
                    CandidateId = 1,
                    Date = DateTime.Now,
                    IsPassed = true
                },
                new Interview
                {
                    InterviewId = 2,
                    CandidateId = 2,
                    Date = DateTime.Now,
                    IsPassed = true
                });
            context.Employees.AddRange(
                new Employee
                {
                    EmployeeId = 1,
                    DepartmentId = 1,
                    InterviewId = 1,
                    PassportId = 1,
                    ContactDataId = 1,
                    Active = true,
                    AuthorizationCode = Guid.NewGuid().ToString()
                },
                new Employee
                {
                    EmployeeId = 2,
                    DepartmentId = 2,
                    InterviewId = 2,
                    PassportId = 2,
                    ContactDataId = 2,
                    Active = true,
                    AuthorizationCode = Guid.NewGuid().ToString()
                });
            context.Periods.Add(
                new Period
                {
                    PeriodId = 1,
                    Month = 1,
                    Year = 2022,
                    TotalWorkLoadHours = 240
                });
            context.DepartmentWorkLoads.AddRange(
                new DepartmentWorkLoad
                {
                    ScheduleId = 1,
                    DepartmentId = 1,
                    PeriodId = 1,
                    WorkLoad = 120,
                    IsEqualOrMore = false,
                    WorkedHours = 100
                },
                new DepartmentWorkLoad
                {
                    ScheduleId = 2,
                    DepartmentId = 2,
                    PeriodId = 1,
                    WorkLoad = 120,
                    WorkedHours = 120,
                    IsEqualOrMore = true
                });
            context.EmployeeWorkLoads.AddRange(
                new EmployeeWorkLoad
                {
                    AddendumId = 1,
                    EmployeeId = 1,
                    PeriodId = 1,
                    WorkLoadHours = 120,
                    WorkedHours = 100
                },
                new EmployeeWorkLoad
                {
                    AddendumId = 2,
                    EmployeeId = 2,
                    PeriodId = 1,
                    WorkLoadHours = 120,
                    WorkedHours = 140
                });
            context.CompanyData.Add(
                new CompanyData
                {
                    DirectorName = "Директор",
                    CompanyName = "ООО Компания",
                    CompanyAddress = "Аддресс",
                    Bank = "Sberbank",
                    BIK = "1234567890",
                    CAcc = "1234567890",
                    PAcc = "1234567890",
                    INN = "1234567890",
                    KPP = "1234567890"
                });
            byte[] fileBytes;
            FileStream file = new FileStream(@"wwwroot/files/prefab.docx", FileMode.Open, FileAccess.Read);
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }
            context.Files.Add(
                new Domain.File
                {
                    Id = 1,
                    Name = "prefab.docx",
                    Data = fileBytes
                });
            context.SaveChanges();
            return context;
        }
        public static void Destroy(HRMDBContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
