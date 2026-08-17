namespace SimpleWeatherApi;

public interface IRainForecastService
{
    RainForecast GetRainForecast(string city);
}

public sealed class RainForecastService : IRainForecastService
{
    private const double MaxRainMm = 50d;

    public RainForecast GetRainForecast(string city)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(city);

        var normalizedCity = city.Trim();
        var seed = normalizedCity.ToUpperInvariant().Sum(character => (long)character);
        var rainMm = Math.Round((seed % (long)(MaxRainMm * 10)) / 10d, 1);

        return new RainForecast(normalizedCity, rainMm);
    }
}
