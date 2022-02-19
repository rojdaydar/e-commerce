using EcommerceService.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceService.API.Extensions;

public static class DatabaseExtension
{
    public static void ApplyDbChangesAndSeed(this IApplicationBuilder builder)
    {
        using var serviceScope =
            builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

        using var context = serviceScope.ServiceProvider.GetService<EcommerceDbContext>();

        if (context == null) return;
        initialize(context);
    }
        
    private static void initialize(EcommerceDbContext context)
    {
        context.Database.Migrate();
    }
}