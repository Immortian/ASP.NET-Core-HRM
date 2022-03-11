using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using HRM.Application.Interfaces;

namespace HRM.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<HRMDBContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<IHRMDBContext>(provider =>
                provider.GetService<HRMDBContext>());

            return services;
        }
    }
}
