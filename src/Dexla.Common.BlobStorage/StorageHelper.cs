using System.Text;
using Dexla.Common.Utilities;

namespace Dexla.Common.BlobStorage;

public static class StorageHelper
{
    public static string BuildBlobName(params string[] paths)
    {
        if (paths.Length.Equals(0))
            return string.Empty;
            
        StringBuilder sb = BuildKebabPaths(paths);

        return sb.ToString()[1..];
    }

    public static string GenerateAbsoluteUrl(string baseUri, params string[] paths)
    {
        Uri uri = new(baseUri);
            
        StringBuilder sb = BuildKebabPaths(paths);

        string relativePath = sb.ToString();
            
        return new Uri(uri, relativePath).ToString();
    }
        
    public static string GetFileName(string absoluteUri, bool removeExtension = false)
    {
        int index = absoluteUri.LastIndexOf("/", StringComparison.Ordinal);

        string fileNameWithExtension = absoluteUri[(index + 1)..];

        return removeExtension ? Path.GetFileNameWithoutExtension(fileNameWithExtension) : fileNameWithExtension;
    }

    private static StringBuilder BuildKebabPaths(IEnumerable<string> paths)
    {
        StringBuilder sb = new();
        foreach (string path in paths)
        {
            if (path.Contains("/"))
                throw new NotSupportedException("The character / is not allowed");

            sb.Append($"/{path.Replace("_", "-").Kebaberise()}");
        }

        return sb;
    }
}