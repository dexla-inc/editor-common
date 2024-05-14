using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class DatasourceAuthConfigurationByIdResponse : ISuccess
{
    public Dictionary<string, DatasourceAuthConfigurationResponse> AuthConfigurations { get; }

    public DatasourceAuthConfigurationByIdResponse(
        Dictionary<string, DatasourceAuthConfigurationResponse> authConfigurations)
    {
        AuthConfigurations = authConfigurations;
    }

    public string TrackingId { get; set; }
}

public class DatasourceAuthConfigurationResponse : ISuccess
{
    public AuthenticationSchemes Type { get; }
    public string? AccessTokenUrl { get; }
    public string? RefreshTokenUrl { get; }
    public string? UserEndpointUrl { get; }
    public string? AccessTokenProperty { get; }
    public string? RefreshTokenProperty { get; }
    public string? ExpiryTokenProperty { get; }
    public string? ApiKey { get; }
    public DataSourceTypes? DataType { get; }

    public DatasourceAuthConfigurationResponse(
        AuthenticationSchemes type,
        string? accessTokenUrl,
        string? refreshTokenUrl,
        string? userEndpointUrl,
        string? accessTokenProperty,
        string? refreshTokenProperty,
        string? expiryTokenProperty,
        string? apiKey,
        DataSourceTypes? dataType)
    {
        Type = type;
        AccessTokenUrl = accessTokenUrl;
        RefreshTokenUrl = refreshTokenUrl;
        UserEndpointUrl = userEndpointUrl;
        AccessTokenProperty = accessTokenProperty;
        RefreshTokenProperty = refreshTokenProperty;
        ExpiryTokenProperty = expiryTokenProperty;
        ApiKey = apiKey;
        DataType = dataType;
    }

    public string TrackingId { get; set; }
}