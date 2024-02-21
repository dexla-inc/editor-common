using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class PageStateResponse : ISuccess
{
    public string? Id { get; }
    public string State { get; }
    public long Created { get; }

    public PageStateResponse(string? id, IEnumerable<string> state, long created)
    {
        Id = id;
        State = SetState(state);
        Created = created;
    }


    private static string SetState(IEnumerable<string> state) => string.Join("", state);

    public string TrackingId { get; set; }
}