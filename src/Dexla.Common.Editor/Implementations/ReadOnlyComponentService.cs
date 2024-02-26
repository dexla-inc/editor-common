using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Interfaces;
using Dexla.Common.Editor.Models;
using Dexla.Common.Editor.Responses;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Repository.Types.Models;
using Dexla.Common.Types;
using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Implementations;

public class ReadOnlyComponentService(
        IRepository<Component, ComponentModel> repository,
        IContext context)
    : DexlaService<Component, ComponentModel>(repository), IReadOnlyComponentService
{
    public async Task<IResponse> List(string projectId, string companyId, string scopes, string? search)
    {
        FilterConfiguration filterConfiguration = new();

        filterConfiguration.AppendArray(
            new Dictionary<string, object>
            {
                { nameof(Component.ProjectId), projectId },
                { nameof(Component.CompanyId), companyId },
            },
            SearchTypes.OR);

        List<string> scopesList = [];
        if (scopes.Contains(','))
            scopesList = scopes.Split(",").ToList();
        else
            scopesList.Add(scopes);

        filterConfiguration.AppendArray(nameof(Component.Scope), scopesList, SearchTypes.ONE_OF);
        
        if (search != null)
            filterConfiguration.Append(nameof(Component.Description), search, SearchTypes.PARTIAL);

        (IReadOnlyList<Component> entities, int totalRecords) =
            await context.GetEntities<Component>(filterConfiguration);

        return new PagedResponse<ComponentResponse>
        {
            Results = entities.Select(_getResponse()).ToList(),
            Paging = new PagingModel(totalRecords, entities.Count, 0, entities.Count)
        };
    }

    public Func<Component, ComponentResponse> _getResponse()
    {
        return entity => new ComponentResponse(
            entity.Id,
            entity.Type.ToString(),
            entity.Description,
            entity.Content,
            entity.ImagePreviewUrl,
            entity.Scope);
    }

    public ComponentResponse _getResponse(ComponentModel model)
    {
        return new ComponentResponse(
            model.Id!,
            model.Type,
            model.Description,
            model.Content,
            model.ImagePreviewUrl,
            Enum.Parse<ComponentScopes>(model.Scope));
    }
}