using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Models;
using Dexla.Common.Repository.Types.Models;
using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Interfaces;
using Dexla.Common.Types.Models;

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
    
    public static Func<ApiEndpoint, ApiEndpointResponse> EntityToEndpointResponse()
    {
        return entity => new ApiEndpointResponse(
            entity.Id,
            entity.ApiId,
            entity.BaseUrl,
            string.Join("/", entity.BaseUrl, entity.RelativeUrl),
            entity.RelativeUrl,
            entity.Description,
            entity.MethodType,
            entity.AuthenticationScheme != null
                ? Enum.Parse<AuthenticationSchemes>(entity.AuthenticationScheme, true)
                : AuthenticationSchemes.NONE,
            entity.WithCredentials,
            entity.MediaType,
            entity.Authentication,
            entity.Headers,
            entity.Parameters,
            entity.RequestBody,
            entity.Body,
            entity.ExampleResponse,
            entity.ErrorExampleResponse,
            entity.IsServerRequest);
    }
    
    public static ApiEndpointResponse ModelToResponse(ApiEndpointModel model)
    {
        return new ApiEndpointResponse(
            model.Id!,
            model.ApiId,
            model.BaseUrl,
            string.Join("/", model.BaseUrl, model.RelativeUrl),
            model.RelativeUrl,
            model.Description,
            Enum.Parse<MethodTypes>(model.MethodType, true),
            model.AuthenticationScheme != null
                ? Enum.Parse<AuthenticationSchemes>(model.AuthenticationScheme, true)
                : AuthenticationSchemes.NONE,
            model.WithCredentials,
            model.MediaType,
            new EndpointAuthentication
            {
                EndpointType = model.Authentication.EndpointType,
                TokenKey = model.Authentication.TokenKey,
                TokenSecondaryKey = model.Authentication.TokenSecondaryKey
            },
            model.Headers,
            model.Parameters,
            model.RequestBody,
            model.Body,
            model.ExampleResponse,
            model.ErrorExampleResponse,
            model.IsServerRequest);
    }
    
    public static IResponse ModelToResponse(RepositoryActionResultModel<ApiEndpointModel> actionResult)
    {
        return actionResult.ActionResult(
            actionResult,
            ModelToResponse);
    }
    
    public static IResponse ModelToNoContentResponse(RepositoryActionResultModel<ApiEndpointModel> actionResult)
    {
        return actionResult.ActionResult<Success>(
            actionResult,
            _ => Success.Instance);
    }

    public string TrackingId { get; set; }
}