using System.Threading.Tasks;
using Dexla.Common.Editor.Models;
using Dexla.Common.Repository.Types.Models;
using Dexla.Common.Types;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Interfaces;

public interface IReadOnlyApiService : IDexlaService 
{
    Task<IResponse> Get(string id, bool? withAuth);
    Task<IResponse> List(
        string projectId,
        string? type,
        string? search,
        int offset,
        int limit);
    Task<IResponse> ListEndpoints(
        string projectId,
        string? dataSourceId,
        string? methodType,
        bool authOnly,
        int offset,
        int limit);
    Task<IResponse> GetEndpoint(string id);
    Task<IResponse> GetAuthConfig(string projectId, string id);

    FilterConfiguration _addFilterConfiguration(
        string projectId,
        string? dataSourceId,
        string? relativeUrl = null,
        string? methodType = null);

    IResponse _getEndpointResponse(RepositoryActionResultModel<ApiEndpointModel> actionResult);
}