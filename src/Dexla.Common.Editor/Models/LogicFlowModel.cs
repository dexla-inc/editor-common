using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Utilities;

namespace Dexla.Common.Editor.Models;

public class LogicFlowModel : IModelWithProjectId
{
    internal LogicFlowModel()
    {
    }

    public string? Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public BasicAuditInformation? AuditInformation { get; set; }

    public void SetCompanyId(string value)
    {
        CompanyId = value;
    }

    public string UserId { get; set; }
    public string CompanyId { get; set; } = string.Empty;
    public string ProjectId { get; set; }
    public string Name { get; set; }
    public string Data { get; set; }
    public long CreatedAt { get; set; }
    public long UpdatedAt { get; set; }

    public void SetUserId(string value)
    {
        UserId = value;
    }

    public void SetProjectId(string value)
    {
        ProjectId = value;
    }

    public void SetCreatedAt()
    {
        long timestamp = DateTimeExtensions.GetTimestamp();

        CreatedAt = timestamp;
        UpdatedAt = timestamp;
    }

    public void SetUpdatedAt()
    {
        long timestamp = DateTimeExtensions.GetTimestamp();
        UpdatedAt = timestamp;
    }

    public void SetExistingValues(string userId, string projectId, long createdAt)
    {
        UserId = userId;
        ProjectId = projectId;
        CreatedAt = createdAt;
    }
}