﻿using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Models;
using Dexla.Common.Repository.Types.Models;
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
    public UserRoles AccessLevel { get; }
    public bool IsOwner { get; }
    public string Domain { get; }
    public string SubDomain { get; }
    public long Created { get; }
    public string[] Screenshots { get; }
    public string? HomePageId { get; set; }
    public string? CustomCode { get; set; }
    public string? RedirectSlug { get; }
    public string? FaviconUrl { get; }

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
        string? redirectSlug,
        string? faviconUrl)
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
        RedirectSlug = redirectSlug;
        FaviconUrl = faviconUrl;
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
                m.RedirectSlug,
                m.FaviconUrl
            )
            {
                HomePageId = homePageId
            });
    }

    public static ProjectResponse ModelToResponse(Project entity, bool? isOwner = false)
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

    public static ProjectWithBrandingResponse ModelToResponse(ProjectWithBranding entity, string userId)
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
            entity.RedirectSlug,
            entity.FaviconUrl,
            entity.Branding
        );
    }
    
    public static Func<Project, ProjectResponse> EntityToResponse()
    {
        return entity => new ProjectResponse(
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
            entity.Screenshots,
            entity.CustomCode,
            entity.RedirectSlug,
            entity.FaviconUrl);
    }
}