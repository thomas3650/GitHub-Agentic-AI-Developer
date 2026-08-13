using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using SimpleWeatherApi.Validation;

namespace SimpleWeatherApi.Tests;

public sealed class CityQueryValidatorTests
{
    private readonly CityQueryValidator _validator = new();

    [Fact]
    public void TryNormalizeReturnsTrueAndTrimmedCityForValidInput()
    {
        var success = _validator.TryNormalize("  London  ", out var normalizedCity, out var validationError);

        Assert.True(success);
        Assert.Equal("London", normalizedCity);
        Assert.Null(validationError);
    }

    [Fact]
    public void TryNormalizeReturnsTrueAndPreservesUntrimmedCityWhenNoWhitespace()
    {
        var success = _validator.TryNormalize("Paris", out var normalizedCity, out var validationError);

        Assert.True(success);
        Assert.Equal("Paris", normalizedCity);
        Assert.Null(validationError);
    }

    [Fact]
    public void TryNormalizeReturnsBadRequestForNullCity()
    {
        AssertMissingCityBadRequest(city: null);
    }

    [Fact]
    public void TryNormalizeReturnsBadRequestForEmptyCity()
    {
        AssertMissingCityBadRequest(city: string.Empty);
    }

    [Fact]
    public void TryNormalizeReturnsBadRequestForWhitespaceCity()
    {
        AssertMissingCityBadRequest(city: "   ");
    }

    private void AssertMissingCityBadRequest(string? city)
    {
        var success = _validator.TryNormalize(city, out var normalizedCity, out var validationError);

        Assert.False(success);
        Assert.Equal(string.Empty, normalizedCity);
        Assert.NotNull(validationError);

        var statusCodeResult = Assert.IsAssignableFrom<IStatusCodeHttpResult>(validationError);
        Assert.Equal(StatusCodes.Status400BadRequest, statusCodeResult.StatusCode);

        var valueResult = Assert.IsAssignableFrom<IValueHttpResult>(validationError);
        Assert.NotNull(valueResult.Value);

        var errorProperty = valueResult.Value!.GetType().GetProperty("error");
        Assert.NotNull(errorProperty);
        Assert.Equal("The city query parameter is required.", errorProperty!.GetValue(valueResult.Value));
    }
}
