using EcommerceService.Core.DTOs.Campaign;
using EcommerceService.Core.Extensions;
using EcommerceService.Core.Services;
using Microsoft.Extensions.Caching.Memory;

namespace EcommerceService.Service.Services;

public class TimeService : ITimeService
{
    private readonly IMemoryCache _memoryCache;
    public DateTime _localDateTime => GetDateTime();
    private const string key = "current-time";

    public TimeService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public void SetDateTime(DateTime time) => _memoryCache.Set(key, time);

    public DateTime GetDateTime() => _memoryCache.Get<DateTime>(key);


    public IncreaseDto Increase(int hour)
    {
        DateTime currentDatetime;
        if (_memoryCache.TryGetValue(key, out DateTime currentLocalDatetime))
        {
            currentDatetime = SystemDatetimeProviver.IncreaseHour(hour, currentLocalDatetime);
        }
        else
        {
            var defaultDatetime = DateTime.Now.Date.Add(new TimeSpan(0, 00, 0));
            currentDatetime = SystemDatetimeProviver.IncreaseHour(hour, defaultDatetime);
        }

        SetDateTime(currentDatetime);
        
        IncreaseDto increaseDto = new()
        {
            Hour = currentDatetime.ToString("hh:mm")
        };
        return increaseDto;
    }
}