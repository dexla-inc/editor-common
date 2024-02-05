using Dexla.Common.Editor.Implementations;
using Dexla.Common.Editor.Models;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class ProjectWithBrandingResponse : ProjectResponse
{
    public BrandingResponse? Branding { get; set; }

    public void SetBranding()
    {
        BrandingModel defaultModel = BrandingModel.GetDefault(string.Empty, string.Empty);
        
        Branding = ReadOnlyBrandingService.GetResponse(defaultModel);
    }
}