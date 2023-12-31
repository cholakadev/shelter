﻿using Users.Infrastructure.Repositories;
using Users.Services;
using Users.Core.Services;
using SharedKernel.Infrastructure.Repositories;
using Users.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Users.Infrastructure.Database;

namespace Users.Presentation.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UsersDbContext>(options =>
            {
                var connectionString = configuration["UsersDbConnectionString"];

                options.UseSqlServer(connectionString);
            });
        }

        public static void AddServicesConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IUserService, UserService>();
        }

        public static void AddRepositoriesConfiguration(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
