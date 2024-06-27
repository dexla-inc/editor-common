using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Types.Enums;
using Dexla.Common.Utilities;

namespace Dexla.Common.Editor.Models;

public class ApiModel : IModelWithProjectId
{
    public string? Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public BasicAuditInformation? AuditInformation { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string CompanyId { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string AuthenticationScheme { get; set; } = string.Empty;
    public string? Environment { get; set; }
    public string BaseUrl { get; set; } = string.Empty;
    public string? SwaggerUrl { get; set; }
    public bool IsTested { get; set; }
    public long Updated { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? AuthValue { get; set; }
    public string? ApiKey { get; set; }
    
    public void SetUserId(string value)
    {
        UserId = value;
    }

    public void SetProjectId(string projectId)
    {
        ProjectId = projectId;
    }
    
    public void SetCompanyId(string value)
    {
        CompanyId = value;
    }
    
    public void SetUpdated()
    {
        Updated = DateTimeExtensions.GetTimestamp();
    }

    public void SetDataSource(string value)
    {
        Type = value;
    }

    public void SetNoAuthenticationScheme()
    {
        AuthenticationScheme = AuthenticationSchemes.NONE.ToString();
    }

    public void SetBaseUrl(string baseUrl)
    {
        BaseUrl = baseUrl;
    }

    public void SetName(string name)
    {
        Name = name;
    }
}