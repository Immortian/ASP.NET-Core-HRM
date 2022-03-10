using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CRM.Application.Interfaces;

namespace CRM.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<CRMDBContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<ICRMDBContext>(provider =>
                provider.GetService<CRMDBContext>());

            return services;
        }
    }
}
