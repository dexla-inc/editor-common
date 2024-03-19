namespace Dexla.Common.Editor.Entities;

public class DeploymentWithPages : Deployment
{
    public List<DeploymentPage> Pages { get; set; } = [];
}