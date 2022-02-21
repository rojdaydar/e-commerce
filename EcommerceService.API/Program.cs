using EcommerceService.API.Extensions;
using EcommerceService.API.Filters;
using EcommerceService.Core.Services;
using EcommerceService.Data;
using Hangfire;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EcommerceDbContext>(x =>
    x.UseSqlServer(connectionString));

builder.Services.AddHangfire(configuration => { configuration.UseSqlServerStorage(connectionString); });
builder.Services.AddHangfireServer();
builder.Services.AddRegistry();
builder.Services.AddSwagger();
builder.WebHost.AddGraylog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.ApplyDbChangesAndSeed();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "E-Commerce Service");
    c.RoutePrefix = string.Empty;
});

app.UseHangfireServer();
app.UseHangfireDashboard(
    "/hangfire",
    new DashboardOptions
    {
        Authorization = new[] {new HangfireAuthorizationFilter()},
    });
var recurringJobManager = app.Services.GetRequiredService<IRecurringJobManager>();
recurringJobManager.AddOrUpdate<ICampaignService>("CheckCampaingJob", o => o.CheckCampaingJob(), Cron.Minutely);

app.UseRequestResponseLogging();
app.UseRouting();
app.UseEndpoints(endpoint => { endpoint.MapControllers(); });
app.Run();