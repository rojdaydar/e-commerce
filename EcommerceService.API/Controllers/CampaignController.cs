using System.Net;
using EcommerceService.Core.DTOs.Base;
using EcommerceService.Core.DTOs.Campaign;
using EcommerceService.Core.DTOs.Order;
using EcommerceService.Core.DTOs.Product;
using EcommerceService.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceService.API.Controllers;

[Route("api/v1/campaigns")]
[ApiController]
public class CampaignController : ControllerBase
{
    private readonly ICampaignService _campaignService;
    public CampaignController(ICampaignService campaignService)
    {
        _campaignService = campaignService;
    }

    [HttpPost]
    public ActionResult Create(CreateCampaignInput createCampaignInput)
    {
        _campaignService.Create(createCampaignInput);
        return new ObjectResult(null)
        {
            StatusCode = (int) HttpStatusCode.Created
        };
    }
    
    [HttpPost("{name}")]
    public ActionResult<CustomResponseDto> Detail(string name)
    {
        _campaignService.Detail(name);
        
        return new OkObjectResult(
            new CustomResponseDto().Success((int) HttpStatusCode.OK));
    }
    
}