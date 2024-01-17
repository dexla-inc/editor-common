using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Interfaces;

public interface IReadOnlyProjectService : IDexlaService
{
    Task<IResponse> Get(string id);
    Task<IResponse> GetByDomain(string domain);
}