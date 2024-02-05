using Dexla.Common.Editor.Implementations;
using Dexla.Common.Editor.Models;
using Dexla.Common.Types.Enums;

namespace Dexla.Common.Editor.Responses;

public class ProjectWithBrandingResponse : ProjectResponse
{
    public BrandingResponse? Branding { get; set; }

    public ProjectWithBrandingResponse(
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
        string? faviconUrl,
        BrandingModel? branding)
        : base(id, companyId, name, friendlyName, region, type, industry, description, similarCompany, isOwner, domain,
            subDomain, created, screenshots, customCode, redirectSlug, faviconUrl)
    {
        Branding = ReadOnlyBrandingService.GetResponse(branding ?? BrandingModel.GetDefault(string.Empty, string.Empty));
    }
}