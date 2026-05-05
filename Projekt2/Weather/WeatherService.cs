
using Projekt2.Exceptions;  

namespace Projekt2.Weather;

public class WeatherService
{
    private readonly IWeatherProvider _provider;

    public WeatherService(IWeatherProvider provider)
    {
        _provider = provider;
    }

    public async Task<WeatherData> GetWeatherAsync(string city, CancellationToken ct = default)
    {
        try
        {  
            return await _provider.GetWeatherAsync(city, ct);
        }
        catch (WeatherException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception($"Neočekávaná chyba při zpracování dat pro město {city}.", ex);
        }
    }
    
}