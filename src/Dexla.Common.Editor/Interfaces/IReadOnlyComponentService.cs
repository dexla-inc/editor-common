using System;
using System.Threading.Tasks;
using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Models;
using Dexla.Common.Editor.Responses;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Interfaces;

public interface IReadOnlyComponentService : IDexlaService
{
    Task<IResponse> List(string projectId, string companyId, string scopes, string? search);
    ComponentResponse _getResponse(ComponentModel model);
    Func<Component, ComponentResponse> _getResponse();
}