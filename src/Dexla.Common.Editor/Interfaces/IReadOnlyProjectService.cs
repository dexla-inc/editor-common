using System.Threading.Tasks;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Interfaces;

public interface IReadOnlyProjectService : IDexlaService
{
    Task<IResponse> Get(string id, bool includeBranding);
    Task<IResponse> GetByDomain(string domain, bool includeBranding);
}