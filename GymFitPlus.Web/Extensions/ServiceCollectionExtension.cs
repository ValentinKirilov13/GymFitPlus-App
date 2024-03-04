using GymFitPlus.Infrastructure.Data;
using GymFitPlus.Infrastructure.Data.Models;
using GymFitPlus.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;
using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services
                .AddScoped<IAccountService, AccountService>();
            services
               .AddScoped<IExcersiseServices, ExcersiseServices>();

            return services;
        }

        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration config)
        {
            string connectionString = config.GetConnectionString("DefaultConnection");

            services
                .AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services
                .AddScoped<IRepository, Repository>();

            services
                .AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }

        public static IServiceCollection AddApplicationIdentity(this IServiceCollection services)
        {
            services
                .AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;

                })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }
    }
}
