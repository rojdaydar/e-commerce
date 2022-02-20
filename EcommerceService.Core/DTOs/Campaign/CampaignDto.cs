namespace EcommerceService.Core.DTOs.Campaign;

public class CampaignDto
{
    public bool Status { get; set; }
    
    public  int TargetSales { get; set; }
    
    public int TotalSales { get; set; }
    
    public int Turnover { get; set; }
    
    public double AvarageItemPrice { get; set; }
}