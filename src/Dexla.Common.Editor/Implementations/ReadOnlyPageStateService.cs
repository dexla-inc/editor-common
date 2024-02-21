using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Interfaces;
using Dexla.Common.Editor.Models;
using Dexla.Common.Editor.Responses;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Repository.Types.Models;
using Dexla.Common.Types;
using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Implementations;

public class ReadOnlyPageStateService : DexlaService<PageState, PageStateModel>, IReadOnlyPageStateService
{
    private readonly IContext _context;

    public ReadOnlyPageStateService(
        IRepository<PageState, PageStateModel> repository,
        IContext context) : base(repository)
    {
        _context = context;
    }

    public async Task<IResponse> Get(string projectId, string pageId)
    {
        return await Get(pageId, _getResponse);
    }

    public async Task<IResponse> List(string projectId, string pageId, int offset, int limit)
    {
        FilterConfiguration filterConfiguration = new(projectId);
        filterConfiguration.Append(nameof(PageState.PageId), pageId, SearchTypes.EXACT);

        (IReadOnlyList<PageState> entities, int totalRecords) = await _context.GetEntities<PageState>(
            filterConfiguration,
            offset,
            limit);

        return new PagedResponse<PageStateResponse>
        {
            Results = entities.Select(_getResponse()).ToList(),
            Paging = new PagingModel(totalRecords, entities.Count, offset, limit)
        };
    }
    
    public PageStateResponse _getResponse(PageStateModel model)
    {
        return new PageStateResponse(model.Id, model.State, model.Created);
    }

    public Func<PageState, PageStateResponse> _getResponse()
    {
        return m => new PageStateResponse(
            m.Id,
            m.State,
            m.Created);
    }
}