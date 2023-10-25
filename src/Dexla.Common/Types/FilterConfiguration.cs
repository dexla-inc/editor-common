using Dexla.Common.Types.Enums;

namespace Dexla.Common.Types;

public class FilterConfiguration
{
    public FilterConfiguration()
    {
        
    }

    public FilterConfiguration(string projectId)
    {
        AddDefaults(projectId);
    }
    
    public List<Filter> Filters { get; private set; } = new();

    public void Append(string key, object value, SearchTypes searchType)
    {
        Filters.Add(new Filter(key, value, searchType));
    }
    
    public void AppendArray(string key, List<string> value, SearchTypes searchType)
    {
        Filters.Add(new Filter(key, value, searchType));
    }
    
    public void AppendArray(Dictionary<string, object> filters, SearchTypes searchType)
    {
        Filters.Add(new Filter(filters, searchType));
    }
    
    private void AddDefaults(string projectId)
    {
        Filters.Add(new Filter(nameof(projectId), projectId, SearchTypes.EXACT));
    }
}