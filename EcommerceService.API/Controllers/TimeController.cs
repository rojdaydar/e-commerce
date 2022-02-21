using System.Net;
using EcommerceService.Core.DTOs.Base;
using EcommerceService.Core.DTOs.Campaign;
using EcommerceService.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceService.API.Controllers;

[Route("api/v1/increases")]
[ApiController]
public class TimeController : ControllerBase
{

    private readonly ITimeService _timeService;
    // GET
    public TimeController(ITimeService timeService)
    {
        _timeService = timeService;
    }

    [HttpPost]
    public ActionResult<CustomResponseDto<IncreaseDto>> Increase(int hour)
    {
        var increaseDto = _timeService.Increase(hour);
        return new OkObjectResult(
            new CustomResponseDto<IncreaseDto>().Success((int) HttpStatusCode.OK, increaseDto));
    }
}