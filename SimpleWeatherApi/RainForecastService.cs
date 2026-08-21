namespace SimpleWeatherApi;

public sealed class RainForecastService : IRainForecastService
{
    private const int MaxRainMmTenths = 500;

    public RainForecast GetRainForecast(string city)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(city);

        var normalizedCity = city.Trim();
        var seed = normalizedCity.ToUpperInvariant().Sum(character => (long)character);

        // Produce a deterministic 0.0–50.0 mm rainfall value in 0.1 mm increments.
        var rainMm = (double)(seed % (MaxRainMmTenths + 1)) / 10d;

        return new RainForecast(normalizedCity, rainMm);
    }
}
