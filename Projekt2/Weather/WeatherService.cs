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

    // Volá klientskou metodu a vrací kompletní data
    public async Task<WeatherData> GetWeatherAsync(string city)
    {
        try 
        {
            // Zde voláme GetWeatherAsync, protože tu máte v OpenMeteoClientovi
            return await _provider.GetWeatherAsync(city);
        }
        catch (Exception ex)
        {
            throw new Exception($"Nepodařilo se načíst data pro město {city}.", ex);
        }
    }

    // Výpočet průměru z dat získaných od klienta
    public async Task<double> GetAverageTemperatureAsync(string[] cities)
    {
        double total = 0;
        foreach (var city in cities)
        {
            // Získáme celý objekt a vytáhneme z něj jen teplotu
            var weather = await _provider.GetWeatherAsync(city);
            total += weather.Temperature;
        }
        return total / cities.Length;
    }
}