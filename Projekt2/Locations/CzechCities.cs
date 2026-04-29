namespace Projekt2.Locations;

public static class CzechCities
{
    public static readonly Dictionary<string, (double lat, double lon)> RegionalCapitals = new()
    {
        ["Praha"] = (50.0755, 14.4378),
        ["České Budějovice"] = (48.9745, 14.4741),
        ["Plzeň"] = (49.7384, 13.3736),
        ["Karlovy Vary"] = (50.2327, 12.8712),
        ["Ústí nad Labem"] = (50.6607, 14.0323),
        ["Liberec"] = (50.7671, 15.0562),
        ["Hradec Králové"] = (50.2092, 15.8328),
        ["Pardubice"] = (50.0343, 15.7766),
        ["Jihlava"] = (49.3961, 15.5912),
        ["Brno"] = (49.1951, 16.6068),
        ["Olomouc"] = (49.5938, 17.2509),
        ["Zlín"] = (49.2265, 17.6671),
        ["Ostrava"] = (49.8209, 18.2625)
    };
}