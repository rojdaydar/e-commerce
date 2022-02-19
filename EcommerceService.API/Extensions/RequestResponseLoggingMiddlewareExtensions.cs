using EcommerceService.API.Middlewares;

namespace EcommerceService.API.Extensions;

public static class RequestResponseLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder) =>
        builder.UseMiddleware<RequestResponseLoggingMiddleware>();
}