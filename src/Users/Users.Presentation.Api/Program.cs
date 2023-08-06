using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Users.Presentation.Api.Configuration;
using Users.Presentation.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("AppConfig");
builder.Configuration.AddAzureAppConfiguration(connectionString);

builder.Services.AddAuthorization();
builder.Services.AddCustomVersioning();
builder.Services.AddCustomSwagger();
builder.Services.AddDatabase(builder.Configuration, builder.Environment);
builder.Services.AddRepositoriesConfiguration();
builder.Services.AddServicesConfiguration();
builder.Services.AddCorsConfiguration(builder.Configuration);

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
