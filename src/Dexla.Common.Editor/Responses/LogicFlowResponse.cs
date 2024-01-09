﻿using Dexla.Common.Types.Interfaces;

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

    public string TrackingId { get; set; }
}