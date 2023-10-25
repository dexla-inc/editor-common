namespace Dexla.Common.Types.Configuration;

public class PublicApiSetting 
{
    public string BaseUrl { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string AuthorizationType { get; set; } = string.Empty;
    public string AuthorizationValue { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public Dictionary<string, string> Headers { get; set; } = new();
}