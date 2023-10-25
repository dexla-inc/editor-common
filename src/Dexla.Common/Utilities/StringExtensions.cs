using System.Text.RegularExpressions;

namespace Dexla.Common.Utilities;

public static class StringExtensions
{
    private static readonly Regex regEx = new(@"\{([\w'-]+)\}", RegexOptions.Compiled);

    public static string ApplyReplacements(this string format, Dictionary<string, string> replaceWith)
    {
        return regEx.Replace(format, delegate(Match match)
        {
            string key = match.Groups[1].Value;
            return replaceWith.TryGetValue(key, out string? value) ? value : string.Empty;
        });
    }

    public static string ReplaceEmptyStrings(this string value)
    {
        return value.Replace(" ", "");
    }
}