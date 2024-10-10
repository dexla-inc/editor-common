using Dexla.Common.Editor.Entities;
using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class ApiIncludeResponse : ISuccess
{
    public string Id { get; set; }
    public string Name { get; set; }
    public  AuthenticationSchemes AuthenticationScheme { get; }
    public EnvironmentTypes? Environment { get; }
    public string BaseUrl { get; }
    public string? SwaggerUrl { get; }
    public long Updated { get; }
    public DataSourceTypes Type { get; }
    public string? ApiKey { get; }
    public bool IsTested { get; }
    public IEnumerable<ApiEndpointResponse>? Endpoints { get; }
    public DatasourceAuthConfigurationResponse? Auth { get; }

    public ApiIncludeResponse()
    {
    }

    public ApiIncludeResponse(
        string id,
        string name,
        AuthenticationSchemes authenticationScheme,
        EnvironmentTypes? environment,
        string baseUrl,
        string? swaggerUrl,
        long updated,
        DataSourceTypes type,
        string? apiKey,
        bool isTested,
        IEnumerable<ApiEndpointResponse>? endpoints = null,
        DatasourceAuthConfigurationResponse? auth = null)
    {
        Id = id;
        Name = name;
        AuthenticationScheme = authenticationScheme;
        Environment = environment;
        BaseUrl = baseUrl;
        SwaggerUrl = swaggerUrl;
        Updated = updated;
        Type = type;
        ApiKey = apiKey;
        IsTested = isTested;
        Endpoints = endpoints ?? [];
        Auth = auth;
    }

    public static Func<ApiWithApiEndpoints, ApiIncludeResponse> EntityToResponse()
    {
        return entity =>
        {
            DatasourceAuthConfigurationResponse authConfig = GetAuthConfig(entity);
            return new ApiIncludeResponse(
                entity.Id,
                entity.Name,
                entity.AuthenticationScheme,
                entity.Environment,
                entity.BaseUrl,
                entity.SwaggerUrl,
                entity.Updated,
                entity.Type,
                entity.ApiKey,
                entity.IsTested,
                entity.ApiEndpoints.Select(e => ApiEndpointResponse.EntityToEndpointResponse()(e)).ToList(),
                authConfig
            );
        };
    }
    
    
    private static DatasourceAuthConfigurationResponse GetAuthConfig(ApiWithApiEndpoints api)
        {
            string accessTokenUrl = string.Empty, refreshTokenUrl = string.Empty, userUrl = string.Empty;
            string? accessTokenKey = null, refreshTokenKey = null, tokenSecondaryKey = null;
            AuthenticationSchemes authScheme = AuthenticationSchemes.NONE;

            foreach (ApiEndpoint endpoint in api.ApiEndpoints)
            {
                if (Enum.TryParse(endpoint.AuthenticationScheme, true, out AuthenticationSchemes parsedAuthScheme))
                {
                    switch (endpoint.Authentication?.EndpointType)
                    {
                        case EndpointTypes.ACCESS:
                            accessTokenUrl = endpoint.RelativeUrl;
                            accessTokenKey = endpoint.Authentication.TokenKey;
                            tokenSecondaryKey = endpoint.Authentication.TokenSecondaryKey;
                            authScheme = parsedAuthScheme;
                            break;
                        case EndpointTypes.REFRESH:
                            refreshTokenUrl = endpoint.RelativeUrl;
                            refreshTokenKey = endpoint.Authentication.TokenKey;
                            break;
                        case EndpointTypes.USER:
                            userUrl = endpoint.RelativeUrl;
                            break;
                    }
                }
            }

            return new DatasourceAuthConfigurationResponse(
                authScheme,
                CombineUrls(api.BaseUrl, accessTokenUrl),
                CombineUrls(api.BaseUrl, refreshTokenUrl),
                CombineUrls(api.BaseUrl, userUrl),
                accessTokenKey,
                refreshTokenKey,
                tokenSecondaryKey,
                api.ApiKey,
                api.Type
            );
        }

        private static string CombineUrls(string baseUrl, string relativeUrl)
        {
            return new Uri(new Uri(baseUrl), relativeUrl).ToString();
        }

    public string TrackingId { get; set; }

    public static ApiIncludeResponse ModelToResponse(ApiWithApiEndpoints apiWithApiEndpoints)
    {
        return EntityToResponse()(apiWithApiEndpoints);
    }

    public static ApiWithApiEndpoints ResponseToEntity(ApiIncludeResponse response)
    {
        return new ApiWithApiEndpoints
        {
            Id = response.Id,
            Name = response.Name,
            AuthenticationScheme = response.AuthenticationScheme,
            Environment = response.Environment,
            BaseUrl = response.BaseUrl,
            SwaggerUrl = response.SwaggerUrl,
            Updated = response.Updated,
            Type = response.Type,
            ApiKey = response.ApiKey,
            IsTested = response.IsTested,
            ApiEndpoints = response.Endpoints?.Select(ApiEndpointResponse.ResponseToEntity).ToList() ?? []
        };
    }
}