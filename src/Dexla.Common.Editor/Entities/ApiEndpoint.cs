using Dexla.Common.Editor.Entities;
using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Models;

namespace Dexla.Common.Editor.Entities;

public class ApiEndpoint : IEntity
{
    public string Id { get; set; } = string.Empty;
    public EntityStatus EntityStatus { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
    public string ApiId { get; set; } = string.Empty;
    public string RelativeUrl { get; set; } = string.Empty;
    public MethodTypes MethodType { get; set; }
    public string Description { get; set; } = string.Empty;
    public string MediaType { get; set; } = string.Empty;
    public bool? WithCredentials { get; set; }
    public string? AuthenticationScheme { get; set; }
    public List<ApiHeader> Headers { get; set; } = new();
    public List<ApiParameter> Parameters { get; set; } = new();
    public List<ApiBodyParameter> RequestBody { get; set; } = new();
    public string? Body { get; set; }
    public string? ExampleResponse { get; set; }
    public string? ErrorExampleResponse { get; set; }
    public EndpointAuthentication? Authentication { get; set; }
    public bool IsServerRequest { get; set; }

    public bool AuthOnly()
    {
        return Authentication?.EndpointType is EndpointTypes.ACCESS or EndpointTypes.REFRESH or EndpointTypes.USER;
    }

    public static string[] AllowedEndpointTypes()
    {
        return new[]
            { EndpointTypes.ACCESS.ToString(), EndpointTypes.REFRESH.ToString(), EndpointTypes.USER.ToString() };
    }
}