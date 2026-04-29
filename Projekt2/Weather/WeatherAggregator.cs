

namespace Projekt2.Weather;

public static class WeatherAggregator
{
    public static (double AvgTemp, double AvgWind, string CommonCondition) CalculateStats(IEnumerable<WeatherData> results)
    {
        if (!results.Any())
            return (0, 0, "Neznámý");

        var avgTemp = results.Average(w => w.Temperature);
        var avgWind = results.Average(w => w.WindSpeed);
        
        var commonCondition = results
            .GroupBy(w => w.Condition)
            .OrderByDescending(g => g.Count())
            .First().Key;

        return (avgTemp, avgWind, commonCondition);
    }
}