using Dexla.Common.Types.Enums;

namespace Dexla.Common.Editor.Entities;

public class ResponsiveBreakpoint
{
    public ResponsiveBreakpointTypes Type { get; set; }
    public string Breakpoint { get; set; } = string.Empty;
}