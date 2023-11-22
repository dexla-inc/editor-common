using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Interfaces;
using Dexla.Common.Editor.Models;
using Dexla.Common.Editor.Responses;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Repository.Types.Models;
using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Implementations;

public class ReadOnlyBrandingService(IRepository<Branding, BrandingModel> repository) : IReadOnlyBrandingService
{
    public async Task<IResponse> Get(
        string projectId,
        CancellationToken cancellationToken)
    {
        RepositoryActionResultModel<BrandingModel> actionResult = await repository.Get(projectId);

        return actionResult.CurrentVersion != null
            ? _getResponse(actionResult)
            : BrandingResponse.GetDefault();
    }

    public IResponse _getResponse(RepositoryActionResultModel<BrandingModel> actionResult)
    {
        return actionResult.ActionResult<BrandingResponse>(
            actionResult,
            m => new BrandingResponse(
                m.Id,
                m.Theme,
                m.WebsiteUrl,
                m.Fonts,
                m.Colors,
                m.ResponsiveBreakpoints,
                m.FaviconUrl,
                m.LogoUrl,
                m.Logos,
                m.DefaultFont,
                m.HasCompactButtons,
                m.DefaultRadius,
                m.DefaultSpacing,
                Enum.Parse<LoaderTypes>(m.Loader),
                Enum.Parse<FocusRingTypes>(m.FocusRing),
                Enum.Parse<CardStyleTypes>(m.CardStyle)
            )
        );
    }
}