namespace EcommerceService.Core.Domain;

public class Campaign : Base
{
    public string Name { get; set; }

    public int ProductId { get; set; }

    public int Duration { get; set; }

    public int TargetSalesCount { get; set; }

    public double PriceManipulationLimit { get; set; }
    
    public Product Product { get; set; }
    
    public ICollection<Order> Orders { get; set; }
}