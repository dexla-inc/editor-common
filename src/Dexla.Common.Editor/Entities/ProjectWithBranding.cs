using Dexla.Common.Editor.Models;

namespace Dexla.Common.Editor.Entities;

public class ProjectWithBranding : Project
{
    public BrandingModel? Branding { get; set; }

    public void SetBranding()
    {
        Branding = BrandingModel.GetDefault(string.Empty, string.Empty);
    }
}