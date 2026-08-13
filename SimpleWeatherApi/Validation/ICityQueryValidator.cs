namespace SimpleWeatherApi.Validation;

using System.Diagnostics.CodeAnalysis;

public interface ICityQueryValidator
{
    bool TryNormalize(string? city, out string normalizedCity, [NotNullWhen(false)] out IResult? validationError);
}
