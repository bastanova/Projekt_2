using Projekt2.Meteo;
using Projekt2.Weather;
using Projekt2.Exceptions;
using Projekt2.Locations;


AppDomain.CurrentDomain.UnhandledException += (_, args) => {
    Console.WriteLine("Fatální chyba aplikace. Probíhá ukončení.");
};

using var httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };
var provider = new OpenMeteoClient(httpClient);
var service = new WeatherService(provider);

try 
{
    var cities = CzechCities.RegionalCapitals.Keys.ToArray();

    foreach (var city in cities)
{
    try 
    {
        var weather = await service.GetWeatherAsync(city); 
        
        Console.WriteLine($"{weather.City,-10} | Teplota: {weather.Temperature,5:F1}°C | Vítr: {weather.WindSpeed,4:F1} km/h | Stav: {weather.Condition}");
    }
    catch (WeatherException ex)
    {
        Console.WriteLine($"{city,-10} | Chyba: {ex.Message}");
    }
}

    var avgTemp = await service.GetAverageTemperatureAsync(cities);
    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine($"Celková průměrná teplota v ČR: {avgTemp:F1}°C");
}
catch (Exception ex)
{
    Console.WriteLine($"Neočekávaná systémová chyba: {ex.Message}");
}