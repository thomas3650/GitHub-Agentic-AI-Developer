namespace SimpleWeatherApi;

public interface IWeatherForecastService
{
    WeatherForecast GetForecast(string city);
}
