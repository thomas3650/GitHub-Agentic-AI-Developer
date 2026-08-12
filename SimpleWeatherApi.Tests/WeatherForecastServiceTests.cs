using SimpleWeatherApi;

namespace SimpleWeatherApi.Tests;

public sealed class WeatherForecastServiceTests
{
    private readonly WeatherForecastService _service = new();

    [Fact]
    public void GetForecastTrimsTheCityName()
    {
        var forecast = _service.GetForecast("  London  ");

        Assert.Equal("London", forecast.City);
    }

    [Fact]
    public void GetForecastReturnsDeterministicForecastForSameCity()
    {
        var firstForecast = _service.GetForecast("London");
        var secondForecast = _service.GetForecast("London");

        Assert.Equal(firstForecast, secondForecast);
    }

    [Fact]
    public void GetForecastIgnoresCityNameCasingForSeedCalculation()
    {
        var lowerCaseForecast = _service.GetForecast("london");
        var upperCaseForecast = _service.GetForecast("LONDON");

        Assert.Equal(lowerCaseForecast.TemperatureC, upperCaseForecast.TemperatureC);
        Assert.Equal(lowerCaseForecast.Condition, upperCaseForecast.Condition);
    }

    [Fact]
    public void GetForecastRejectsBlankCity()
    {
        Assert.Throws<ArgumentException>(() => _service.GetForecast("   "));
    }
}
