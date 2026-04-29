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

        string conditionText = MapWeatherCode(response.current_weather.weathercode);

        return new WeatherData
        (
            City: city, 
            Temperature: response.current_weather.temperature, 
            WindSpeed: response.current_weather.windspeed, 
            Condition: conditionText 
        );
    }

   
    private string MapWeatherCode(int code)
    {
        return code switch
        {
            0 => "Jasno",
            1 or 2 => "Polojasno",
            3 => "Zataženo",
            45 or 48 => "Mlha",
            >= 51 and <= 67 => "Déšť/Mrholení",
            >= 71 and <= 77 => "Sněžení",
            >= 80 and <= 82 => "Přeháňky",
            >= 95 => "Bouřky",
            _ => "Neznámý stav"
        };
    }
}