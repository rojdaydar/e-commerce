using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace EcommerceService.API.Filters;

public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize([NotNull] DashboardContext context)
    {
        return true;
    }
}