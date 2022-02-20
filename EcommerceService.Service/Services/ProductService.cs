using AutoMapper;
using EcommerceService.Core.Domain;
using EcommerceService.Core.DTOs.Product;
using EcommerceService.Core.Exceptions;
using EcommerceService.Core.Repositories;
using EcommerceService.Core.Services;

namespace EcommerceService.Service.Services;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IMapper mapper, IRepository<Product> productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public void Create(CreateProductInput createProductInput)
    {
        var product = _mapper.Map<Product>(createProductInput);

        var isExistProductByProductCode =
            _productRepository.Find(p => p.ProductCode.Equals(product.ProductCode) && !p.IsDeleted);
        if (isExistProductByProductCode is not null)
            throw new CustomException("Ürün kaydı zaten yapılmıştır.", "2000");

        var addedProduct = _productRepository.Add(product);
        _productRepository.SaveChanges();

        if (addedProduct is {Id: 0})
            throw new Exception();
    }

    public ProductDto Detail(string productCode)
    {
        var product = _productRepository.Find(p => p.ProductCode == productCode && !p.IsDeleted);

        var productDto = _mapper.Map<ProductDto>(product);

        return productDto;
    }
}