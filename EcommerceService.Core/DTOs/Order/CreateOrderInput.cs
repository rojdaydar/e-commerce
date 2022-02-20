namespace EcommerceService.Core.DTOs.Order;

public class CreateOrderInput
{
    public string ProductCode { get; set; }
    
    public int Quantity { get; set; }
}