using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Models;
using Dexla.Common.Repository.Types.Models;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class LogicFlowResponse : ISuccess
{
    public string Id { get; }
    public string Name { get; }
    public string Data { get; }
    public long CreatedAt { get; }
    public long UpdatedAt { get; }

    public LogicFlowResponse(
        string id,
        string name,
        string data,
        long createdAt,
        long updatedAt)
    {
        Id = id;
        Name = name;
        Data = data;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
    
    public static Func<LogicFlow, LogicFlowResponse> EntityToResponse()
    {
        return e => new LogicFlowResponse(
            e.Id,
            e.Name,
            e.Data,
            e.CreatedAt,
            e.UpdatedAt);
    }

    public static LogicFlowResponse ModelToResponse(LogicFlowModel model)
    {
        return new LogicFlowResponse(
            model.Id!,
            model.Name,
            model.Data,
            model.CreatedAt,
            model.UpdatedAt);
    }

    public static IResponse ModelToResponse(RepositoryActionResultModel<LogicFlowModel> actionResult)
    {
        return actionResult.ActionResult(
            actionResult,
            ModelToResponse);
    }

    public string TrackingId { get; set; }

    public static LogicFlowModel ResponseToModel(LogicFlowResponse response)
    {
        return new LogicFlowModel
        {
            Id = response.Id,
            Name = response.Name,
            Data = response.Data,
            CreatedAt = response.CreatedAt,
            UpdatedAt = response.UpdatedAt
        };
    }
}