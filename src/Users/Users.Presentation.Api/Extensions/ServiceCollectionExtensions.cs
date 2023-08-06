using Users.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;                        
using Users.Infrastructure.Repositories;
using Users.Services;
using Users.Core.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Users.Presentation.Api.Filters;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc;
using Users.Core.Models.Settings;
using SharedKernel.Infrastructure.Repositories;
using Users.Core.Repositories;

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
            services.AddTransient<IUserRepository, UserRepository>();
        }

        /// <summary>Configure api versioning.</summary>
        public static void AddCustomVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddVersionedApiExplorer(
                options =>
                {
                    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });
        }

        /// <summary>Configure CORS.</summary>
        public static void AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var applicationSettings = configuration.GetSection(nameof(ApplicationSettings)).Get<ApplicationSettings>();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowClient", policy =>
                {
                    policy.WithOrigins(applicationSettings.ClientLocations).AllowAnyHeader().AllowAnyMethod();
                });
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
                options.AddPolicy("AllowLocalClient", policy =>
                {
                    policy.WithOrigins(applicationSettings.LocalDevelopmentLocation).AllowAnyHeader().AllowAnyHeader();
                    policy.WithOrigins(applicationSettings.ClientLocations).AllowAnyHeader().AllowAnyMethod();
                });
            });
        }

        /// <summary>Configure Swagger.</summary>
        public static void AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                // note: need a temporary service provider here because one has not been created yet
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName, new OpenApiInfo
                    {
                        Version = description.GroupName,
                        Title = "Shelter API",
                        Description = "Web API for Shelter project.",
                    });
                }


                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme.",
                });


                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Bearer",
                            In   = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Id   = "Bearer",
                                Type = ReferenceType.SecurityScheme,
                            },
                        },
                        new List<string>()
                    },
                });


                options.OperationFilter<SwaggerDefaultValues>();

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                options.IncludeXmlComments(xmlPath);
            });
        }
    }
}
