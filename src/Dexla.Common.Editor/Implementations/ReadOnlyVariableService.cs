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

public class ReadOnlyVariableService : DexlaService<Variable, VariableModel>, IReadOnlyVariableService
{
    private readonly IContext _context;

    public ReadOnlyVariableService(
        IRepository<Variable, VariableModel> repository,
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
            filterConfiguration.Append(nameof(Variable.Name), search, SearchTypes.PARTIAL);

        filterConfiguration.AppendArray(
            new Dictionary<string, object>
            {
                { nameof(Variable.PageId), pageId },
                { nameof(Variable.IsGlobal), true }
            },
            SearchTypes.OR);

        (IReadOnlyList<Variable> entities, int totalRecords) =
            await _context.GetEntities<Variable>(filterConfiguration);

        return new PagedResponse<VariableResponse>
        {
            Results = entities.Select(_getResponse()).ToList(),
            Paging = new PagingModel(totalRecords, entities.Count, offset, limit)
        };
    }

    public Func<Variable, VariableResponse> _getResponse()
    {
        return m => new VariableResponse(
            m.Id,
            m.Name,
            m.Type,
            m.DefaultValue,
            m.Value,
            m.IsGlobal,
            m.PageId);
    }

    public VariableResponse _getResponse(VariableModel model)
    {
        return new VariableResponse(
            model.Id!,
            model.Name,
            Enum.Parse<FrontEndTypes>(model.Type),
            model.DefaultValue,
            model.Value,
            model.IsGlobal,
            model.PageId);
    }
}