using SimpleWeatherApi;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/weather", (string? city) =>
{
    if (string.IsNullOrWhiteSpace(city))
    {
        return Results.BadRequest(new { error = "The city query parameter is required." });
    }

    var normalizedCity = city.Trim();
    var seed = normalizedCity.ToUpperInvariant().Sum(character => character);
    var temperatureC = (seed % 35) - 5;
    var conditions = new[] { "Sunny", "Cloudy", "Rainy", "Windy", "Snowy" };
    var condition = conditions[seed % conditions.Length];
    var temperatureF = (int)Math.Round((temperatureC * 9d / 5d) + 32);

    return Results.Ok(new WeatherResponse(normalizedCity, temperatureC, temperatureF, condition));
});

app.Run();

public partial class Program;
