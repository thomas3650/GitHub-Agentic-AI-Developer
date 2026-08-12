using SimpleWeatherApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IWeatherForecastService, WeatherForecastService>();

var app = builder.Build();

app.MapGet("/weather", (string? city, IWeatherForecastService weatherForecastService) =>
{
    if (string.IsNullOrWhiteSpace(city))
    {
        return Results.BadRequest(new { error = "The city query parameter is required." });
    }

    var forecast = weatherForecastService.GetForecast(city);
    return Results.Ok(WeatherResponse.FromForecast(forecast));
});

app.Run();

public partial class Program;
