﻿using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;

namespace Dexla.Common.Editor.Entities;

public class PageStateHistory : IEntity
{
    public virtual string Id { get; set; } = string.Empty;
    public virtual EntityStatus EntityStatus { get; set; }
    public virtual string UserId { get; set; } = string.Empty;
    public virtual string ProjectId { get; private set; } = string.Empty;
    public virtual string PageId { get; set; } = string.Empty;
    public virtual List<string> State { get; set; } = [];
    public virtual long Created { get; set; }
}