using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;

namespace Dexla.Common.Editor.Models;

public class AppDto : IModel
{
    public string Id { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public object Configuration { get; set; } = new object();
    public EntityStatus EntityStatus { get; set; }
}