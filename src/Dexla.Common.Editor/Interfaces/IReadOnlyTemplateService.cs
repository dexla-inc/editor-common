using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Models;
using Dexla.Common.Editor.Responses;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Interfaces;

public interface IReadOnlyTemplateService : IDexlaService
{
    Task<IResponse> List(int offset, int limit);
    Task<IResponse> Get(string name);
    TemplateResponse _getResponse(TemplateModel model);
    Func<Template, TemplateResponse> _getResponse();
}