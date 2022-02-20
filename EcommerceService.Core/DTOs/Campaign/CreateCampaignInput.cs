namespace EcommerceService.Core.DTOs.Campaign;

public class CreateCampaignInput
{
    public string Name { get; set; }
    public string ProductCode { get; set; }
    public int Duration { get; set; }
    public double PriceManipulationLimit { get; set; }
    public int TargetSalesCount { get; set; }
}