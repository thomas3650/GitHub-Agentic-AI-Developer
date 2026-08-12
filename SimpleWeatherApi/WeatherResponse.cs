namespace SimpleWeatherApi;

public sealed record WeatherResponse(string City, int TemperatureC, int TemperatureF, string Condition);
