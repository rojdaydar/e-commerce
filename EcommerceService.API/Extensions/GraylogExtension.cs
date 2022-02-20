using Serilog;
using Serilog.Events;
using Serilog.Sinks.Graylog;
using Serilog.Sinks.Graylog.Core.Transport;

namespace EcommerceService.API.Extensions;

public static class GraylogExtension
{
    public static IWebHostBuilder AddGraylog(this IWebHostBuilder webHostBuilder)
    {
        return webHostBuilder.ConfigureLogging((context, configureLogging) =>
        {
            configureLogging.ClearProviders();
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Graylog(new GraylogSinkOptions()
                {
                    Host = context.Configuration.GetValue<string>("Graylog:Host"),
                    Port = context.Configuration.GetValue<int>("Graylog:Post"),
                    TransportType =
                        (TransportType) Enum.Parse(typeof(TransportType),
                            context.Configuration.GetValue<string>("Graylog:Protocol"), true),
                })
                .CreateLogger();
            configureLogging.AddSerilog();
        });
    }
}