using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Types.Models;

public class CreatedSuccess : ISuccess
{
    public string Id { get; set; }

    public CreatedSuccess(string id)
    {
        Id = id;
    }

    public string TrackingId { get; set; }
}