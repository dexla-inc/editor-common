using System;
using System.Threading.Tasks;
using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Models;
using Dexla.Common.Editor.Responses;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Interfaces;

public interface IReadOnlyVariableService : IDexlaService
{
    Task<IResponse> List(string projectId, string? search, int offset, int limit);
    Task<IResponse> Get(string id);
    VariableResponse _getResponse(VariableModel model);
    Func<Variable, VariableResponse> _getResponse();
}