using SimpleWeatherApi;
using SimpleWeatherApi.Endpoints;
using SimpleWeatherApi.Validation;
using NSwag;
using NSwag.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddSingleton<IWeatherDescriptionService, WeatherDescriptionService>();
builder.Services.AddSingleton<IRainForecastService, RainForecastService>();
builder.Services.AddSingleton<ICityQueryValidator, CityQueryValidator>();
builder.Services.AddEndpoints(typeof(Program).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(options =>
{
    options.DocumentName = "v1";
    options.Title = "Simple Weather API";
    options.Version = "v1";
});

var app = builder.Build();

app.MapEndpoints();
app.UseOpenApi();
app.UseSwaggerUi();

app.Run();

public partial class Program;
