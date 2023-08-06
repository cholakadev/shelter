using Users.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Users.Core.Repositories;
using Users.Infrastructure.Repositories;
using Users.Services;
using Users.Core.Services;

namespace Users.Presentation.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private const string UserConnectionKey = "UsersDbConnectionString";

        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddDbContext<UsersDbContext>(options =>
            {
                var connectionString = configuration[UserConnectionKey];

                options.UseSqlServer(connectionString);
                if (environment.IsDevelopment())
                {
                    options
                        .LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuted })
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors();
                }
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
        }
    }
}
