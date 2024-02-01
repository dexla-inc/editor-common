using Dexla.Common.Editor.Entities;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Types;

namespace Dexla.Common.Editor.Interfaces;

public interface IEditorContext : IContext
{ 
    Task<IEnumerable<Deployment>> GetMostRecentEnvironmentDeployments(string projectId);

    Task<TResult> JoinCollections<TEntity, TForeignEntity, TResult>(
        FilterConfiguration filterConfiguration,
        string localField,
        string foreignField,
        string foreignCollectionName,
        CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
        where TForeignEntity : class, IEntity
        where TResult : class, IEntity;
}
