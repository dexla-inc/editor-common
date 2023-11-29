using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Interfaces;
using Dexla.Common.Editor.Models;
using Dexla.Common.Editor.Responses;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Repository.Types.Models;
using Dexla.Common.Types;
using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Interfaces;
using Dexla.EditorAPI.Core.Entities;

namespace Dexla.Common.Editor.Implementations;

public class ReadOnlyTemplateService(
        IRepository<Template, TemplateModel> repository,
        IContext context)
    : DexlaService<Template, TemplateModel>(repository), IReadOnlyTemplateService
{
    public async Task<IResponse> Get(string name, bool includeTiles = false)
    {
        FilterConfiguration templateFilterConfig = new();
        templateFilterConfig.Append(nameof(Template.Name), name, SearchTypes.EXACT);

        Template? template = await context.GetByFields<Template>(templateFilterConfig);

        if (!includeTiles || template == null)
        {
            return template != null
                ? _getResponse()(template)
                : new ErrorResponse("Template not found with the name " + name + ".");
        }

        FilterConfiguration tileFilterConfig = new();
        tileFilterConfig.Append(nameof(Tile.TemplateId), template.Id, SearchTypes.EXACT);
        (IReadOnlyList<Tile> tiles, int totalRecords) = await context.GetEntities<Tile>(tileFilterConfig);

        return _getResponse(tiles)(template);
    }

    public async Task<IResponse> List(
        int offset,
        int limit)
    {
        FilterConfiguration filterConfiguration = new();

        (IReadOnlyList<Template> entities, int totalRecords) =
            await context.GetEntities<Template>(filterConfiguration);

        return new PagedResponse<TemplateResponse>
        {
            Results = entities.Select(_getResponse()).ToList(),
            Paging = new PagingModel(totalRecords, entities.Count, offset, limit)
        };
    }

    public Func<Template, TemplateResponse> _getResponse(IReadOnlyList<Tile>? tiles = null)
    {
        return entity => new TemplateResponse(
            entity.Id,
            entity.Name,
            entity.State,
            entity.Prompt,
            entity.Type,
            entity.Tags,
            entity.UpdatedAt,
            entity.CreatedAt)
        {
            Tiles = tiles?.Select(tile => new TileResponse(
                    tile.Id,
                    tile.Name,
                    tile.State,
                    tile.Prompt,
                    tile.TemplateId,
                    tile.UpdatedAt,
                    tile.CreatedAt))
                .ToList()
        };
    }

    public TemplateResponse _getResponse(TemplateModel model)
    {
        return new TemplateResponse(
            model.Id!,
            model.Name,
            model.State,
            model.Prompt,
            Enum.Parse<TemplateTypes>(model.Type),
            model.Tags.Select(Enum.Parse<TemplateTags>).ToArray(),
            model.UpdatedAt,
            model.CreatedAt);
    }
}