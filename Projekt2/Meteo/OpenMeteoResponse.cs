namespace Projekt2.Meteo;

public record OpenMeteoResponse(CurrentWeather current_weather);
public record CurrentWeather(double temperature, double windspeed);