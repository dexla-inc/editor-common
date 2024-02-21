namespace Dexla.Common.Utilities;

public static class ListExtensions 
{
    public static List<string> SplitStringIntoChunks(this string source, int chunkSize)
    {
        List<string> chunks = [];

        for (int i = 0; i < source.Length; i += chunkSize)
            chunks.Add(i + chunkSize <= source.Length ? source.Substring(i, chunkSize) : source[i..]);

        return chunks;
    }
}