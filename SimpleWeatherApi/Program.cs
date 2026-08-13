using SimpleWeatherApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddSingleton<IWeatherDescriptionService, WeatherDescriptionService>();

var app = builder.Build();

const string MissingCityQueryParameterError = "The city query parameter is required.";

app.MapGet("/weather", (string? city, IWeatherForecastService weatherForecastService) =>
{
    if (!TryGetNormalizedCity(city, out var normalizedCity, out var validationError))
    {
        return validationError;
    }

    var forecast = weatherForecastService.GetForecast(normalizedCity);
    return Results.Ok(WeatherResponse.FromForecast(forecast));
});

app.MapGet("/weather/description", (string? city, IWeatherForecastService weatherForecastService, IWeatherDescriptionService descriptionService) =>
{
    if (!TryGetNormalizedCity(city, out var normalizedCity, out var validationError))
    {
        return validationError;
    }

    var forecast = weatherForecastService.GetForecast(normalizedCity);
    var description = descriptionService.GetHumanDescription(forecast);
    return Results.Ok(new { city = forecast.City, description });
});

app.Run();

bool TryGetNormalizedCity(string? city, out string normalizedCity, out IResult? validationError)
{
    if (string.IsNullOrWhiteSpace(city))
    {
        normalizedCity = string.Empty;
        validationError = Results.BadRequest(new { error = MissingCityQueryParameterError });
        return false;
    }

    normalizedCity = city.Trim();
    validationError = null;
    return true;
}

public partial class Program;
