using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Models;
using Dexla.Common.Editor.Responses;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Interfaces;

public interface IReadOnlyLogicFlowService : IDexlaService
{
    Task<IResponse> List(string projectId, string? search, int offset, int limit);
    Task<IResponse> Get(string id);
    Func<LogicFlow, LogicFlowResponse> _getResponse();
    LogicFlowResponse _getResponse(LogicFlowModel model);
}