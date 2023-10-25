using System.Collections.Specialized;
using System.Web;
using Dexla.Common.Types;
using Dexla.Common.Types.Enums;

namespace Dexla.Common.Utilities;

public static class WebExtensions
{
    public static T DeserializeQueryString<T>(this string queryString, Casing casing = Casing.CAMEL_CASE) where T : class
    {
        string serialisedQueryString = queryString.SerializeQueryString();
        return Json.Deserialize<T>(serialisedQueryString, casing);
    }
        
    public static string SerializeQueryString(this string queryString)
    {
        NameValueCollection nvc = HttpUtility.ParseQueryString(queryString);
        Dictionary<string, string?> formDictionary = nvc.AllKeys.ToDictionary(p => p!, p => nvc[p]);
        return Json.Serialize(formDictionary);
    }

    public static IEnumerable<KeyValuePair<string, string>> GetQueryParams(this string queryString, bool isBase64Encoded = false)
    {
        int i = queryString.IndexOf("?", StringComparison.Ordinal);
        string queryWithoutBaseUrl = i != -1 ? queryString[i..] : queryString;
            
        NameValueCollection nvc = HttpUtility.ParseQueryString(queryWithoutBaseUrl);

        if (!nvc.HasKeys())
            return new List<KeyValuePair<string, string>>();
            
        return nvc.AllKeys.SelectMany(
            nvc.GetValues!,
            (k, v) => new KeyValuePair<string, string>(k!, isBase64Encoded ? v.Replace(" ", "+") : v));
    }
        
    public static Dictionary<string, string> QueryStringToDictionary(this string queryString, bool isBase64Encoded = false)
    {
        int i = queryString.IndexOf("?", StringComparison.Ordinal);
        string queryWithoutBaseUrl = i != -1 ? queryString[i..] : queryString;
            
        NameValueCollection nvc = HttpUtility.ParseQueryString(queryWithoutBaseUrl);

        return !nvc.HasKeys() 
            ? new Dictionary<string, string>() 
            : nvc.AllKeys.ToDictionary(k => k!, k => isBase64Encoded ? nvc[k]?.Replace(" ", "+")! : nvc[k]!);
    }
        
    public static string GetQueryString(this object obj)
    {
        IEnumerable<string> properties = from p in obj.GetType().GetProperties()
            where p.GetValue(obj, null) != null
            select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

        return string.Join("&", properties.ToArray());
    }
        
    public static KeyValuePair<string, string> Get(this IEnumerable<KeyValuePair<string, string>> source, string key)
    {
        return source.FirstOrDefault(pair => pair.Key == key);
    }
        
    public static string GetValue(this IEnumerable<KeyValuePair<string, string>> source, string key)
    {
        return source.FirstOrDefault(pair => pair.Key == key).Value;
    }
}