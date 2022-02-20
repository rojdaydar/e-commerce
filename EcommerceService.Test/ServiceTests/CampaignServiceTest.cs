using EcommerceService.Core.Domain;
using EcommerceService.Core.DTOs.Campaign;
using EcommerceService.Core.DTOs.Product;
using EcommerceService.Core.Exceptions;
using EcommerceService.Core.Services;
using EcommerceService.Service.Services;
using EcommerceService.Test.ServiceTests.Base;
using Xunit;

namespace EcommerceService.Test.ServiceTests;

public class CampaignServiceTest : ServiceTestBase
{
    private readonly ICampaignService _campaignService;

    public CampaignServiceTest()
    {
        _campaignService = new CampaignService(_campaignRepository, _mapper, _productRepository, _orderRepository);
    }

    [Theory]
    [MemberData(nameof(ValidCreateCampaignInput))]
    public void Create_ShouldThrowCustomException_WhenThereIsNotProductByProductCode(
        CreateCampaignInput createCampaignInput)
    {
        // arrange 
        var product = new Product()
        {
            ProductCode = "P2",
            Price = 5.5M,
            Stock = 100,
            IsDeleted = false
        };
        AddProductToInMemory(product);

        // act
        var exception = Assert.Throws<CustomException>(() =>
            _campaignService.Create(createCampaignInput));

        // assert
        Assert.NotNull(exception);
        Assert.Equal("Kampanyalı ürün bulunamadı.", exception.ErrorMessage.ErrorDescription);
        Assert.Equal("3000", exception.ErrorMessage.ErrorCode);
    }
    
    [Theory]
    [MemberData(nameof(ValidCreateCampaignInput))]
    public void Create_ShouldThrowCustomException_WhenThereIsProductByProductCodeButDeleted(
        CreateCampaignInput createCampaignInput)
    {
        // arrange 
        var product = new Product()
        {
            ProductCode = "P2",
            Price = 5.5M,
            Stock = 100,
            IsDeleted = true
        };
        AddProductToInMemory(product);

        // act
        var exception = Assert.Throws<CustomException>(() =>
            _campaignService.Create(createCampaignInput));

        // assert
        Assert.NotNull(exception);
        Assert.Equal("Kampanyalı ürün bulunamadı.", exception.ErrorMessage.ErrorDescription);
        Assert.Equal("3000", exception.ErrorMessage.ErrorCode);
    }
    
    [Theory]
    [MemberData(nameof(ValidCreateCampaignInput))]
    public void Create_ShouldShouldAddSuccessfully_WhenThereIsProductByProductCode(
        CreateCampaignInput createCampaignInput)
    {
        // arrange 
        var product = new Product()
        {
            ProductCode = "P1",
            Price = 5.5M,
            Stock = 100,
            IsDeleted = false
        };
        AddProductToInMemory(product);

        // act
         _campaignService.Create(createCampaignInput);

         var addedCampaign=_campaignRepository.Find(c => c.Name.Equals(createCampaignInput.Name));

        // assert
        Assert.NotNull(addedCampaign);
        Assert.Equal(createCampaignInput.Duration, addedCampaign.Duration);
        Assert.Equal(createCampaignInput.Name, addedCampaign.Name);
        Assert.Equal(createCampaignInput.ProductCode, addedCampaign.ProductCode);
        Assert.Equal(createCampaignInput.PriceManipulationLimit,addedCampaign.PriceManipulationLimit);
        Assert.False(addedCampaign.IsDeleted);
    }
}