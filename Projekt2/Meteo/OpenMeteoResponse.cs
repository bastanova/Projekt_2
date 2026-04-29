namespace Projekt2.Meteo;

// Odpovídá struktuře JSONu z Open-Meteo
public record OpenMeteoResponse(CurrentWeather current_weather);
public record CurrentWeather(double temperature, double windspeed);