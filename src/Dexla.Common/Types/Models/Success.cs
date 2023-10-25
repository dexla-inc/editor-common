using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Types.Models;

public class Success : ISuccess
{
    public static readonly Success Instance = new();

    private Success()
    {
    }

    public string TrackingId { get; set; } = string.Empty;
}