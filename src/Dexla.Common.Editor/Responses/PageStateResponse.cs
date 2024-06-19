using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Models;
using Dexla.Common.Repository.Types.Models;
using Dexla.Common.Types.Interfaces;
using Dexla.Common.Types.Models;

namespace Dexla.Common.Editor.Responses;

public class PageStateResponse : ISuccess
{
    public string? Id { get; }
    public string State { get; }
    public string Description { get; }
    public long Created { get; }

    public PageStateResponse(string? id, IEnumerable<string> state, string description, long created)
    {
        Id = id;
        State = SetState(state);
        Description = description;
        Created = created;
    }
    
    public PageStateResponse(string? id, IEnumerable<string> state, long created)
    {
        Id = id;
        State = SetState(state);
        Created = created;
    }

    private static string SetState(IEnumerable<string> state) => string.Join("", state);
    
    public static PageStateResponse ModelToResponse(PageStateModel model, string description)
    {
        return new PageStateResponse(model.Id, model.State, description, model.Created);
    }
    
    public static CreatedSuccess ModelToCreatedResponse(string id)
    {
        return new CreatedSuccess(id);
    }
    
    public static IResponse ModelToResponse(RepositoryActionResultModel<PageStateModel> actionResult, string description)
    {
        return actionResult.ActionResult(
            actionResult,
            m => ModelToResponse(m, description));
    }

    public static Func<PageState, PageStateResponse> ModelToResponse(string description)
    {
        return m => new PageStateResponse(
            m.Id,
            m.State,
            description,
            m.Created);
    }
    
    public static Func<PageState, PageStateResponse> ModelToResponse()
    {
        return m => new PageStateResponse(
            m.Id,
            m.State,
            m.Created);
    }
    
    public static PageStateResponse EntityToResponse(PageState entity, string description)
    {
        return new PageStateResponse(
            entity.Id,
            entity.State,
            description,
            entity.Created);
    }
    
    public static PageStateResponse EntityToResponse(PageState entity)
    {
        return new PageStateResponse(
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