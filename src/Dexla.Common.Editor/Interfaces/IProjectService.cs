using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Interfaces;

public interface IProjectService : IDexlaService
{
    Task<IResponse> Get(string id);
    Task<IResponse> GetProjectId(string domain);
}