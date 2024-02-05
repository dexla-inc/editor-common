using Dexla.Common.Editor.Models;
using Dexla.Common.Repository.Types.Models;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Interfaces;

public interface IReadOnlyBrandingService : IDexlaService
{
    Task<IResponse> Get(string projectId, CancellationToken cancellationToken);

    IResponse GetResponse(RepositoryActionResultModel<BrandingModel> actionResult);
}