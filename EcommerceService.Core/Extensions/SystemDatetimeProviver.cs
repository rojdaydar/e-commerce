using DateTimeProviders;

namespace EcommerceService.Core.Extensions;

public class SystemDatetimeProviver
{
    public static DateTime IncreaseHour(int hour, DateTime dateTime)
    {
        DateTimeProvider.Provider = new StaticDateTimeProvider(dateTime);
        new OverrideDateTimeProvider(dateTime).MoveTimeForward(TimeSpan.FromHours(hour));
        return DateTimeProvider.Now.DateTime;
    }
}