using Dexla.Common.Repository.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public interface IAuditInformation
{
    public AuditUserDto UpdatedBy { get; }
}

public class AuditUserDto
{
    public AuditUserDto(BasicAuditInformation? auditInformation)
    {
        Name = auditInformation?.UpdatedBy?.Name ?? auditInformation?.CreatedBy?.Name ?? string.Empty;
        Date = auditInformation?.UpdatedBy?.Date ?? auditInformation?.CreatedBy?.Date ?? 0;
    }

    public string Name { get; }
    public long Date { get; }
}