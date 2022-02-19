using EcommerceService.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddRegistry();
builder.Services.AddSwagger();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "E-Commerce Service");
    c.RoutePrefix = string.Empty;
});
app.UseRequestResponseLogging();
app.UseRouting();
app.UseEndpoints(endpoint => { endpoint.MapControllers(); });
app.Run();