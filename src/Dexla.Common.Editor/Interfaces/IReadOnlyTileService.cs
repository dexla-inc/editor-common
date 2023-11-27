using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Models;
using Dexla.Common.Editor.Responses;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Interfaces;

public interface IReadOnlyTileService : IDexlaService
{
    Task<IResponse> List(string templateId, int offset, int limit);
    Task<IResponse> Get(string templateId, string id);
    TileResponse _getResponse(TileModel model);
    Func<Tile, TileResponse> _getResponse();
}