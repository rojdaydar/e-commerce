using System.Collections.Generic;
using AutoMapper;
using EcommerceService.Core.Domain;
using EcommerceService.Core.DTOs.Campaign;
using EcommerceService.Core.DTOs.Product;
using EcommerceService.Core.Repositories;
using EcommerceService.Data.Repositories;
using EcommerceService.Service.Mapping;

namespace EcommerceService.Test.ServiceTests.Base;

public class ServiceTestBase : InMemoryTestBase
{
    protected IRepository<Product> _productRepository;
    protected IRepository<Campaign> _campaignRepository;
    protected IRepository<Order> _orderRepository;
    protected IMapper _mapper;

    public ServiceTestBase()
    {
        initRepository();
        initMapper();
    }

    private void initMapper()
    {
        var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MapProfile()); });
        _mapper = mappingConfig.CreateMapper();
    }

    private void initRepository()
    {
        _productRepository = new Repository<Product>(_dbContext);
        _orderRepository = new Repository<Order>(_dbContext);
        _campaignRepository = new Repository<Campaign>(_dbContext);
    }

    protected void AddProductToInMemory(Product product)
    {
        _productRepository.Add(product);
        _productRepository.SaveChanges();
    }
    
    protected void AddCampaingToInMemory(Campaign campaign)
    {
        _campaignRepository.Add(campaign);
        _campaignRepository.SaveChanges();
    }

    #region Product Member Datas

    public static IEnumerable<object[]> ValidCreateProductInput()
    {
        yield return new object[]
        {
            new CreateProductInput()
            {
               ProductCode = "P1", 
               Price = 5.5M,
               Stock = 100
            }
        };
    }
    
    #endregion
    
    
    #region Campaign Member Datas

    public static IEnumerable<object[]> ValidCreateCampaignInput()
    {
        yield return new object[]
        {
            new CreateCampaignInput()
            {
                ProductCode = "P1", 
                PriceManipulationLimit = 5,
                Duration = 5,
                Name = "C1",
                TargetSalesCount = 200
            }
        };
    }
    
    #endregion
}