using System.Reflection;

namespace Dexla.Common.Utilities;

public class Mapping
{
    public static void Properties<TSource, TDestination>(
        TSource source,
        TDestination destination,
        ICollection<string> excludedProperties)
        where TSource : class
        where TDestination : class
    {
        PropertyInfo[] sourceProperties = typeof(TSource).GetProperties();
        PropertyInfo[] destinationProperties = typeof(TDestination).GetProperties();

        foreach (PropertyInfo sourceProperty in sourceProperties)
        {
            if (excludedProperties.Contains(sourceProperty.Name))
            {
                continue;
            }

            PropertyInfo? destinationProperty = destinationProperties.FirstOrDefault(p =>
                p.Name == sourceProperty.Name && p.PropertyType == sourceProperty.PropertyType);
            if (destinationProperty == null || !destinationProperty.CanWrite) continue;
            object? value = sourceProperty.GetValue(source);
            destinationProperty.SetValue(destination, value);
        }
    }
}