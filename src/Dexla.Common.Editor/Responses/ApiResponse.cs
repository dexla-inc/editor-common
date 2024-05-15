using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Models;
using Dexla.Common.Repository.Types.Models;
using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Interfaces;
using Dexla.Common.Types.Models;

namespace Dexla.Common.Editor.Responses;

public class ApiResponse : ISuccess
{
    public string Id { get; }
    public string Name { get; }
    public AuthenticationSchemes AuthenticationScheme { get; }
    public EnvironmentTypes? Environment { get; }
    public string BaseUrl { get; }
    public string? SwaggerUrl { get; }
    public long Updated { get; }
    public DataSourceTypes Type { get; }
    public List<ApiEndpointResponse> AuthEndpoints { get; }
    public string? AuthValue { get; }
    public string? ApiKey { get; }
    public bool IsTested { get; }

    public ApiResponse(
        string id,
        string name,
        AuthenticationSchemes authenticationScheme,
        EnvironmentTypes? environment,
        string baseUrl,
        string? swaggerUrl,
        long updated,
        DataSourceTypes type,
        string? authValue,
        string? apiKey,
        bool isTested)
    {
        Id = id;
        Name = name;
        AuthenticationScheme = authenticationScheme;
        Environment = environment;
        BaseUrl = baseUrl;
        SwaggerUrl = swaggerUrl;
        Updated = updated;
        Type = type;
        AuthValue = authValue;
        ApiKey = apiKey;
        IsTested = isTested;
    }

    public ApiResponse(
        string id,
        string name,
        AuthenticationSchemes authenticationScheme,
        EnvironmentTypes? environment,
        string baseUrl,
        string? swaggerUrl,
        long updated,
        DataSourceTypes type,
        string? authValue,
        string? apiKey,
        bool isTested,
        List<ApiEndpointResponse> authEndpoints)
    {
        Id = id;
        Name = name;
        AuthenticationScheme = authenticationScheme;
        Environment = environment;
        BaseUrl = baseUrl;
        SwaggerUrl = swaggerUrl;
        Updated = updated;
        Type = type;
        AuthValue = authValue;
        ApiKey = apiKey;
        IsTested = isTested;
        AuthEndpoints = authEndpoints;
    }

    public static Func<Api, ApiResponse> EntityToResponse()
    {
        return entity => new ApiResponse(
            entity.Id,
            entity.Name,
            entity.AuthenticationScheme,
            entity.Environment,
            entity.BaseUrl,
            entity.SwaggerUrl,
            entity.Updated,
            entity.Type,
            entity.AuthValue,
            entity.ApiKey,
            entity.IsTested);
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

    public static ApiResponse ModelToResponse(ApiModel model)
    {
        return new ApiResponse(
            model.Id!,
            model.Name,
            Enum.Parse<AuthenticationSchemes>(model.AuthenticationScheme),
            model.Environment != null
                ? Enum.Parse<EnvironmentTypes>(model.Environment)
                : EnvironmentTypes.None,
            model.BaseUrl,
            model.SwaggerUrl,
            model.Updated,
            !string.IsNullOrWhiteSpace(model.Type) ? Enum.Parse<DataSourceTypes>(model.Type) : DataSourceTypes.API,
            model.AuthValue,
            model.ApiKey,
            model.IsTested);
    }

    public static ApiFullResponse ModelToFullResponse(
        ApiModel model,
        IEnumerable<DataSourceEndpoint>? changedEndpoints = null,
        IEnumerable<DataSourceEndpoint>? deletedEndpoints = null)
    {
        return new ApiFullResponse(
            model.Id!,
            model.Name,
            model.AuthenticationScheme,
            model.Environment != null ? Enum.Parse<EnvironmentTypes>(model.Environment) : EnvironmentTypes.None,
            model.BaseUrl,
            model.SwaggerUrl,
            model.Updated,
            !string.IsNullOrWhiteSpace(model.Type) ? Enum.Parse<DataSourceTypes>(model.Type) : DataSourceTypes.API,
            model.AuthValue,
            model.ApiKey,
            model.IsTested,
            changedEndpoints,
            deletedEndpoints);
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

    public static IResponse ModelToFullResponse(
        RepositoryActionResultModel<ApiModel> actionResult,
        IEnumerable<DataSourceEndpoint>? changedEndpoints = null,
        IEnumerable<DataSourceEndpoint>? deletedEndpoints = null)
    {
        return actionResult.ActionResult<ApiFullResponse>(
            actionResult,
            m => ModelToFullResponse(m, changedEndpoints, deletedEndpoints));
    }
    
    public static IResponse ModelToResponse(RepositoryActionResultModel<ApiModel> actionResult)
    {
        return actionResult.ActionResult(
            actionResult,
            ModelToResponse);
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

    public string TrackingId { get; set; } = string.Empty;
}