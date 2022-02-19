using Microsoft.OpenApi.Models;
namespace EcommerceService.API.Extensions;

public static class SwaggerExtension
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        return services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo() {Title = "E-Commerce Service API", Version = "v1"});
        });
    }
}