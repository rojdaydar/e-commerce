namespace EcommerceService.Core.Domain;

public class Campaign : Base
{
    public string Name { get; set; }
    public string ProductCode { get; set; }

    public decimal CurrentProductPrice { get; set; }
    public int ProductId { get; set; }

    public Product Product { get; set; }
    public int Duration { get; set; }

    public int TargetSalesCount { get; set; }
    public decimal PriceManipulationLimit { get; set; }
    public ICollection<Order> Orders { get; set; }
}