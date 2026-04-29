using Projekt2.Meteo;
using Projekt2.Weather;
using Projekt2.Exceptions;
using Projekt2.Results;

// 1. Nastavení globálního handleru
AppDomain.CurrentDomain.UnhandledException += (_, args) => {
    Console.WriteLine("Fatální chyba aplikace. Probíhá ukončení.");
};

// 2. Inicializace
using var httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };
var provider = new OpenMeteoClient(httpClient);
var service = new WeatherService(provider);

// 3. Hlavní asynchronní blok
try 
{
    var cities = new[] { "Prague", "Brno", "Ostrava" };
    Console.WriteLine($"--- Aktuální počasí ({DateTime.Now:d.M.yyyy H:mm}) ---");
    Console.WriteLine("-------------------------------------------------");

    // Procházíme města jedno po druhém pro detailní výpis
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

    // Původní výpočet průměru (pokud jej stále chcete)
    var avgTemp = await service.GetAverageTemperatureAsync(cities);
    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine($"Celková průměrná teplota v ČR: {avgTemp:F1}°C");
}
catch (Exception ex)
{
    Console.WriteLine($"Neočekávaná systémová chyba: {ex.Message}");
}