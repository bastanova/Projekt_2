namespace Projekt2.Exceptions;

public abstract class WeatherException(string message, Exception? inner = null) 
    : Exception(message, inner);

public sealed class ApiUnavailableException(string provider, Exception inner) 
    : WeatherException($"Služba {provider} je momentálně nedostupná.", inner);

public sealed class CityNotFoundException(string city) 
    : WeatherException($"Město '{city}' nebylo nalezeno.");