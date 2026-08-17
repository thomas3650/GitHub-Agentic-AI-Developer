using SimpleWeatherApi;

namespace SimpleWeatherApi.Tests;

public sealed class RainForecastServiceTests
{
    private readonly RainForecastService _service = new();

    [Fact]
    public void GetRainForecastTrimsTheCityName()
    {
        var forecast = _service.GetRainForecast("  London  ");

        Assert.Equal("London", forecast.City);
    }

    [Fact]
    public void GetRainForecastReturnsDeterministicRainAmountForSameCity()
    {
        var firstForecast = _service.GetRainForecast("London");
        var secondForecast = _service.GetRainForecast("London");

        Assert.Equal(firstForecast, secondForecast);
    }

    [Fact]
    public void GetRainForecastIgnoresCityNameCasingForSeedCalculation()
    {
        var lowerCaseForecast = _service.GetRainForecast("london");
        var upperCaseForecast = _service.GetRainForecast("LONDON");

        Assert.Equal("london", lowerCaseForecast.City);
        Assert.Equal("LONDON", upperCaseForecast.City);
        Assert.Equal(lowerCaseForecast.RainMm, upperCaseForecast.RainMm);
    }

    [Fact]
    public void GetRainForecastReturnsNonNegativeRainAmountWithinExpectedRange()
    {
        var forecast = _service.GetRainForecast("London");

        Assert.InRange(forecast.RainMm, 0d, 50d);
    }

    [Fact]
    public void GetRainForecastRejectsBlankCity()
    {
        Assert.Throws<ArgumentException>(() => _service.GetRainForecast("   "));
    }
}
