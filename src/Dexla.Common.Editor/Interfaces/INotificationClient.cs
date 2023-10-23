namespace Dexla.Common.Editor.Interfaces;

public interface INotificationClient
{
    Task Send(string message, string title, string? moreInfo = null);
}
