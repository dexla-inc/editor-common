using System.Text.RegularExpressions;

namespace Dexla.Common.Utilities;

public static class StringUtils
{
    public static string ToSnakeCase(this string str)
    {
        string temp = str.Replace("_", " ");
        
        // Use a regular expression to insert an underscore before any capital letter
        // that is either preceded by a lowercase letter or a number
        temp = Regex.Replace(temp, @"((?<=[a-z])[A-Z]|(?<=[A-Z])[A-Z](?=[a-z])|(?<=[A-Za-z])(?=[0-9])|(?<=[0-9])(?=[A-Za-z]))", "_$1");

        // Replace multiple spaces with a single space
        temp = Regex.Replace(temp, @"\s+", " ");
        
        // Trim spaces at the start and end, and replace remaining spaces with underscores
        temp = temp.Trim().Replace(" ", "_");

        return temp.ToLower();
    }
}