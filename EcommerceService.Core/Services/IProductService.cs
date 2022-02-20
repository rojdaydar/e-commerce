using EcommerceService.Core.DTOs.Product;

namespace EcommerceService.Core.Services;

public interface IProductService
{
    void Create(CreateProductInput createProductInput);

    ProductDto Detail(string productCode);
}