using Newtonsoft.Json.Serialization;
using System.Net;
using Volvo.Fleet.Database;
using Volvo.Fleet.Domain.Services;
using Volvo.Fleet.Entities;
using Volvo.Fleet.VehicleService;


var services = builder.Services;
var configuration = builder.Configuration;

services.AddCors(options =>
{
    options.AddPolicy("Default", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

services.AddDbContext<VolvoDbContext>();

services.AddScoped<IUnitOfWork, UnitOfWork>();

services.AddScoped<IVehicleService, VehicleService>();

services
    .AddHttpContextAccessor()
    .AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
    });

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors("Default");

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            if (ex.Message.ToLower().Contains("not found"))
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            else
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var errorResponse = new
            {
                status = context.Response.StatusCode,
                message = ex.Message,
            };

            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(errorResponse));
        }
    }
}
