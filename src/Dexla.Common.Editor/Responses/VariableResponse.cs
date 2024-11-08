﻿using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Models;
using Dexla.Common.Repository.Types.Models;
using Dexla.Common.Types;
using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class VariableResponse : IMapperResponse<Variable, VariableModel, VariableResponse>, ISuccess
{
    public string Id { get; }
    public string Name { get; }
    public FrontEndTypes Type { get; }
    public object DefaultValue { get; }
    public bool IsGlobal { get; }

    public VariableResponse(
        string id,
        string name,
        FrontEndTypes type,
        object defaultValue,
        bool isGlobal)
    {
        Id = id;
        Name = name;
        Type = type;
        DefaultValue = defaultValue;
        IsGlobal = isGlobal;
    }

    internal VariableResponse()
    {
    }

    public string TrackingId { get; set; }

    public static Func<Variable, VariableResponse> EntityToResponse()
    {
        return m => new VariableResponse(
            m.Id,
            m.Name,
            m.Type,
            Json.Deserialize<object>(m.DefaultValue),
            m.IsGlobal);
    }

    public static VariableResponse ModelToResponse(VariableModel model)
    {
        return new VariableResponse(
            model.Id!,
            model.Name,
            Enum.Parse<FrontEndTypes>(model.Type),
            model.DefaultValue,
            model.IsGlobal);
    }

    public static IResponse ModelToResponse(RepositoryActionResultModel<VariableModel> actionResult)
    {
        return actionResult.ActionResult(
            actionResult,
            ModelToResponse);
    }

    public static VariableModel ResponseToModel(VariableResponse response)
    {
        return new VariableModel
        {
            Id = response.Id,
            Name = response.Name,
            Type = response.Type.ToString(),
            DefaultValue = Json.Serialize(response.DefaultValue),
            IsGlobal = response.IsGlobal
        };
    }
}