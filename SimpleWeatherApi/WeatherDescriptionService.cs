namespace SimpleWeatherApi;

public interface IWeatherDescriptionService
{
    string GetHumanDescription(WeatherForecast forecast);
}

public sealed class WeatherDescriptionService : IWeatherDescriptionService
{
    public string GetHumanDescription(WeatherForecast forecast)
    {
        ArgumentNullException.ThrowIfNull(forecast);

        var temperatureFeeling = GetTemperatureFeeling(forecast.TemperatureC);
        var conditionDescription = GetConditionDescription(forecast.Condition);

        return $"In {forecast.City}, it's {temperatureFeeling} with {conditionDescription}.";
    }

    private static string GetTemperatureFeeling(int temperatureC)
    {
        return temperatureC switch
        {
            < -10 => "extremely cold",
            >= -10 and < 0 => "very cold",
            >= 0 and < 10 => "cold",
            >= 10 and < 20 => "cool",
            >= 20 and < 30 => "warm",
            >= 30 and < 40 => "very hot",
            >= 40 => "extremely hot",
        };
    }

    private static string GetConditionDescription(string condition)
    {
        return condition.ToLowerInvariant() switch
        {
            "sunny" or "clear" => "beautiful sunny skies",
            "cloudy" or "overcast" => "gray cloudy skies",
            "rainy" or "rain" => "rainfall expected",
            "snowy" or "snow" => "snow on the ground",
            "windy" => "strong winds",
            "stormy" or "thunderstorm" => "thunderstorms approaching",
            _ => $"{condition.ToLowerInvariant()} conditions",
        };
    }
}
