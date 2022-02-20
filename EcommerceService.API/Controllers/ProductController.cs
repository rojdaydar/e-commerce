using System.Net;
using EcommerceService.Core.DTOs.Base;
using EcommerceService.Core.DTOs.Product;
using EcommerceService.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceService.API.Controllers;

[Route("api/v1/products")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public ActionResult Create(CreateProductInput createProductInput)
    {
        _productService.Create(createProductInput);

        return new ObjectResult(null)
        {
            StatusCode = (int) HttpStatusCode.Created
        };
    }

    [HttpGet("{productCode}")]
    public ActionResult<CustomResponseDto<ProductDto>> DetailByCode(string productCode)
    {
        var productDto = _productService.Detail(productCode);

        if (productDto is null)
            return new NoContentResult();
        
        return new OkObjectResult(
            new CustomResponseDto<ProductDto>().Success((int) HttpStatusCode.OK, productDto));
    }
}