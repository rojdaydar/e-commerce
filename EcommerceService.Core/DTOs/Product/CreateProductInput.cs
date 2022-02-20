namespace EcommerceService.Core.DTOs.Product;

public class CreateProductInput
{
    public string ProductCode { get; set; }
    
    public decimal Price { get; set; }
    
    public long Stock { get; set; }
}