using Hotels.Core.Services;
using Hotels.Infrastructure.Database;
using Hotels.Services;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Presentation.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HotelsDbContext>(options =>
            {
                var connectionString = configuration["HotelsDbConnectionString"];

                options.UseSqlServer(connectionString);
            });
        }

        public static void AddServicesConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IHotelService, HotelService>();
        }

        public static void AddRepositoriesConfiguration(this IServiceCollection services)
        {
            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
