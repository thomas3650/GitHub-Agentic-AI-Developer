namespace SimpleWeatherApi.Validation;

using System.Diagnostics.CodeAnalysis;

public sealed class CityQueryValidator : ICityQueryValidator
{
    internal const string MissingCityQueryParameterError = "The city query parameter is required.";

    public bool TryNormalize(string? city, out string normalizedCity, [NotNullWhen(false)] out IResult? validationError)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            normalizedCity = string.Empty;
            validationError = Results.BadRequest(new { error = MissingCityQueryParameterError });
            return false;
        }

        normalizedCity = city.Trim();
        validationError = null;
        return true;
    }
}
