using Dexla.Common.Editor.Entities;
using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class ApiEndpointResponse : ISuccess
{
    public string Id { get; set; }
    public string DataSourceId { get; set; }
    public string Url { get; set; }
    public string BaseUrl { get; set; }
    public string RelativeUrl { get; set; }
    public string Description { get; set; }
    public MethodTypes MethodType { get; set; }
    public AuthenticationSchemes AuthenticationScheme { get; set; }
    public bool? WithCredentials { get; set; }
    public string MediaType { get; set; }
    public EndpointAuthentication? Authentication { get; set; } = new();
    public List<ApiHeaderDto> Headers { get; set; }
    public List<ApiParameterDto> Parameters { get; set; }
    public List<ApiBodyParameterDto> RequestBody { get; set; }
    public string? Body { get; set; }
    public string? ExampleResponse { get; set; } = string.Empty;
    public string? ErrorExampleResponse { get; set; } = string.Empty;
    public bool IsServerRequest { get; set; }
    
    public ApiEndpointResponse()
    {
    }

    public ApiEndpointResponse(
        string id,
        string dataSourceId,
        string baseUrl,
        string url,
        string relativeUrl,
        string description,
        MethodTypes methodType,
        AuthenticationSchemes authenticationScheme,
        bool? withCredentials,
        string mediaType,
        EndpointAuthentication? authentication,
        List<ApiHeaderDto> headers,
        List<ApiParameterDto> parameters,
        List<ApiBodyParameterDto> requestBody,
        string? body,
        string? exampleResponse,
        string? errorExampleResponse,
        bool isServerRequest)
    {
        Id = id;
        DataSourceId = dataSourceId;
        BaseUrl = baseUrl;
        Url = url;
        RelativeUrl = relativeUrl;
        Description = description;
        MethodType = methodType;
        AuthenticationScheme = authenticationScheme;
        WithCredentials = withCredentials;
        MediaType = mediaType;
        Authentication = authentication;
        Headers = headers;
        Parameters = parameters;
        RequestBody = requestBody;
        Body = body;
        ExampleResponse = exampleResponse;
        ErrorExampleResponse = errorExampleResponse;
        IsServerRequest = isServerRequest;
    }

    public string TrackingId { get; set; }
}