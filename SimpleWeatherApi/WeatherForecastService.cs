namespace SimpleWeatherApi;

public sealed class WeatherForecastService : IWeatherForecastService
{
    private static readonly string[] Conditions = ["Sunny", "Cloudy", "Rainy", "Windy", "Snowy"];

    public WeatherForecast GetForecast(string city)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(city);

        var normalizedCity = city.Trim();
        var seed = normalizedCity.ToUpperInvariant().Sum(character => character);
        var temperatureC = (seed % 35) - 5;
        var condition = Conditions[seed % Conditions.Length];

        return new WeatherForecast(normalizedCity, temperatureC, condition);
    }
}
