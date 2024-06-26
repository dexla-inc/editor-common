using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Models;
using Dexla.Common.Repository.Types.Models;
using Dexla.Common.Types;
using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class ProjectResponse : ISuccess
{
    public string Id { get; }
    public string CompanyId { get; }
    public string Name { get; }
    public string FriendlyName { get; }
    public Region Region { get; }
    public ProjectTypes Type { get; }
    public string Industry { get; }
    public string Description { get; }
    public string? SimilarCompany { get; }
    public bool IsOwner { get; }
    public string Domain { get; }
    public string SubDomain { get; }
    public long Created { get; }
    public string[] Screenshots { get; }
    public string? HomePageId { get; set; }
    public string? CustomCode { get; set; }
    public string? FaviconUrl { get; }
    public RedirectsDto? Redirects { get; }
    public Dictionary<string, object> Metadata { get; }
    public List<AppDto>? Apps { get; }
    public Dictionary<string, LiveUrlDto> LiveUrls { get; set; } = [];

    public ProjectResponse(
        string id,
        string companyId,
        string name,
        string friendlyName,
        Region region,
        ProjectTypes type,
        string industry,
        string description,
        string? similarCompany,
        bool isOwner,
        string domain,
        string subDomain,
        long created,
        string[] screenshots,
        string? customCode,
        string? faviconUrl,
        RedirectsDto? redirects,
        Dictionary<string, object> metadata,
        List<AppDto>? apps,
        Dictionary<string, LiveUrlDto> liveUrls)
    {
        Id = id;
        CompanyId = companyId;
        Name = name;
        FriendlyName = friendlyName;
        Region = region;
        Type = type;
        Industry = industry;
        Description = description;
        SimilarCompany = similarCompany;
        IsOwner = isOwner;
        Domain = domain;
        SubDomain = subDomain;
        Created = created;
        Screenshots = screenshots;
        CustomCode = customCode;
        FaviconUrl = faviconUrl;
        Redirects = redirects;
        Metadata = metadata;
        Apps = apps;
        LiveUrls = liveUrls;
    }

    public ProjectResponse()
    {
    }

    public string TrackingId { get; set; }

    public static IResponse ModelToResponse(
        RepositoryActionResultModel<ProjectModel> actionResult,
        string? homePageId = null)
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
                m.FaviconUrl,
                m.Redirects,
                m.Metadata,
                m.Apps,
                m.LiveUrls
            )
            {
                HomePageId = homePageId
            });
    }

    public static Func<Project, ProjectResponse> EntityToResponse()
    {
        return entity => CreateProjectResponse(entity, entity.IsOwner);
    }

    public static ProjectResponse EntityToResponse(Project entity, bool? isOwner = false)
    {
        return CreateProjectResponse(entity, isOwner ?? entity.IsOwner);
    }

    private static ProjectResponse CreateProjectResponse(Project entity, bool isOwner)
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
            isOwner,
            entity.Domain,
            entity.SubDomain,
            entity.Created,
            entity.Screenshots,
            entity.CustomCode,
            entity.FaviconUrl,
            new RedirectsDto
            {
                SignInPageId = entity.Redirects.SignInPageId,
                NotFoundPageId = entity.Redirects.NotFoundPageId
            },
            entity.Metadata,
            entity.Apps?.Select(a => new AppDto
            {
                Id = a.Id,
                Type = a.Type,
                Configuration = Json.Deserialize<object>(a.Configuration)
            }).ToList(),
            entity.LiveUrls.ToDictionary(
                kvp => kvp.Key.ToString(),
                kvp => new LiveUrlDto
                {
                    Domain = kvp.Value.Domain,
                    SubDomain = kvp.Value.SubDomain
                }
            )
        );
    }

    public static ProjectWithBrandingResponse ModelToBrandingResponse(ProjectWithBranding entity, string userId)
    {
        return new ProjectWithBrandingResponse(
            entity.Id,
            entity.CompanyId,
            entity.Name,
            entity.FriendlyName,
            Regions.ParseRegion(entity.Region) ?? new Region(),
            entity.Type,
            entity.Industry,
            entity.Description,
            entity.SimilarCompany,
            userId == entity.UserId,
            entity.Domain,
            entity.SubDomain,
            entity.Created,
            entity.Screenshots,
            entity.CustomCode,
            entity.FaviconUrl,
            string.IsNullOrEmpty(entity.Branding.Id) ? BrandingModel.GetDefault(userId, entity.Id) : entity.Branding,
            new RedirectsDto
            {
                SignInPageId = entity.Redirects.SignInPageId,
                NotFoundPageId = entity.Redirects.NotFoundPageId
            },
            entity.Metadata,
            entity.Apps?.Select(a => new AppDto
            {
                Id = a.Id,
                Type = a.Type,
                Configuration = Json.Deserialize<object>(a.Configuration)
            }).ToList(),
            entity.LiveUrls.ToDictionary(
                kvp => kvp.Key.ToString(),
                kvp => new LiveUrlDto
                {
                    Domain = kvp.Value.Domain,
                    SubDomain = kvp.Value.SubDomain
                }
            )
        );
    }

    public static ProjectResponse ModelToResponse(ProjectModel entityProject)
    {
        return new ProjectResponse(
            entityProject.Id!,
            entityProject.CompanyId,
            entityProject.Name,
            entityProject.FriendlyName,
            Regions.ParseRegion(entityProject.Region),
            Enum.Parse<ProjectTypes>(entityProject.Type),
            entityProject.Industry,
            entityProject.Description,
            entityProject.SimilarCompany,
            entityProject.IsOwner,
            entityProject.Domain,
            entityProject.SubDomain,
            entityProject.Created,
            entityProject.Screenshots,
            entityProject.CustomCode,
            entityProject.FaviconUrl,
            entityProject.Redirects,
            entityProject.Metadata,
            entityProject.Apps,
            entityProject.LiveUrls
        );
    }
}