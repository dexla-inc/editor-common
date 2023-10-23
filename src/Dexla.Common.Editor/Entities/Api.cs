using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Types.Enums;
using Dexla.Common.Utilities;

namespace Dexla.Common.Editor.Entities;

public class Api : IEntity
{
    public string Id { get; set; } = UtilityExtensions.GetId();
    public EntityStatus EntityStatus { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public AuthenticationSchemes AuthenticationScheme { get; set; }
    public EnvironmentTypes? Environment { get; set; } 
    public string BaseUrl { get; set; } = string.Empty;
    public string? SwaggerUrl { get; set; }
    public bool IsTested { get; set; }
    public long Updated { get; set; }
    public DataSourceTypes Type { get; set; }
    public string? AuthValue { get; set;  }
}