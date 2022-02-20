using EcommerceService.Core.DTOs.Campaign;

namespace EcommerceService.Core.Services;

public interface ICampaignService
{
    void Create(CreateCampaignInput createCampaignInput);

    CampaignDto? Detail(string name);

    Task CampaingJob();
}