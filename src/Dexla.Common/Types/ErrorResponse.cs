using Dexla.Common.Types.Interfaces;
using Dexla.Common.Types.Models;

namespace Dexla.Common.Types;

public class ErrorResponse : IFail
{
    public ErrorResponse(string message) : this(message, new List<ErrorDetail>())
    {
    }

    public ErrorResponse(string message, IEnumerable<ErrorDetail> errors, string? trackingId = null)
    {
        Message = message;
        Errors = errors;
        TrackingId = trackingId;
    }
    
    public ErrorResponse(string message, string field)
    {
        Message = message;
        Errors = new List<ErrorDetail>
        {
            new(field, message)
        };
    }
    
    public ErrorResponse(string message, string field, string trackingId)
    {
        Message = message;
        Errors = new List<ErrorDetail>
        {
            new(field, message)
        };
        TrackingId = trackingId;
    }

    public string Message { get; set; }
    public string? TrackingId { get; set; }
    public IEnumerable<ErrorDetail> Errors { get; set; }
}