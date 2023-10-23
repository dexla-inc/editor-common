using Dexla.Common.Types.Enums;

namespace Dexla.Common.Editor.Responses;

public static class Regions
{
    public static List<Region> GetRegions()
    {
        return _regions;
    }

    public static Region ParseRegion(string region)
    {
        if (string.IsNullOrEmpty(region))
            Region.GetDefault();
        
        return _regions.FirstOrDefault(r => r.Type.ToString() == region)!;
    }
    
    public static Region? ParseRegion(RegionTypes region)
    {
        return _regions.FirstOrDefault(r => r.Type == region);
    }

    private static readonly List<Region> _regions = new()
    {
        new Region { Type = RegionTypes.UK_SOUTH, Name = "UK" },
        new Region { Type = RegionTypes.US_CENTRAL, Name = "US" },
        new Region { Type = RegionTypes.FRANCE_CENTRAL, Name = "EU" }
    };
}