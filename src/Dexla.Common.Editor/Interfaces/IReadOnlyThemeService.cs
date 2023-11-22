using Dexla.Common.Editor.Models;
using Dexla.Common.Repository.Types.Models;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Interfaces;

public interface IReadOnlyThemeService :IDexlaService
{
    Task<IResponse> Get(string projectId, CancellationToken cancellationToken);

    IResponse _getResponse(RepositoryActionResultModel<BrandingModel> actionResult);
}