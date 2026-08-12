namespace SimpleWeatherApi;

public sealed record WeatherResponse(string City, int TemperatureC, string Condition)
{
    public int TemperatureF => (int)Math.Round((TemperatureC * 9d / 5d) + 32);

    public static WeatherResponse FromForecast(WeatherForecast forecast) =>
        new(forecast.City, forecast.TemperatureC, forecast.Condition);
}
