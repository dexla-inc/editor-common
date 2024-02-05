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
            ? GetResponse(actionResult)
            : GetResponse(BrandingModel.GetDefault("", projectId));
    }

    public static BrandingResponse GetResponse(BrandingModel model)
    {
        return new BrandingResponse(
            model.Id,
            model.Theme,
            model.WebsiteUrl,
            model.Fonts,
            model.Colors,
            model.ResponsiveBreakpoints,
            model.FaviconUrl,
            model.LogoUrl,
            model.Logos,
            model.DefaultFont,
            model.HasCompactButtons,
            model.DefaultRadius,
            model.DefaultSpacing,
            model.InputSize,
            Enum.Parse<LoaderTypes>(model.Loader),
            Enum.Parse<FocusRingTypes>(model.FocusRing),
            Enum.Parse<CardStyleTypes>(model.CardStyle)
        );
    }

    public IResponse GetResponse(RepositoryActionResultModel<BrandingModel> actionResult)
    {
        return actionResult.ActionResult(actionResult, GetResponse);
    }
}