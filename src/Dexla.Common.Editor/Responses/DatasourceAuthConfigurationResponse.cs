using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class DatasourceAuthConfigurationResponse : ISuccess
{
    public AuthenticationSchemes Type { get; }
    public string? AccessTokenUrl { get; }
    public string? RefreshTokenUrl { get; }
    public string? UserEndpointUrl { get; }
    public string? AccessTokenProperty { get; }
    public string? RefreshTokenProperty { get; }
    public string? ExpiryTokenProperty { get; }

    public DatasourceAuthConfigurationResponse(
        AuthenticationSchemes type,
        string? accessTokenUrl,
        string? refreshTokenUrl,
        string? userEndpointUrl,
        string? accessTokenProperty,
        string? refreshTokenProperty,
        string? expiryTokenProperty)
    {
        Type = type;
        AccessTokenUrl = accessTokenUrl;
        RefreshTokenUrl = refreshTokenUrl;
        UserEndpointUrl = userEndpointUrl;
        AccessTokenProperty = accessTokenProperty;
        RefreshTokenProperty = refreshTokenProperty;
        ExpiryTokenProperty = expiryTokenProperty;
    }

    public string TrackingId { get; set; }
}