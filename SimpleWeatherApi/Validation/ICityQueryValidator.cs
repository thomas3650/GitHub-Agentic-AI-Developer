namespace SimpleWeatherApi.Validation;

public interface ICityQueryValidator
{
    bool TryNormalize(string? city, out string normalizedCity, out IResult? validationError);
}
