using Projekt2.Meteo;
using Projekt2.Weather;
using Projekt2.Exceptions;
using Projekt2.Locations;

GlobalExceptionHandler.Setup();

using var httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };
var provider = new OpenMeteoClient(httpClient);
var service = new WeatherService(provider);

try 
{
    var cities = CzechCities.RegionalCapitals.Keys.ToArray();
    var weatherResults = new List<WeatherData>();

    Console.WriteLine($"--- Aktuální počasí v ČR (Zjištěno: {DateTime.Now:d.M.yyyy H:mm:ss}) ---");
    Console.WriteLine(new string('-', 85));

    foreach (var city in cities)
    {
        try 
        {
            var weather = await service.GetWeatherAsync(city); 
            weatherResults.Add(weather);
            
            Console.WriteLine($"{weather.City,-20} | Teplota: {weather.Temperature,5:F1}°C | Vítr: {weather.WindSpeed,4:F1} km/h | Stav: {weather.Condition}");
        }
        catch (WeatherException ex)
        {
            Console.WriteLine($"{city,-20} | Chyba: {ex.Message}");
        }
    }

    var stats = WeatherAggregator.CalculateStats(weatherResults);

    Console.WriteLine(new string('-', 85));
    Console.WriteLine($"CELKOVÝ PŘEHLED PRO ČR:");
    Console.WriteLine($"Průměrná teplota: {stats.AvgTemp:F1}°C");
    Console.WriteLine($"Průměrná rychlost větru: {stats.AvgWind:F1} km/h");
    Console.WriteLine($"Převažující stav: {stats.CommonCondition}");
    Console.WriteLine(new string('-', 85));
}
catch (Exception ex)
{
    Console.WriteLine($"Neočekávaná systémová chyba: {ex.Message}");
}