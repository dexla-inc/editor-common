using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Models;
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

    Func<Deployment, DeploymentResponse> _responseToEntity();
    DeploymentResponse _getResponse(Deployment entity, bool includePages);
    Func<DeploymentModel, DeploymentResponse> _responseToModel(bool includePages);
    List<DeploymentPageResponse> _pagesToDeploymentPages(
        IEnumerable<DeploymentPage> pages,
        bool includePagesState);
    DeploymentPageResponse _pageToDeploymentPage(DeploymentPage page, bool includePages);
}