using System.Net.Http.Json;
using System.Globalization;
using Projekt2.Weather;

namespace Projekt2.Meteo;

public class OpenMeteoClient : IWeatherProvider
{
    private readonly HttpClient httpClient;
    
    // Mapování měst na souřadnice (latitude, longitude)
    private static readonly Dictionary<string, (double lat, double lon)> CityCoordinates = new()
    {
        ["Prague"] = (50.0755, 14.4378),
        ["Brno"] = (49.1951, 16.6068),
        ["Ostrava"] = (49.8209, 18.2625),
        ["Plzen"] = (49.7384, 13.3736)
    };
    
    public OpenMeteoClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<WeatherData> GetWeatherAsync(string city, CancellationToken ct = default)
{
    if (!CityCoordinates.TryGetValue(city, out var coords))
    {
        throw new Exceptions.CityNotFoundException(city);
    }
    
    var url = $"https://api.open-meteo.com/v1/forecast?latitude={coords.lat.ToString(System.Globalization.CultureInfo.InvariantCulture)}&longitude={coords.lon.ToString(System.Globalization.CultureInfo.InvariantCulture)}&current_weather=true";
    
    var response = await httpClient.GetFromJsonAsync<OpenMeteoResponse>(url, ct);

    if (response == null) throw new Exceptions.CityNotFoundException(city);

    // Vracíme kompletní objekt WeatherData
    return new WeatherData(
        City: city, 
        Temperature: response.current_weather.temperature, 
        WindSpeed: response.current_weather.windspeed, 
        Condition: "Jasno" // Pro reálný stav byste museli mapovat weathercode z JSONu
    );
}
}