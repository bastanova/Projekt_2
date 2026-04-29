using Projekt2.Meteo;
using Projekt2.Exceptions;
using Projekt2.Weather;

namespace Projekt2.Weather;

public class WeatherService
{
    private readonly OpenMeteoClient _provider;

    public WeatherService(OpenMeteoClient provider)
    {
        _provider = provider;
    }

    public async Task<WeatherData> GetWeatherAsync(string city)
    {
        try 
        {
            return await _provider.GetWeatherAsync(city);
        }
        catch (Exception ex)
        {
            throw new Exception($"Nepodařilo se načíst data pro město {city}.", ex);
        }
    }

    public async Task<double> GetAverageTemperatureAsync(string[] cities)
    {
        double total = 0;
        foreach (var city in cities)
        {
            var weather = await _provider.GetWeatherAsync(city);
            total += weather.Temperature;
        }
        return total / cities.Length;
    }
}