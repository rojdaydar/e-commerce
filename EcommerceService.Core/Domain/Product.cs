namespace EcommerceService.Core.Domain;

public class Product : Base
{
    public string Code { get; set; }
    
    public decimal Price { get; set; }
    
    public long Stock { get; set; }
    
    public ICollection<Order> Orders { get; set; }
}