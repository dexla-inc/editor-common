using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Types.Configuration;

public class SettingCollection<TType> : ISettingWithDictionary<TType>
{
    public Dictionary<string, TType> Settings { get; set; } = new(StringComparer.OrdinalIgnoreCase);
}