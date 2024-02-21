using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Interfaces;

public interface IReadOnlyPageStateService : IDexlaService
{
    Task<IResponse> Get(string projectId, string pageId);
    Task<IResponse> List(string projectId, string pageId, int offset, int limit);
}