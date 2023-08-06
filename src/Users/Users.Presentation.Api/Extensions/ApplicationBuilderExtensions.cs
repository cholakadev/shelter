using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Users.Presentation.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>Add Swagger.</summary>
        public static void UseCustomSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var groupName in provider.ApiVersionDescriptions.Select(it => it.GroupName))
                {
                    options.SwaggerEndpoint($"/swagger/{groupName}/swagger.json", groupName.ToUpperInvariant());
                }
            });
        }
    }
}
