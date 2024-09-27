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
        string? faviconUrl,
        BrandingModel? branding,
        RedirectsDto redirects,
        Dictionary<string, object> metadata,
        List<AppDto>? apps,
        Dictionary<string, LiveUrlDto> liveUrls,
        bool isOnboarding)
        : base(id, companyId, name, friendlyName, region, type, industry, description, similarCompany, isOwner, domain,
            subDomain, created, screenshots, customCode, faviconUrl, redirects, metadata, apps, liveUrls, isOnboarding)
    {
        Branding = ReadOnlyBrandingService.GetResponse(branding ?? BrandingModel.GetDefault(string.Empty, string.Empty));
    }
}