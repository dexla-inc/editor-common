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

public class LogicFlowService : DexlaService<LogicFlow, LogicFlowModel>, ILogicFlowService
{
    private readonly IContext _context;

    public LogicFlowService(
        IRepository<LogicFlow, LogicFlowModel> repository,
        IContext context) : base(repository)
    {
        _context = context;
    }

    public async Task<IResponse> Get(string id)
    {
        return await Get(id, _getResponse);
    }

    public async Task<IResponse> List(
        string projectId,
        string? search,
        string pageId,
        int offset,
        int limit)
    {
        FilterConfiguration filterConfiguration = new(projectId);

        if (search != null)
            filterConfiguration.Append(nameof(LogicFlow.Name), search, SearchTypes.PARTIAL);

        filterConfiguration.AppendArray(
            new Dictionary<string, object>
            {
                { nameof(LogicFlow.PageId), pageId },
                { nameof(LogicFlow.IsGlobal), true }
            },
            SearchTypes.OR);

        (IReadOnlyList<LogicFlow> entities, int totalRecords) =
            await _context.GetEntities<LogicFlow>(filterConfiguration);

        return new PagedResponse<LogicFlowResponse>
        {
            Results = entities.Select(_getResponse()).ToList(),
            Paging = new PagingModel(totalRecords, entities.Count, offset, limit)
        };
    }

    private static Func<LogicFlow, LogicFlowResponse> _getResponse()
    {
        return entity => new LogicFlowResponse(
            entity.Id,
            entity.Name,
            entity.Data,
            entity.PageId,
            entity.IsGlobal,
            entity.CreatedAt,
            entity.UpdatedAt);
    }

    private static LogicFlowResponse _getResponse(LogicFlowModel model)
    {
        return new LogicFlowResponse(
            model.Id!,
            model.Name,
            model.Data,
            model.PageId,
            model.IsGlobal,
            model.CreatedAt,
            model.UpdatedAt);
    }
}