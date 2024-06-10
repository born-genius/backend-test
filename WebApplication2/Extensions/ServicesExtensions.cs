using Serilog;
using TechnicalTest.Core.Interfaces.ExternalServices;
using TechnicalTest.Core.Interfaces.Managers;
using TechnicalTest.Core.Interfaces.Repositories;
using TechnicalTest.Core.Managers;
using TechnicalTest.Core.Models;
using TechnicalTest.Infrastructure.ExternalServices;
using TechnicalTest.Infrastructure.Repositories;

namespace TechnicalTest.Api.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddAppServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            //Managers
            services.AddScoped<ICustomerManager, CustomerManager>();
            services.AddScoped<IBankManager, BankManager>();

            //Repositories
            #region Repositories
            services.AddScoped<IOtpRepository, OtpRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            #endregion

            //Third party service
            #region service
            services.AddScoped<IBankService, BankService>();
            #endregion service

            //Configurations
            #region Configurations
            services.AddSingleton(configuration.GetSection("ApiOptions").Get<ApiOptions>()); 
            #endregion

        }
        public static void ResiterLogger(this IServiceCollection services, IConfiguration configuration)
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            services.AddSingleton(logger);
        }
    }
}
