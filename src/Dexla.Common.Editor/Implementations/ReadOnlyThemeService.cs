using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Interfaces;
using Dexla.Common.Editor.Models;
using Dexla.Common.Editor.Responses;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Repository.Types.Models;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Implementations;

public class ReadOnlyThemeService : IReadOnlyThemeService
{
    private readonly IRepository<Theme, ThemeModel> _repository;

    public ReadOnlyThemeService(IRepository<Theme, ThemeModel> repository)
    {
        _repository = repository;
    }

    public async Task<IResponse> Get(
        string projectId,
        CancellationToken cancellationToken)
    {
        RepositoryActionResultModel<ThemeModel> actionResult = await _repository.Get(projectId);

        return actionResult.CurrentVersion != null
            ? _getResponse(actionResult)
            : ThemeResponse.GetDefault();
    }

    public IResponse _getResponse(RepositoryActionResultModel<ThemeModel> actionResult)
    {
        return actionResult.ActionResult<ThemeResponse>(
            actionResult,
            m => new ThemeResponse(
                m.Id,
                m.Fonts,
                m.Colors,
                m.ResponsiveBreakpoints,
                m.FaviconUrl,
                m.LogoUrl,
                m.Logos,
                m.DefaultBorderRadius,
                m.DefaultSpacing,
                m.WebsiteUrl));
    }
}