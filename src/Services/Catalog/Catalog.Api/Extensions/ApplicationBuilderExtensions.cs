using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Catalog.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.API V1");
            });

            return app;
        }
    }
}