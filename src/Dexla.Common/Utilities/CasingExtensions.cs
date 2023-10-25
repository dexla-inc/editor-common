using Humanizer;

namespace Dexla.Common.Utilities;

public static class CasingExtensions
{
    public static string Pluralise(this string word)
    {
        return word.Pluralize();
    }
    
    public static string ToPascalCase(this string word)
    {
        return word.Pascalize();
    }
    
    public static string ToLowerThenPascalCase(this string word)
    {
        return word.ToLower().Pascalize();
    }
    
    public static string ToPascalCase<TEnum>(this TEnum word) where TEnum : struct, Enum
    {
        return word.ToString().ToLower().Pascalize();
    }
    
    public static string Kebaberise(this string word)
    {
        return word.Kebaberize();
    }
}