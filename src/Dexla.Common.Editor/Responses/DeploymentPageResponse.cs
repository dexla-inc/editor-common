using Dexla.Common.Editor.Entities;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class DeploymentPageResponse : DeploymentPage, ISuccess
{
    public string TrackingId { get; set; }
}