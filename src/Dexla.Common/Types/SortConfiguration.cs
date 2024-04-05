using Dexla.Common.Types.Enums;
using Dexla.Common.Utilities;

namespace Dexla.Common.Types;

public class SortConfiguration
{
    [Obsolete("Use Sorts instead")]
    public string PropertyName { get; private set; }
    [Obsolete("Use Sorts instead")]
    public SortDirections SortDirections { get; private set; }


    [Obsolete("Use parameterless constructor instead")]
    public SortConfiguration(string propertyName, SortDirections sortDirections)
    {
        PropertyName = propertyName.ToCamelCase();
        SortDirections = sortDirections;
    }
    
    public SortConfiguration() : this(string.Empty, SortDirections.Ascending)
    {
        
    }

    public void Append(string propertyName, SortDirections sortDirections)
    {
        Sorts.Add(new Sort(propertyName.ToCamelCase(), sortDirections));
    }
    
    public List<Sort> Sorts { get; private set; } = [];
}

public class Sort(string propertyName, SortDirections sortDirections)
{
    public string PropertyName { get; } = propertyName;
    public SortDirections SortDirections { get; } = sortDirections;
}