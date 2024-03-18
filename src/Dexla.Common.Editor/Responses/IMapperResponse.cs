using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Repository.Types.Models;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public interface IMapperResponse<TEntity, TModel, TResponse> 
    where TEntity : class, IEntity
    where TModel : class, IModel
    where TResponse : class, ISuccess
{
    public static abstract Func<TEntity, TResponse> EntityToResponse();
    public static abstract TResponse ModelToResponse(TModel model);
    public static abstract IResponse ModelToResponse(RepositoryActionResultModel<TModel> actionResult);
}