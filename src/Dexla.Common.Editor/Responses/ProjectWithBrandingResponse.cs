﻿using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Models;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class ProjectWithBrandingResponse : Project, ISuccess
{
    public BrandingModel? Branding { get; set; }
    public string TrackingId { get; set; }

    public void SetBranding(string? projectId)
    {
        Branding = BrandingModel.GetDefault(string.Empty, projectId ?? string.Empty);
    }
}