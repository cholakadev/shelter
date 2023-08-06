//using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using SharedKernel.Configuration;
using SharedKernel.Extensions;
using SharedKernel.Models.Settings;
using Users.Infrastructure.Database;
using Users.Presentation.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("AppConfig");
builder.Configuration.AddAzureAppConfiguration(connectionString);

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection(nameof(TokenSettings)));

builder.Services.AddAuthorization();
builder.Services.AddSharedAuthentication(builder.Configuration);
builder.Services.AddSharedCustomVersioning();
builder.Services.AddSharedCustomSwagger();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddRepositoriesConfiguration();
builder.Services.AddServicesConfiguration();
builder.Services.AddSharedCorsConfiguration(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseCustomSwagger(app.Services.GetRequiredService<IApiVersionDescriptionProvider>());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => Results.Redirect("swagger", true)).ExcludeFromDescription();

app.Run();
