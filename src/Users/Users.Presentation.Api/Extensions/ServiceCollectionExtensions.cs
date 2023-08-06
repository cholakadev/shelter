using Users.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

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
    }
}
