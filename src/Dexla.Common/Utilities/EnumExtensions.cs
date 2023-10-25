namespace Dexla.Common.Utilities;

public static class EnumExtensions
{
    public static bool CanParse<TEnum>(this string value) where TEnum : struct
    {
        return Enum.TryParse<TEnum>(value, out _);
    }
}