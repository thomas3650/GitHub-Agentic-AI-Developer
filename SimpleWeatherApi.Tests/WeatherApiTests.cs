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

        response.EnsureSuccessStatusCode();

        var forecast = await response.Content.ReadFromJsonAsync<WeatherResponse>();

        Assert.NotNull(forecast);
        Assert.Equal("London", forecast.City);
        Assert.Equal(-2, forecast.TemperatureC);
        Assert.Equal(28, forecast.TemperatureF);
        Assert.Equal("Windy", forecast.Condition);
    }

    [Fact]
    public async Task GetWeatherRequiresCity()
    {
        var response = await _client.GetAsync("/weather");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
