using Dexla.Common.Types.Enums;

namespace Dexla.Common.Types;

public class Filter
{
    public string Name { get; }
    public object? Value { get; }
    public List<string>? Values { get; }
    public Dictionary<string, object>? Names { get; }
    public SearchTypes SearchType { get; }
    public List<Filter>? ChildFilters { get; }
    
    public static Filter Add(string name, object value, SearchTypes searchType)
    {
        return new Filter(name, value, searchType);
    }

    public Filter(string name, object? value, SearchTypes searchType)
    {
        Name = name;
        Value = value;
        SearchType = searchType;
    }
    
    public Filter(string name, List<string> values, SearchTypes searchType)
    {
        Name = name;
        Values = values;
        SearchType = searchType;
    }
    
    public Filter(Dictionary<string, object> names, SearchTypes searchType)
    {
        Names = names;
        SearchType = searchType;
    }
    
    public Filter(List<Filter> childFilters, SearchTypes searchType)
    {
        ChildFilters = childFilters;
        SearchType = searchType;
    }
    
    public static Filter And(params Filter[] filters)
    {
        return new Filter([..filters], SearchTypes.AND);
    }

    public static Filter Or(params Filter[] filters)
    {
        return new Filter([..filters], SearchTypes.OR);
    }
}