﻿using GymFitPlus.Infrastructure.Data;
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
               .AddScoped<IExerciseService, ExerciseService>();
            services
               .AddScoped<IFitnessProgramService, FitnessProgramService>();
            services
               .AddScoped<IWorkoutService, WorkoutService>();
            services
               .AddScoped<IStatisticService, StatisticService>();
            services
               .AddScoped<IRecipeService, RecipeService>();

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
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequiredLength = 8;
                })
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }

        public static IServiceCollection AddApplicationAuthentication(this IServiceCollection services)
        {
            services
                .ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = PathString.FromUriComponent("/Account/LogInSignUp");
                    options.AccessDeniedPath = PathString.FromUriComponent("/Home/AccessDenied");
                });

            return services;
        }
    }
}
