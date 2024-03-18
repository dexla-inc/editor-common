using Dexla.Common.Types.Enums;
using Dexla.Common.Utilities;

namespace Dexla.Common.Types;

public class SortConfiguration(string propertyName, SortDirections sortDirections)
{
    public string PropertyName { get; } = propertyName.ToCamelCase();
    public SortDirections SortDirections { get; } = sortDirections;
}