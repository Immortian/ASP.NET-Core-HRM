using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Quartz;
using HRM.Application.Salary;

namespace HRM.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                q.AddJob<MonthResultSalaryHandler>(opt => opt.WithIdentity("summrize"))
                .AddTrigger(opts => opts
                    .ForJob("summrize")
                    .WithIdentity("summrizeTrigger")
                    .WithSchedule(CronScheduleBuilder.MonthlyOnDayAndHourAndMinute(1, 6, 0)
                    .InTimeZone(TimeZoneInfo.Local))
                    );
            });
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
