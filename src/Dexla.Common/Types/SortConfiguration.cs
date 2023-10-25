using Dexla.Common.Types.Enums;

namespace Dexla.Common.Types;

public class SortConfiguration
{
    public string PropertyName { get; }
    public SortDirections SortDirections { get; }

    public SortConfiguration(string propertyName, SortDirections sortDirections)
    {
        PropertyName = propertyName;
        SortDirections = sortDirections;
    }
}