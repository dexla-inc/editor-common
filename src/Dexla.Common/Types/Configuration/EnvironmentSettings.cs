namespace Dexla.Common.Types.Configuration;

public record EnvironmentSettings
{
    public string EnvironmentName { get; set; } = string.Empty;
    public string ApiBaseUrl { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
}