using SimpleWeatherApi.Validation;

namespace SimpleWeatherApi.Endpoints;

public sealed class WeatherDescriptionEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/weather/description", (
            string? city,
            ICityQueryValidator cityQueryValidator,
            IWeatherForecastService weatherForecastService,
            IWeatherDescriptionService descriptionService) =>
        {
            if (!cityQueryValidator.TryNormalize(city, out var normalizedCity, out var validationError))
            {
                return validationError;
            }

            var forecast = weatherForecastService.GetForecast(normalizedCity);
            var description = descriptionService.GetHumanDescription(forecast);
            return Results.Ok(new { city = forecast.City, description });
        });
    }
}
