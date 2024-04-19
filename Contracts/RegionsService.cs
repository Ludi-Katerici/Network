using System.Text.Json;

namespace Contracts;

public sealed class RegionsService
{
    public static List<Region> RegionsList { get; set; }

    static RegionsService()
    {
        RegionsList = new List<Region>();
        
        string jsonString = new HttpClient().GetStringAsync("https://raw.githubusercontent.com/Kostadin/Places-in-Bulgaria/master/Places-in-Bulgaria.json").GetAwaiter().GetResult();
        var rootObject = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, Dictionary<string, Location>>>>(jsonString);
        foreach (var region in rootObject)
        {
            var regionDto = new Region
            {
                RegionName = region.Key,
                Cities = new List<string>()
            };
            foreach (var city in region.Value)
            {
                foreach (var info in city.Value)
                {
                    if (info.Value.type == "гр.")
                    {
                        regionDto.Cities.Add(info.Value.name);
                    }
                }
            }
            RegionsList.Add(regionDto);
        }
    }

    private class Location
    {
        public string name { get; set; }
        public string type { get; set; }
        public string town_hall { get; set; }
        public string phone_code { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public List<string> post_codes { get; set; }
    }
}

public class Region
{
    public string RegionName { get; set; }
    public List<string> Cities { get; set; }
}