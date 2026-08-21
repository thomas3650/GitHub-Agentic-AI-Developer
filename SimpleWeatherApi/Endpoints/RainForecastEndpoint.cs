using SimpleWeatherApi.Validation;

namespace SimpleWeatherApi.Endpoints;

public sealed class RainForecastEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/weather/rain", (
            string? city,
            ICityQueryValidator cityQueryValidator,
            IRainForecastService rainForecastService) =>
        {
            if (!cityQueryValidator.TryNormalize(city, out var normalizedCity, out var validationError))
            {
                return validationError;
            }

            var rainForecast = rainForecastService.GetRainForecast(normalizedCity);
            return Results.Ok(rainForecast);
        });
    }
}
