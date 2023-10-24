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

    public async Task<DeploymentResponse> GetMostRecent(string projectId, string environment)
    {
        IEnumerable<Deployment> entities = await _context.GetMostRecentEnvironmentDeployments(projectId);
        IEnumerable<DeploymentModel> list = _modelMapper.Create(entities);

        DeploymentResponse? result = list
            .Where(m => m.Environment == Enum.Parse<EnvironmentTypes>(environment))
            .OrderByDescending(m => m.Version)
            .Select(_responseToModel())
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
            IResponse result = await GetMostRecent(projectId, environment);

            if (result is not DeploymentResponse response)
                return new DeploymentPageResponse();

            DeploymentPageResponse? page = response.Pages
                .Where(d =>
                    (pageId != null && d.Id == pageId) ||
                    (slug != null && d.Slug == slug))
                .Select(d => new DeploymentPageResponse
                {
                    Id = d.Id,
                    PageState = d.PageState,
                    Slug = d.Slug,
                    Title = d.Title,
                })
                .FirstOrDefault();

            return page ?? new DeploymentPageResponse();
        }
        catch (Exception e)
        {
            _logger.LogWarning(e.Message);
            return new DeploymentPageResponse();
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
            m.Pages.ToList()
        );
    }

    private static Func<DeploymentModel, DeploymentResponse> _responseToModel()
    {
        return m => new DeploymentResponse(
            m.Id!,
            m.ProjectId,
            m.Environment,
            m.CommitMessage,
            m.TaskId,
            m.Version,
            m.Pages.ToList()
                .ToList());
    }
}