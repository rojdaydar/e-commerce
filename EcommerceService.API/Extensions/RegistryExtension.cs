using EcommerceService.Core.Repositories;
using EcommerceService.Core.Services;
using EcommerceService.Data.Repositories;
using EcommerceService.Service;

namespace EcommerceService.API.Extensions;

public static class RegistryExtension
{
    public static void AddRegistry(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>),typeof(Repository<>));

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ICampaignService, CampaignService>();
    }
}