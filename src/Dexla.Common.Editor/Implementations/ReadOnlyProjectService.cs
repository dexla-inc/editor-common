using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Interfaces;
using Dexla.Common.Editor.Models;
using Dexla.Common.Editor.Responses;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Repository.Types.Models;
using Dexla.Common.Types;
using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Interfaces;
using Microsoft.Extensions.Logging;
using Nager.PublicSuffix;

namespace Dexla.Common.Editor.Implementations;

public class ReadOnlyProjectService : DexlaService<Project, ProjectModel>, IReadOnlyProjectService
{
    private readonly IContext _context;
    private readonly ILogger<ReadOnlyProjectService> _loggerService;

    public ReadOnlyProjectService(
        IRepository<Project, ProjectModel> repository,
        IContext context,
        ILogger<ReadOnlyProjectService> loggerService) : base(repository)
    {
        _context = context;
        _loggerService = loggerService;
    }
    
    public async Task<IResponse> Get(string id)
    {
        RepositoryActionResultModel<ProjectModel> actionResult = await Repository.Get(id);

        return _getResponse(actionResult);
    }

    public async Task<IResponse> GetProjectId(string domain)
    {
        try
        {
            FilterConfiguration filterConfig = new();

            DomainParser domainParser = new(new WebTldRuleProvider());
            DomainInfo? domainInfo = domainParser.Parse(domain);

            string subDomain = domainInfo.SubDomain ?? string.Empty;

            filterConfig.Append(nameof(Project.SubDomain), subDomain, SearchTypes.EXACT);
            filterConfig.Append(nameof(Project.Domain), domainInfo.RegistrableDomain, SearchTypes.EXACT);

            Project? project = await _context.GetByFields<Project>(filterConfig);

            return project is null 
                ? new ProjectResponse() 
                : _getResponse(project);
        }
        catch (ParseException e)
        {
            _loggerService.LogWarning("Failed to parse domain {0}", domain);
            return new ProjectResponse();
        }
    }
    
    public IResponse _getResponse(RepositoryActionResultModel<ProjectModel> actionResult)
    {
        return actionResult.ActionResult<ProjectResponse>(
            actionResult,
            m => new ProjectResponse(
                m.Id!,
                m.CompanyId,
                m.Name,
                m.FriendlyName,
                Regions.ParseRegion(m.Region),
                Enum.Parse<ProjectTypes>(m.Type),
                m.Industry,
                m.Description,
                m.SimilarCompany,
                m.IsOwner,
                m.Domain,
                m.SubDomain,
                m.Created,
                m.Screenshots
            ));
    }

    public IResponse _getResponse(Project entity)
    {
        return new ProjectResponse(
            entity.Id,
            entity.CompanyId,
            entity.Name,
            entity.FriendlyName,
            Regions.ParseRegion(entity.Region) ?? new Region(),
            entity.Type,
            entity.Industry,
            entity.Description,
            entity.SimilarCompany,
            entity.IsOwner,
            entity.Domain,
            entity.SubDomain,
            entity.Created,
            entity.Screenshots
        );
    }
}