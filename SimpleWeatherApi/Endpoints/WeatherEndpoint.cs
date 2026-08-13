using SimpleWeatherApi.Validation;

namespace SimpleWeatherApi.Endpoints;

public sealed class WeatherEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/weather", (
            string? city,
            ICityQueryValidator cityQueryValidator,
            IWeatherForecastService weatherForecastService) =>
        {
            if (!cityQueryValidator.TryNormalize(city, out var normalizedCity, out var validationError))
            {
                return validationError!;
            }

            var forecast = weatherForecastService.GetForecast(normalizedCity);
            return Results.Ok(WeatherResponse.FromForecast(forecast));
        });
    }
}
