namespace Dexla.Common.Types.Interfaces;

public interface IFail : IResponse
{
    public string Message { get; set; }
    public IEnumerable<ErrorDetail> Errors { get; set; }
}