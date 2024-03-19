namespace Dexla.Common.Editor.Entities;

public class DeploymentWithPages : Deployment
{
    public virtual List<DeploymentPage>? Pages { get; set; }
}