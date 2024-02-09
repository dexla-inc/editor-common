using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Utilities;

namespace Dexla.Common.Editor.Entities;

public class Page : IEntity
{
    protected Page()
    {
        
    }
    
    public string Id { get; set; } = UtilityExtensions.GetId();
    public virtual EntityStatus EntityStatus { get; set; }
    public virtual string UserId { get; set; }
    public virtual string ProjectId { get; set; }
    public virtual string Title { get; set; }
    public virtual string Slug { get; set; }
    public virtual string PageState { get; set; }
    public virtual bool IsHome { get; set; }
    public virtual bool AuthenticatedOnly { get; set; }
    public virtual string AuthenticatedUserRole { get; set; }
    public virtual string? ParentPageId { get; set; }
    public virtual bool HasNavigation { get; set; }
    public Dictionary<string,string>? QueryStrings { get; set; }
    public virtual string Description { get; set; }
    public List<Action>? Actions = [];
}