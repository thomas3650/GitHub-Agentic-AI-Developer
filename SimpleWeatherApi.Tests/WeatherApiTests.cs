using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using SimpleWeatherApi;

namespace SimpleWeatherApi.Tests;

public sealed class WeatherApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public WeatherApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetWeatherReturnsForecastForCity()
    {
        var response = await _client.GetAsync("/weather?city=London");
        var validConditions = new[] { "Sunny", "Cloudy", "Rainy", "Windy", "Snowy" };

        response.EnsureSuccessStatusCode();

        var forecast = await response.Content.ReadFromJsonAsync<WeatherResponse>();
        Assert.NotNull(forecast);
        Assert.Equal("London", forecast.City);
        Assert.Contains(forecast.Condition, validConditions);
        Assert.Equal((int)Math.Round((forecast.TemperatureC * 9d / 5d) + 32), forecast.TemperatureF);
    }

    [Fact]
    public async Task GetWeatherTrimsCityName()
    {
        var response = await _client.GetAsync("/weather?city=%20%20London%20%20");

        response.EnsureSuccessStatusCode();

        var forecast = await response.Content.ReadFromJsonAsync<WeatherResponse>();

        Assert.NotNull(forecast);
        Assert.Equal("London", forecast.City);
    }

    [Fact]
    public async Task GetWeatherRequiresCity()
    {
        var response = await _client.GetAsync("/weather");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetWeatherRejectsBlankCity()
    {
        var response = await _client.GetAsync("/weather?city=%20%20%20");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
