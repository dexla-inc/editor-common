namespace Dexla.Common.Types.Configuration;

public class PublicApiSettings : SettingCollection<PublicApiSetting>
{
    public PublicApiSetting Base { get; set; } = new();
}