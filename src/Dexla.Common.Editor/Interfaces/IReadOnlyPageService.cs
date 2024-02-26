using System.Threading.Tasks;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Interfaces;

public interface IReadOnlyPageService : IDexlaService
{
    Task<IResponse> List(
        string projectId,
        string? search,
        int offset,
        int take,
        bool? isHome,
        string? slug = null);
    Task<IResponse> Get(string id);
    Task<IResponse> GetBySlug(string projectId, string slug);
}