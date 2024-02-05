using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Interfaces;
using Dexla.Common.Editor.Models;
using Dexla.Common.Editor.Responses;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Repository.Types.Models;
using Dexla.Common.Types;
using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Interfaces;
using Nager.PublicSuffix;

namespace Dexla.Common.Editor.Implementations;

public class ReadOnlyProjectService(
    IRepository<Project, ProjectModel> repository,
    IContext context,
    ILoggerService<ReadOnlyProjectService> loggerService)
    : DexlaService<Project, ProjectModel>(repository), IReadOnlyProjectService
{
    public async Task<IResponse> Get(string id, bool includeBranding)
    {
        if (includeBranding)
        {
            FilterConfiguration filterConfig = new();
            filterConfig.Append(nameof(Project.Id), id, SearchTypes.EXACT);
            return await _projectWithBranding(filterConfig);
        }

        RepositoryActionResultModel<ProjectModel> actionResult = await Repository.Get(id);

        return _getResponse(actionResult);
    }

    public async Task<IResponse> GetByDomain(string domain, bool includeBranding)
    {
        try
        {
            FilterConfiguration filterConfig = new();
            DomainParser domainParser = new(new WebTldRuleProvider());
            DomainInfo? domainInfo = domainParser.Parse(domain);
            string? projectId = string.Empty;

            if (domainInfo.RegistrableDomain == "dexla.io")
            {
                projectId = domainInfo.SubDomain ?? string.Empty;
                filterConfig.Append(nameof(Project.Id), projectId, SearchTypes.EXACT);
            }
            else if (domainInfo.TLD == "localhost")
            {
                projectId = domainInfo.Domain ?? string.Empty;
                filterConfig.Append(nameof(Project.Id), projectId, SearchTypes.EXACT);
            }
            else
            {
                filterConfig.Append(nameof(Project.SubDomain), domainInfo.SubDomain, SearchTypes.EXACT);
                filterConfig.Append(nameof(Project.Domain), domainInfo.RegistrableDomain, SearchTypes.EXACT);
            }

            if (includeBranding)
                return await _projectWithBranding(filterConfig);

            Project? project = await context.GetByFields<Project>(filterConfig);

            if (project is null)
                loggerService.LogWarning("Project not found for domain {0}", domain);

            return project is null
                ? new ProjectResponse()
                : _getResponse(project);
        }
        catch (ParseException e)
        {
            loggerService.LogWarning("Failed to parse domain {0}", domain);
            return new ProjectResponse();
        }
    }

    public IResponse _getResponse(RepositoryActionResultModel<ProjectModel> actionResult, string? homePageId = null)
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
                m.Screenshots,
                m.CustomCode,
                m.RedirectSlug,
                m.FaviconUrl
            )
            {
                HomePageId = homePageId
            });
    }

    public ProjectResponse _getResponse(Project entity, bool? isOwner = false)
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
            isOwner ?? entity.IsOwner,
            entity.Domain,
            entity.SubDomain,
            entity.Created,
            entity.Screenshots,
            entity.CustomCode,
            entity.RedirectSlug,
            entity.FaviconUrl
        );
    }

    private async Task<IResponse> _projectWithBranding(FilterConfiguration filterConfig)
    {
        try
        {
            ProjectWithBrandingResponse projectWithBranding =
                await context.JoinCollections<Project, Branding, ProjectWithBrandingResponse>(
                    filterConfig,
                    nameof(Project.Id),
                    nameof(Branding.ProjectId));
            
            if (projectWithBranding.Branding is null)
                projectWithBranding.SetBranding();

            if (string.IsNullOrEmpty(projectWithBranding.Id))
                return new ErrorResponse("Can't find project", Json.Serialize(filterConfig.Filters.ToArray()));

            return projectWithBranding;
        }
        catch (Exception e)
        {
            //await loggerService.LogError("Failed to get project with branding {0}", e.Message);
            return new ErrorResponse("Failed to get project with branding " + e.Message);
        }
    }
}