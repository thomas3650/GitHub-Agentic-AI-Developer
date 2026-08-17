using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
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
        var error = await response.Content.ReadFromJsonAsync<JsonObject>();

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("The city query parameter is required.", error?["error"]?.GetValue<string>());
    }

    [Fact]
    public async Task GetWeatherRejectsBlankCity()
    {
        var response = await _client.GetAsync("/weather?city=%20%20%20");
        var error = await response.Content.ReadFromJsonAsync<JsonObject>();

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("The city query parameter is required.", error?["error"]?.GetValue<string>());
    }

    [Fact]
    public async Task GetWeatherDescriptionReturnsDescriptionForCity()
    {
        var response = await _client.GetAsync("/weather/description?city=London");

        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadFromJsonAsync<JsonObject>();

        Assert.NotNull(body);
        Assert.Equal("London", body["city"]?.GetValue<string>());

        var description = body["description"]?.GetValue<string>();
        Assert.False(string.IsNullOrWhiteSpace(description));
        Assert.StartsWith("In London, it's ", description);
        Assert.EndsWith(".", description);
    }

    [Fact]
    public async Task GetWeatherDescriptionTrimsCityName()
    {
        var response = await _client.GetAsync("/weather/description?city=%20%20London%20%20");

        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadFromJsonAsync<JsonObject>();

        Assert.NotNull(body);
        Assert.Equal("London", body["city"]?.GetValue<string>());
        Assert.StartsWith("In London, it's ", body["description"]?.GetValue<string>());
    }

    [Fact]
    public async Task GetWeatherDescriptionRequiresCity()
    {
        var response = await _client.GetAsync("/weather/description");
        var error = await response.Content.ReadFromJsonAsync<JsonObject>();

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("The city query parameter is required.", error?["error"]?.GetValue<string>());
    }

    [Fact]
    public async Task GetWeatherDescriptionRejectsBlankCity()
    {
        var response = await _client.GetAsync("/weather/description?city=%20%20%20");
        var error = await response.Content.ReadFromJsonAsync<JsonObject>();

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("The city query parameter is required.", error?["error"]?.GetValue<string>());
    }

    [Fact]
    public async Task GetWeatherRainReturnsRainAmountForCity()
    {
        var response = await _client.GetAsync("/weather/rain?city=London");

        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadFromJsonAsync<JsonObject>();

        Assert.NotNull(body);
        Assert.Equal("London", body["city"]?.GetValue<string>());

        var rainMm = body["rainMm"]?.GetValue<double>();
        Assert.NotNull(rainMm);
        Assert.InRange(rainMm.Value, 0d, 50d);
    }

    [Fact]
    public async Task GetWeatherRainTrimsCityName()
    {
        var response = await _client.GetAsync("/weather/rain?city=%20%20London%20%20");

        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadFromJsonAsync<JsonObject>();

        Assert.NotNull(body);
        Assert.Equal("London", body["city"]?.GetValue<string>());
    }

    [Fact]
    public async Task GetWeatherRainRequiresCity()
    {
        var response = await _client.GetAsync("/weather/rain");
        var error = await response.Content.ReadFromJsonAsync<JsonObject>();

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("The city query parameter is required.", error?["error"]?.GetValue<string>());
    }

    [Fact]
    public async Task GetWeatherRainRejectsBlankCity()
    {
        var response = await _client.GetAsync("/weather/rain?city=%20%20%20");
        var error = await response.Content.ReadFromJsonAsync<JsonObject>();

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("The city query parameter is required.", error?["error"]?.GetValue<string>());
    }
}
