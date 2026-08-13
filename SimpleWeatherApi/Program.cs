using SimpleWeatherApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddSingleton<IWeatherDescriptionService, WeatherDescriptionService>();

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

app.MapGet("/weather/description", (string? city, IWeatherForecastService weatherForecastService, IWeatherDescriptionService descriptionService) =>
{
    if (string.IsNullOrWhiteSpace(city))
    {
        return Results.BadRequest(new { error = "The city query parameter is required." });
    }

    var forecast = weatherForecastService.GetForecast(city);
    var description = descriptionService.GetHumanDescription(forecast);
    return Results.Ok(new { city = forecast.City, description });
});

app.Run();

public partial class Program;
