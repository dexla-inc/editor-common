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

public class ReadOnlyDeploymentService : IReadOnlyDeploymentService
{
    private readonly IEditorContext _context;
    private readonly IModelMapper<DeploymentModel> _modelMapper;
    private readonly ILoggerService<ReadOnlyDeploymentService> _logger;

    public ReadOnlyDeploymentService(
        IEditorContext context,
        IModelMapper<DeploymentModel> modelMapper,
        ILoggerService<ReadOnlyDeploymentService> logger)
    {
        _context = context;
        _modelMapper = modelMapper;
        _logger = logger;
    }

    public async Task<IResponse> List(string projectId)
    {
        SortConfiguration sortConfig = new("version", SortDirections.Descending);

        (IReadOnlyList<Deployment> entities, int _) =
            await _context.GetEntities<Deployment>(new FilterConfiguration(projectId), 0, 100, sortConfig);

        return new PagedResponse<DeploymentResponse>
        {
            Results = entities.Select(_responseToEntity()).OrderByDescending(e => e.Version).ToList()
        };
    }

    public async Task<DeploymentResponse> GetMostRecent(string projectId, string environment, bool includePages)
    {
        IEnumerable<Deployment> entities = await _context.GetMostRecentEnvironmentDeployments(projectId);
        IEnumerable<DeploymentModel> list = _modelMapper.Create(entities);

        DeploymentResponse? result = list
            .Where(m => m.Environment == Enum.Parse<EnvironmentTypes>(environment))
            .Select(_responseToModel(includePages))
            .FirstOrDefault();

        return result ?? new DeploymentResponse();
    }

    public async Task<DeploymentPageResponse> GetMostRecentByPage(
        string projectId,
        string environment,
        string? pageId,
        string? slug)
    {
        try
        {
            IResponse result = await GetMostRecent(projectId, environment, true);

            if (result is not DeploymentResponse response)
                return DeploymentPageResponse.Empty;

            DeploymentPageResponse? page = response.Pages
                .FirstOrDefault(d => (pageId != null && d.Id == pageId) || (slug != null && d.Slug == slug));

            return page ?? DeploymentPageResponse.Empty;
        }
        catch (Exception e)
        {
            _logger.LogWarning(e.Message);
            return DeploymentPageResponse.Empty;
        }
    }

    private static Func<Deployment, DeploymentResponse> _responseToEntity()
    {
        return m => new DeploymentResponse(
            m.Id!,
            m.ProjectId,
            m.Environment,
            m.CommitMessage,
            m.TaskId,
            m.Version,
            m.Pages
                .Select(p => new DeploymentPageResponse(p.Id, p.Title, p.Slug))
                .ToList());
    }

    private static Func<DeploymentModel, DeploymentResponse> _responseToModel(bool includePagesState)
    {
        return m => new DeploymentResponse(
            m.Id!,
            m.ProjectId,
            m.Environment,
            m.CommitMessage,
            m.TaskId,
            m.Version,
            _pagesToDeploymentPages(m.Pages, includePagesState));
    }

    private static List<DeploymentPageResponse> _pagesToDeploymentPages(IEnumerable<DeploymentPage> pages, bool includePagesState)
    {
        return pages.Select(d => _pageToDeploymentPage(d, includePagesState)).ToList();
    }
    
    private static DeploymentPageResponse _pageToDeploymentPage(DeploymentPage d, bool includePages)
    {
        return new DeploymentPageResponse(d.Id, d.Title, d.Slug)
        {
            PageState = includePages ? d.PageState : null,
        };
    }
}