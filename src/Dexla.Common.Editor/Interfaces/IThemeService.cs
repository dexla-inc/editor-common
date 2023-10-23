using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Interfaces;

public interface IThemeService :IDexlaService
{
    Task<IResponse> Get(string projectId, CancellationToken cancellationToken);
}