using SimpleWeatherApi;

namespace SimpleWeatherApi.Tests;

public sealed class WeatherDescriptionServiceTests
{
    private readonly WeatherDescriptionService _service = new();

    [Theory]
    [InlineData(-11, "Sunny", "In Copenhagen, it's extremely cold with beautiful sunny skies.")]
    [InlineData(-11, " Sunny ", "In Copenhagen, it's extremely cold with beautiful sunny skies.")]
    [InlineData(-10, "Overcast", "In Copenhagen, it's very cold with gray cloudy skies.")]
    [InlineData(0, "Rain", "In Copenhagen, it's cold with rainfall expected.")]
    [InlineData(10, "Snow", "In Copenhagen, it's cool with snow on the ground.")]
    [InlineData(20, "Windy", "In Copenhagen, it's warm with strong winds.")]
    [InlineData(30, "Thunderstorm", "In Copenhagen, it's very hot with thunderstorms approaching.")]
    [InlineData(40, "Foggy", "In Copenhagen, it's extremely hot with foggy conditions.")]
    public void GetHumanDescriptionReturnsExpectedDescription(int temperatureC, string condition, string expectedDescription)
    {
        var forecast = new WeatherForecast("Copenhagen", temperatureC, condition);

        var description = _service.GetHumanDescription(forecast);

        Assert.Equal(expectedDescription, description);
    }

    [Fact]
    public void GetHumanDescriptionThrowsForNullForecast()
    {
        Assert.Throws<ArgumentNullException>(() => _service.GetHumanDescription(null!));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void GetHumanDescriptionThrowsForInvalidCondition(string? condition)
    {
        var forecast = new WeatherForecast("Copenhagen", 20, condition!);

        Assert.ThrowsAny<ArgumentException>(() => _service.GetHumanDescription(forecast));
    }
}
