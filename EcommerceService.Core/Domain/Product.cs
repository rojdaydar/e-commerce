namespace EcommerceService.Core.Domain;

public class Product : Base
{
    public Product()
    {
        Orders = new List<Order>();
    }
    public string ProductCode { get; set; }
    
    public decimal Price { get; set; }
    
    public long Stock { get; set; }
    
    public List<Order> Orders { get; set; }

}