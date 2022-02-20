using AutoMapper;
using EcommerceService.Core.Domain;
using EcommerceService.Core.DTOs.Campaign;
using EcommerceService.Core.DTOs.Order;
using EcommerceService.Core.DTOs.Product;

namespace EcommerceService.Service.Mapping;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<CreateProductInput, Product>().ReverseMap();

        CreateMap<Product, ProductDto>().ReverseMap();

        CreateMap<CreateCampaignInput, Campaign>().ReverseMap();

        CreateMap<Order, CreateOrderInput>().ReverseMap();
    }
}