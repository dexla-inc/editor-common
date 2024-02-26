using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Interfaces;
using Dexla.Common.Editor.Models;
using Dexla.Common.Editor.Responses;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Repository.Types.Models;
using Dexla.Common.Types;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Implementations;

public class ReadOnlyTileService(
        IRepository<Tile, TileModel> repository,
        IContext context)
    : DexlaService<Tile, TileModel>(repository), IReadOnlyTileService
{
    public async Task<IResponse> Get(string templateId, string id)
    {
        return await Get(id, _getResponse);
    }

    public async Task<IResponse> List(
        string templateId,
        int offset,
        int limit)
    {
        FilterConfiguration filterConfiguration = new();

        (IReadOnlyList<Tile> entities, int totalRecords) =
            await context.GetEntities<Tile>(filterConfiguration);

        return new PagedResponse<TileResponse>
        {
            Results = entities.Select(_getResponse()).ToList(),
            Paging = new PagingModel(totalRecords, entities.Count, offset, limit)
        };
    }

    public Func<Tile, TileResponse> _getResponse()
    {
        return entity => new TileResponse(
            entity.Id,
            entity.Name,
            entity.State,
            entity.Prompt,
            entity.TemplateId,
            entity.UpdatedAt,
            entity.CreatedAt);
    }

    public TileResponse _getResponse(TileModel model)
    {
        return new TileResponse(
            model.Id!,
            model.Name,
            model.State,
            model.Prompt,
            model.TemplateId,
            model.UpdatedAt,
            model.CreatedAt);
    }
}