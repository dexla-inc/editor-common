using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Interfaces;

public interface ILogicFlowService : IDexlaService
{
    Task<IResponse> List(string projectId, string? search, string pageId, int offset, int limit);
    Task<IResponse> Get(string id);
}