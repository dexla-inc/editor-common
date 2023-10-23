using Dexla.Common.Types.Enums;

namespace Dexla.Common.Editor.Responses;

public class Region
{
    public RegionTypes Type { get; set; }
    public string Name { get; set; }= string.Empty;

    public static Region GetDefault()
    {
        return new Region { Type = RegionTypes.UK_SOUTH, Name = "UK" };
    }
}