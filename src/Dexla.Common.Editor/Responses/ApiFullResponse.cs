using Dexla.Common.Editor.Models;
using Dexla.Common.Repository.Types.Models;
using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Interfaces;
using Dexla.Common.Types.Models;

namespace Dexla.Common.Editor.Responses;

public class ApiFullResponse : ISuccess
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string AuthenticationScheme { get; }
    public EnvironmentTypes? Environment { get; }
    public string BaseUrl { get; }
    public string? SwaggerUrl { get; }
    public long Updated { get; }
    public DataSourceTypes Type { get; }
    public string? AuthValue { get; }
    public string? ApiKey { get; }
    public bool IsTested { get; }
    public IEnumerable<DataSourceEndpoint>? ChangedEndpoints { get; }
    public IEnumerable<DataSourceEndpoint>? DeletedEndpoints { get; }

    public ApiFullResponse()
    {
    }

    public ApiFullResponse(
        string id,
        string name,
        string authenticationScheme,
        EnvironmentTypes? environment,
        string baseUrl,
        string? swaggerUrl,
        long updated,
        DataSourceTypes type,
        string? authValue,
        string? apiKey,
        bool isTested,
        IEnumerable<DataSourceEndpoint>? changedEndpoints = null,
        IEnumerable<DataSourceEndpoint>? deletedEndpoints = null)
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
        ChangedEndpoints = changedEndpoints ?? new List<DataSourceEndpoint>();
        DeletedEndpoints = deletedEndpoints ?? new List<DataSourceEndpoint>();
    }

    public static ApiFullResponse ModelToResponse(
        ApiModel swaggerApiModel,
        IEnumerable<DataSourceEndpoint>? changedEndpoints = null,
        IEnumerable<DataSourceEndpoint>? deletedEndpoints = null)
    {
        return new ApiFullResponse(
            swaggerApiModel.Id!,
            swaggerApiModel.Name,
            swaggerApiModel.AuthenticationScheme,
            swaggerApiModel.Environment != null
                ? Enum.Parse<EnvironmentTypes>(swaggerApiModel.Environment)
                : EnvironmentTypes.Editor,
            swaggerApiModel.BaseUrl,
            swaggerApiModel.SwaggerUrl,
            swaggerApiModel.Updated,
            !string.IsNullOrWhiteSpace(swaggerApiModel.Type) ? Enum.Parse<DataSourceTypes>(swaggerApiModel.Type) : DataSourceTypes.API,
            swaggerApiModel.AuthValue,
            swaggerApiModel.ApiKey,
            swaggerApiModel.IsTested,
            changedEndpoints,
            deletedEndpoints);
    }

    public static IResponse ModelToResponse(
        RepositoryActionResultModel<ApiModel> actionResult,
        IEnumerable<DataSourceEndpoint>? changedEndpoints = null,
        IEnumerable<DataSourceEndpoint>? deletedEndpoints = null)
    {
        return actionResult.ActionResult(actionResult,
            model => ModelToResponse(model, changedEndpoints, deletedEndpoints));
    }

    public string TrackingId { get; set; }
}