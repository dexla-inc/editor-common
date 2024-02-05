using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Interfaces;
using Dexla.Common.Editor.Models;
using Dexla.Common.Editor.Responses;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Repository.Types.Models;
using Dexla.Common.Types;
using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Implementations;

public class ReadOnlyDeploymentService(
    IEditorContext context,
    IModelMapper<DeploymentModel> modelMapper,
    ILoggerService<ReadOnlyDeploymentService> logger)
    : IReadOnlyDeploymentService
{
    public async Task<IResponse> List(string projectId)
    {
        SortConfiguration sortConfig = new("version", SortDirections.Descending);

        (IReadOnlyList<Deployment> entities, int _) =
            await context.GetEntities<Deployment>(new FilterConfiguration(projectId), 0, 100, sortConfig);

        return new PagedResponse<DeploymentResponse>
        {
            Results = entities.Select(_responseToEntity()).OrderByDescending(e => e.Version).ToList()
        };
    }

    public async Task<DeploymentResponse> GetMostRecent(string projectId, string environment, bool includePages)
    {
        IEnumerable<Deployment> entities = await context.GetMostRecentEnvironmentDeployments(projectId);
        IEnumerable<DeploymentModel> list = modelMapper.Create(entities);

        DeploymentResponse? result = list
            .Where(m => m.Environment == Enum.Parse<EnvironmentTypes>(environment))
            .Select(_responseToModel(includePages))
            .FirstOrDefault();

        return result ?? new DeploymentResponse();
    }

    public async Task<DeploymentPageResponse> GetMostRecentByPage(
        string projectId,
        string environment,
        string page)
    {
        try
        {
            IResponse result = await GetMostRecent(projectId, environment, true);

            if (result is not DeploymentResponse response)
                return DeploymentPageResponse.Empty;

            DeploymentPageResponse? deploymentPage = response.Pages
                .FirstOrDefault(d => page == d.Id || page == d.Slug);

            return deploymentPage ?? DeploymentPageResponse.Empty;
        }
        catch (Exception e)
        {
            logger.LogWarning(e.Message);
            return DeploymentPageResponse.Empty;
        }
    }

    public Func<Deployment, DeploymentResponse> _responseToEntity()
    {
        return m => new DeploymentResponse(
            m.Id,
            m.ProjectId,
            m.Environment,
            m.CommitMessage,
            m.TaskId,
            m.Version,
            _pagesToDeploymentPages(m.Pages, false));
    }
    
    public DeploymentResponse _getResponse(Deployment entity, bool includePages)
    {
        return new DeploymentResponse(
            entity.Id,
            entity.ProjectId,
            entity.Environment,
            entity.CommitMessage,
            entity.TaskId,
            entity.Version,
            _pagesToDeploymentPages(entity.Pages, includePages));
    }

    public Func<DeploymentModel, DeploymentResponse> _responseToModel(bool includePages)
    {
        return m => new DeploymentResponse(
            m.Id!,
            m.ProjectId,
            m.Environment,
            m.CommitMessage,
            m.TaskId,
            m.Version,
            _pagesToDeploymentPages(m.Pages, includePages));
    }

    public List<DeploymentPageResponse> _pagesToDeploymentPages(
        IEnumerable<DeploymentPage> pages,
        bool includePagesState)
    {
        return pages.Select(d => _pageToDeploymentPage(d, includePagesState)).ToList();
    }
    
    public DeploymentPageResponse _pageToDeploymentPage(DeploymentPage page, bool includePages)
    {
        return new DeploymentPageResponse(page.Id, page.Title, page.Slug, page.AuthenticatedOnly, page.AuthenticatedUserRole)
        {
            PageState = includePages ? page.PageState : null,
        };
    }
}