namespace Projekt2.Meteo;

public class OpenMeteoResponse
{
    public CurrentWeather current_weather { get; set; }
}

public class CurrentWeather
{
    public double temperature { get; set; }
    public double windspeed { get; set; }
    public int weathercode { get; set; }
}