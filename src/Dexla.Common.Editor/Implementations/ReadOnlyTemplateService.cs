using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Interfaces;
using Dexla.Common.Editor.Models;
using Dexla.Common.Editor.Responses;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Repository.Types.Models;
using Dexla.Common.Types;
using Dexla.Common.Types.Interfaces;
using Dexla.EditorAPI.Core.Entities;

namespace Dexla.Common.Editor.Implementations;

public class ReadOnlyTemplateService(
        IRepository<Template, TemplateModel> repository,
        IContext context)
    : DexlaService<Template, TemplateModel>(repository), IReadOnlyTemplateService
{
    public async Task<IResponse> Get(string id)
    {
        return await Get(id, _getResponse);
    }

    public async Task<IResponse> List(
        int offset,
        int limit)
    {
        FilterConfiguration filterConfiguration = new();

        (IReadOnlyList<Template> entities, int totalRecords) =
            await context.GetEntities<Template>(filterConfiguration);

        return new PagedResponse<TemplateResponse>
        {
            Results = entities.Select(_getResponse()).ToList(),
            Paging = new PagingModel(totalRecords, entities.Count, offset, limit)
        };
    }

    public Func<Template, TemplateResponse> _getResponse()
    {
        return entity => new TemplateResponse(
            entity.Id,
            entity.Name,
            entity.State,
            entity.Type,
            entity.Tags,
            entity.UpdatedAt,
            entity.CreatedAt);
    }

    public TemplateResponse _getResponse(TemplateModel model)
    {
        return new TemplateResponse(
            model.Id!,
            model.Name,
            model.State,
            Enum.Parse<TemplateTypes>(model.Type),
            model.Tags.Select(Enum.Parse<TemplateTags>).ToArray(),
            model.UpdatedAt,
            model.CreatedAt);
    }
}