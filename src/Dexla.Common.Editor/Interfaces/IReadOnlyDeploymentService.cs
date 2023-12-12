using Dexla.Common.Editor.Responses;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Interfaces;

public interface IReadOnlyDeploymentService : IDexlaService
{
    Task<IResponse> List(string projectId);
    Task<DeploymentResponse> GetMostRecent(string projectId, string environment, bool includePages);
    Task<DeploymentPageResponse> GetMostRecentByPage(
        string projectId,
        string environment,
        string page);
}