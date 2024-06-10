using Microsoft.EntityFrameworkCore;
using TechnicalTest.Infrastructure.Data;

namespace TechnicalTest.Api.Extensions
{
    public static class DatabaseExtension
    {
        public static void AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataEntities>(options =>
              options.UseSqlServer(
                  configuration.GetConnectionString("DataEntities"), b => b.MigrationsAssembly("TechnicalTest.Api")));

            services.AddScoped<DbContext, DataEntities>();
        }
    }
}
