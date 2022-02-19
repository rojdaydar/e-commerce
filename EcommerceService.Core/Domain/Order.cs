namespace EcommerceService.Core.Domain;

public class Order : Base
{
    public int ProductId { get; set; }
    
    public int CampaignId { get; set; }
    
    public decimal CurrentPrice { get; set; }
    
    public int Quantity { get; set; }
    
    public Product Product { get; set; }
    
    public Campaign Campaign { get; set; }
    
    
   
}