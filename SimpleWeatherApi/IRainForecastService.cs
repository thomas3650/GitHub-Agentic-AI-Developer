namespace SimpleWeatherApi;

public interface IRainForecastService
{
    RainForecast GetRainForecast(string city);
}
