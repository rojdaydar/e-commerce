using EcommerceService.Core.Domain;
using EcommerceService.Core.DTOs.Product;
using EcommerceService.Core.Exceptions;
using EcommerceService.Core.Services;
using EcommerceService.Service.Services;
using EcommerceService.Test.ServiceTests.Base;
using Xunit;

namespace EcommerceService.Test.ServiceTests;

public class ProductServiceTest : ServiceTestBase
{
    private readonly IProductService _productService;

    public ProductServiceTest()
    {
        _productService = new ProductService(_mapper, _productRepository);
    }

    [Theory]
    [MemberData(nameof(ValidCreateProductInput))]
    public void Create_ShouldThrowCustomException_WhenThereWasProductWithProductCode(
        CreateProductInput createProductInput)
    {
        // arrange 
        var product = new Product()
        {
            ProductCode = "P1",
            Price = 5.5M,
            Stock = 100
        };
        AddProductToInMemory(product);

        // act
        var exception = Assert.Throws<CustomException>(() =>
            _productService.Create(createProductInput));

        // assert
        Assert.NotNull(exception);
        Assert.Equal("Ürün kaydı zaten yapılmıştır.", exception.ErrorMessage.ErrorDescription);
        Assert.Equal("2000", exception.ErrorMessage.ErrorCode);
    }

    [Theory]
    [MemberData(nameof(ValidCreateProductInput))]
    public void Create_ShouldAddSuccessfully_WhenThereIsNotProductByProductCode(CreateProductInput createProductInput)
    {
        // arrange 
        var product = new Product()
        {
            ProductCode = "P2",
            Price = 89.5M,
            Stock = 450
        };
        AddProductToInMemory(product);

        // act
        _productService.Create(createProductInput);
        var addedProduct = _productRepository.Find(p => p.ProductCode.Equals(product.ProductCode));

        // assert
        Assert.NotNull(addedProduct);
        Assert.Equal(product.Price, addedProduct.Price);
        Assert.Equal(product.Stock, addedProduct.Stock);
        Assert.Equal(product.ProductCode, addedProduct.ProductCode);
        Assert.False(addedProduct.IsDeleted);
    }

    [Theory]
    [InlineData("P1")]
    public void Detail_ShouldReturnNull_WhenThereIsNotProduct(string productCode)
    {
        // arrange
        var product = new Product()
        {
            ProductCode = "P2",
            Price = 89.5M,
            Stock = 450
        };
        AddProductToInMemory(product);
        
        // act
        var response= _productService.Detail(productCode);

        // assert
        Assert.Null(response);
    }
    
    [Theory]
    [InlineData("P1")]
    public void Detail_ShouldReturnNull_WhenThereIsProductByProductCodeButItDeleted(string productCode)
    {
        // arrange
        var product = new Product()
        {
            ProductCode = "P2",
            Price = 89.5M,
            Stock = 450,
            IsDeleted = true
        };
        AddProductToInMemory(product);
        
        // act
        var response= _productService.Detail(productCode);

        // assert
        Assert.Null(response);
    }
    
    [Theory]
    [InlineData("P1")]
    public void Detail_ShouldReturnNull_WhenThereIsProduct(string productCode)
    {
        // arrange
        var product = new Product()
        {
            ProductCode = "P1",
            Price = 89.5M,
            Stock = 450
        };
        AddProductToInMemory(product);
        
        // act
        var response= _productService.Detail(productCode);

        // assert
        Assert.NotNull(response);
        Assert.IsType<ProductDto>(response);
        Assert.Equal(product.Price,response.Price);
        Assert.Equal(product.Stock,response.Stock);
        Assert.Equal(product.ProductCode,response.ProductCode);
    }
}