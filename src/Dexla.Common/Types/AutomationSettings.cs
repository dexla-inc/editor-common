namespace Dexla.Common.Types;

public class AutomationSettings
{
    public string BaseUrl { get; set; } = string.Empty;
    public string AuthorizationType { get; set; } = string.Empty;
    public string AuthorizationValue { get; set; } = string.Empty;
    public string OrganizationKey { get; set; } = string.Empty;
    public string OrganizationValue { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string BackupModel { get; set; } = string.Empty;
}
