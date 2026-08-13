using SimpleWeatherApi;
using SimpleWeatherApi.Endpoints;
using SimpleWeatherApi.Validation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddSingleton<IWeatherDescriptionService, WeatherDescriptionService>();
builder.Services.AddSingleton<ICityQueryValidator, CityQueryValidator>();
builder.Services.AddEndpoints(typeof(Program).Assembly);

var app = builder.Build();

app.MapEndpoints();

app.Run();

public partial class Program;
