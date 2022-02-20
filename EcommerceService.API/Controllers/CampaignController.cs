using System.Net;
using EcommerceService.Core.DTOs.Base;
using EcommerceService.Core.DTOs.Campaign;
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
    
    [HttpGet("{name}")]
    public ActionResult<CustomResponseDto<CampaignDto>> Detail(string name)
    {
        var campaignDto = _campaignService.Detail(name);
        
        if (campaignDto is null)
            return new NoContentResult();
        
        return new OkObjectResult(
            new CustomResponseDto<CampaignDto>().Success((int) HttpStatusCode.OK, campaignDto));
    }

    [HttpPost("increase")]
    public ActionResult<CustomResponseDto<IncreaseDto>> Increase(int hour)
    {
        return Ok();
    }
    
}