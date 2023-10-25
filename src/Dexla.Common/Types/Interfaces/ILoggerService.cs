namespace Dexla.Common.Types.Interfaces;

/// <summary>
/// A generic interface for logging where the category name is derived from the specified
/// <typeparamref name="TCategoryName"/> type name.
/// Generally used to enable activation of a named <see cref="ILogger"/> from dependency injection.
/// </summary>
/// <typeparam name="TCategoryName">The type whose name is used for the logger category name.</typeparam>
public interface ILoggerService<out TCategoryName> where TCategoryName : class
{
    Task LogError(Exception ex, string title, string messageTemplate, params object[] args);
    Task LogError(string title,string messageTemplate,  params object[] args);
    Task LogError(string title);
    void LogInformation(string messageTemplate, params object[] args);
    Task LogCritical( string title, string messageTemplate, params object[] args);
    void LogWarning(string messageTemplate,  params object[] args);
}