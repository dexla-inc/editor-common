namespace Dexla.Common.Editor.Entities;

public class ApiWithApiEndpoints : Api
{
    public List<ApiEndpoint> ApiEndpoints { get; set; } = [];
}