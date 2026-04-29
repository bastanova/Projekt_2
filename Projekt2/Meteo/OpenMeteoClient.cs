using System.Net.Http.Json;
using System.Globalization;
using Projekt2.Weather;
using Projekt2.Locations; 

namespace Projekt2.Meteo;

public class OpenMeteoClient : IWeatherProvider
{
    private readonly HttpClient httpClient;

    public OpenMeteoClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<WeatherData> GetWeatherAsync(string city, CancellationToken ct = default)
    {
        if (!CzechCities.RegionalCapitals.TryGetValue(city, out var coords))
        {
            throw new Exceptions.CityNotFoundException(city);
        }
        
        var url = $"https://api.open-meteo.com/v1/forecast?latitude={coords.lat.ToString(CultureInfo.InvariantCulture)}&longitude={coords.lon.ToString(CultureInfo.InvariantCulture)}&current_weather=true";
        
        var response = await httpClient.GetFromJsonAsync<OpenMeteoResponse>(url, ct);

        if (response == null) throw new Exceptions.CityNotFoundException(city);

        return new WeatherData
        (
            City: city, 
            Temperature: response.current_weather.temperature, 
            WindSpeed: response.current_weather.windspeed, 
            Condition: "Jasno"
        );
    }
}