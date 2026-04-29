namespace Projekt2.Weather;

public interface IWeatherProvider
{
    Task<WeatherData> GetWeatherAsync(string city, CancellationToken ct = default);
}