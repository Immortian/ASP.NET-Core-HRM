using HRM.Application.Interfaces;
using HRM.Domain;
using Quartz;
using Quartz.Impl;

namespace HRM.Application.Dismissing
{
    public class DismissingCommandHandler : IJob
    {
        private IUnitOfWork unitOfWork;
        int EmployeeId;
        public DismissingCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task Dismiss(DismissingCommand request)
        {
            EmployeeId = request.EmployeeId;
            var PassportId = unitOfWork.Employee.GetByIdAsync(EmployeeId).Result.PassportId;
            await unitOfWork.Dismissal.CreateAsync(
                new Dismissal
                {
                    PassportId = PassportId,
                    DocumentDate = DateTime.Now,
                    DismissalDate = DateTime.Now.AddDays(14),
                    DismissalReason = request.Reason,
                    Payments = request.Payments
                });
            if (request.Reason == "По собственоому желанию")
            {
                IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
                await scheduler.Start();

                IJobDetail job = JobBuilder.Create<DismissingCommandHandler>().Build();

                ITrigger trigger = TriggerBuilder.Create()  // создаем триггер
                   .WithIdentity("trigger1", "group1")     // идентифицируем триггер с именем и группой
                   .StartAt(DateBuilder.FutureDate(14, IntervalUnit.Day))   // запуск в конкретную дату 14 дней
                   .Build();                               // создаем триггер

                await scheduler.ScheduleJob(job, trigger);        // начинаем выполнение работы
            }
            else
                await Execute(null);
        }
        public async Task Execute(IJobExecutionContext? context)
        {
            await unitOfWork.Employee.DeleteAsync(unitOfWork.Employee.GetByIdAsync(EmployeeId).Result);
            await unitOfWork.Save();
        }
    }
}
    
