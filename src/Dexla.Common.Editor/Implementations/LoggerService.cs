using System;
using System.Threading.Tasks;
using Dexla.Common.Editor.Interfaces;
using Dexla.Common.Types.Interfaces;
using Microsoft.Extensions.Logging;

namespace Dexla.Common.Editor.Implementations;

public class LoggerService<TCategoryName> : ILoggerService<TCategoryName> where TCategoryName : class
{
    private readonly ILogger<TCategoryName> _logger;
    private readonly INotificationClient _notificationClient;

    public LoggerService(
        ILogger<TCategoryName> logger,
        INotificationClient notificationClient)
    {
        _logger = logger;
        _notificationClient = notificationClient;
    }

    public Task LogError(string title, string messageTemplate, params object[] args)
    {
        return LogErrorInternal(null, title, messageTemplate, args);
    }

    public Task LogError(Exception ex, string title, string messageTemplate, params object[] args)
    {
        return LogErrorInternal(ex, title, messageTemplate, args);
    }

    private Task LogErrorInternal(Exception? ex, string title, string messageTemplate, params object[] args)
    {
        _logger.LogError(messageTemplate, args);

        string reformatted;
        try
        {
            reformatted = string.Format(messageTemplate, args);
        }
        catch (FormatException e)
        {
            _logger.LogError("Error occurred during string.Format: {Message}", e.Message);
            reformatted = messageTemplate;
        }

        return _notificationClient.Send(reformatted, title, ex?.StackTrace);
    }

    public Task LogError(string title)
    {
        _logger.LogError(title);
        return _notificationClient.Send(title, title);
    }

    public Task LogCritical(string title, string messageTemplate, params object[] args)
    {
        _logger.LogCritical(messageTemplate, args);
        return _notificationClient.Send(messageTemplate, title);
    }

    public void LogWarning(string messageTemplate, params object[] args)
    {
        _logger.LogWarning(messageTemplate, args);
    }

    public void LogInformation(string messageTemplate, params object[] args)
    {
        _logger.LogInformation(messageTemplate, args);
    }
}