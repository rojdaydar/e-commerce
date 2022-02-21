using EcommerceService.Core.DTOs.Campaign;

namespace EcommerceService.Core.Services;

public interface ITimeService
{
    public DateTime _localDateTime { get; }

    IncreaseDto Increase(int hour);
}