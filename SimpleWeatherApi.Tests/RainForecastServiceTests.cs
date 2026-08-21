using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc.Testing;
using SimpleWeatherApi;

namespace SimpleWeatherApi.Tests;

public sealed class RainForecastServiceTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public RainForecastServiceTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetRainForecastEndpointReturnsRainForCity()
    {
        var response = await _client.GetAsync("/weather/rain?city=London");

        response.EnsureSuccessStatusCode();

        var forecast = await response.Content.ReadFromJsonAsync<RainForecast>();

        Assert.NotNull(forecast);
        Assert.Equal("London", forecast.City);
        Assert.InRange(forecast.RainMm, 0d, 50d);
    }

    [Fact]
    public async Task GetRainForecastEndpointTrimsCityName()
    {
        var response = await _client.GetAsync("/weather/rain?city=%20%20London%20%20");

        response.EnsureSuccessStatusCode();

        var forecast = await response.Content.ReadFromJsonAsync<RainForecast>();

        Assert.NotNull(forecast);
        Assert.Equal("London", forecast.City);
    }

    [Fact]
    public async Task GetRainForecastEndpointIsDeterministicForSameCity()
    {
        var firstResponse = await _client.GetAsync("/weather/rain?city=London");
        var secondResponse = await _client.GetAsync("/weather/rain?city=London");

        firstResponse.EnsureSuccessStatusCode();
        secondResponse.EnsureSuccessStatusCode();

        var firstForecast = await firstResponse.Content.ReadFromJsonAsync<RainForecast>();
        var secondForecast = await secondResponse.Content.ReadFromJsonAsync<RainForecast>();

        Assert.NotNull(firstForecast);
        Assert.NotNull(secondForecast);
        Assert.Equal(firstForecast.RainMm, secondForecast.RainMm);
    }

    [Fact]
    public async Task GetRainForecastEndpointIgnoresCityNameCasing()
    {
        var lowerCaseResponse = await _client.GetAsync("/weather/rain?city=london");
        var upperCaseResponse = await _client.GetAsync("/weather/rain?city=LONDON");

        lowerCaseResponse.EnsureSuccessStatusCode();
        upperCaseResponse.EnsureSuccessStatusCode();

        var lowerCaseForecast = await lowerCaseResponse.Content.ReadFromJsonAsync<RainForecast>();
        var upperCaseForecast = await upperCaseResponse.Content.ReadFromJsonAsync<RainForecast>();

        Assert.NotNull(lowerCaseForecast);
        Assert.NotNull(upperCaseForecast);
        Assert.Equal(lowerCaseForecast.RainMm, upperCaseForecast.RainMm);
    }

    [Fact]
    public async Task GetRainForecastEndpointRequiresCity()
    {
        var response = await _client.GetAsync("/weather/rain");
        var error = await response.Content.ReadFromJsonAsync<JsonObject>();

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("The city query parameter is required.", error?["error"]?.GetValue<string>());
    }

    [Fact]
    public async Task GetRainForecastEndpointRejectsBlankCity()
    {
        var response = await _client.GetAsync("/weather/rain?city=%20%20%20");
        var error = await response.Content.ReadFromJsonAsync<JsonObject>();

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("The city query parameter is required.", error?["error"]?.GetValue<string>());
    }
}
