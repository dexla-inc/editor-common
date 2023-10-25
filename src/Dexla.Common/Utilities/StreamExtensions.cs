using System.Reflection;
using Dexla.Common.Types;

namespace Dexla.Common.Utilities;

public static class StreamExtensions 
{
    public static TType GetEmbeddedResourceAsType<TType>(this Assembly assembly, string resourceName)
    {
        string resourceContent = assembly.GetEmbeddedResource(resourceName);

        return Json.Deserialize<TType>(resourceContent);
    }
    
    public static string GetEmbeddedResource(this Assembly assembly, string resourceName)
    {
        string? resourceContent = null;
        using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
            if (stream != null)
            {
                using StreamReader reader = new(stream);
                resourceContent = reader.ReadToEnd();
            }

        if (resourceContent == null)
        {
            throw new Exception("Resource not found in assembly");
        }

        return resourceContent;
    }
}