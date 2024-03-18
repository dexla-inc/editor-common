using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Models;
using Dexla.Common.Repository.Types.Models;
using Dexla.Common.Types.Interfaces;
using Dexla.Common.Types.Models;

namespace Dexla.Common.Editor.Responses;

public class PageStateHistoryResponse : ISuccess
{
    public string? Id { get; }
    public string State { get; }
    public long Created { get; }

    public PageStateHistoryResponse(string? id, IEnumerable<string> state, long created)
    {
        Id = id;
        State = SetState(state);
        Created = created;
    }

    private static string SetState(IEnumerable<string> state) => string.Join("", state);
    
    public static PageStateHistoryResponse ModelToResponse(PageStateHistoryModel model)
    {
        return new PageStateHistoryResponse(model.Id, model.State, model.Created);
    }
    
    public static CreatedSuccess ModelToResponse(string id)
    {
        return new CreatedSuccess(id);
    }
    
    public static IResponse ModelToResponse(RepositoryActionResultModel<PageStateHistoryModel> actionResult)
    {
        return actionResult.ActionResult(
            actionResult,
            ModelToResponse);
    }

    public static Func<PageStateHistory, PageStateHistoryResponse> ModelToResponse()
    {
        return m => new PageStateHistoryResponse(
            m.Id,
            m.State,
            m.Created);
    }
    
    public static PageStateHistoryResponse EntityToResponse(PageStateHistory entity)
    {
        return new PageStateHistoryResponse(
            entity.Id,
            entity.State,
            entity.Created);
    }
    
    public static CreatedSuccess EntityToResponse(string id)
    {
        return new CreatedSuccess(id);
    }

    public string TrackingId { get; set; }
}