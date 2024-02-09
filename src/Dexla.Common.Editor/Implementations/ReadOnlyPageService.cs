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

public class ReadOnlyPageService : DexlaService<Page, PageModel>, IReadOnlyPageService
{
    private readonly IContext _context;

    public ReadOnlyPageService(IRepository<Page, PageModel> repository, IContext context) : base(repository)
    {
        _context = context;
    }

    public async Task<IResponse> List(
        string projectId,
        string? search,
        int offset,
        int take,
        bool? isHome,
        string? slug = null)
    {
        FilterConfiguration filterConfiguration = new(projectId);

        if (search != null)
            filterConfiguration.Append(nameof(Page.Title), search, SearchTypes.PARTIAL);

        if (isHome == true)
            filterConfiguration.Append(nameof(Page.IsHome), isHome, SearchTypes.EXACT);
        if (slug != null)
            filterConfiguration.Append(nameof(Page.Slug), slug, SearchTypes.EXACT);

        // Sorting isn't working in MongoDb
        SortConfiguration sortConfiguration = new(nameof(Page.Title), SortDirections.Ascending);

        (IReadOnlyList<Page> entities, int totalRecords) = await _context.GetEntities<Page>(
            filterConfiguration,
            offset,
            take,
            sortConfiguration);

        PagedResponse<PageResponse> results = new()
        {
            Results = entities.Select(_getResponse()).ToList(),
            Paging = new PagingModel(totalRecords, entities.Count, offset, take)
        };

        // Sort by results.title except for the home page
        PageResponse? homePage = results.Results.FirstOrDefault(p => p.IsHome);
        if (homePage != null)
        {
            results.Results.Remove(homePage);
            List<PageResponse> sortedResults = results.Results.OrderBy(p => p.Title).ToList();
            results.Results.Clear();
            results.Results.Add(homePage);
            foreach (PageResponse result in sortedResults)
            {
                results.Results.Add(result);
            }
        }

        return new PagedResponse<PageResponse>
        {
            Results = results.Results,
            Paging = new PagingModel(totalRecords, results.Paging?.TotalRecords ?? 0, offset, take)
        };
    }

    public async Task<IResponse> Get(string id)
    {
        return await Get(id, _getResponse);
    }

    public async Task<IResponse> GetBySlug(string projectId, string slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            slug = "/";

        FilterConfiguration filterConfig = new(projectId);
        filterConfig.Append(nameof(Page.Slug), slug, SearchTypes.EXACT);
        Page? page = await _context.GetByFields<Page>(filterConfig);

        if (page is null)
            return new ErrorResponse("Unable to find page with slug " + slug, nameof(slug));

        return _getResponse(page);
    }

    private static IResponse _getResponse(Page page)
    {
        return new PageResponse(
            page.Id,
            page.ProjectId,
            page.Title,
            page.Slug,
            page.Description,
            page.PageState,
            page.IsHome,
            page.AuthenticatedOnly,
            page.AuthenticatedUserRole,
            page.ParentPageId,
            page.HasNavigation,
            page.QueryStrings,
            page.Actions?.Select(a => new PageActionDto
            {
                Id = a.Id,
                Trigger = a.Trigger,
                Action = a.Action,
                SequentialTo = a.SequentialTo
            }).ToList());
    }

    public Func<Page, PageResponse> _getResponse()
    {
        return page => new PageResponse(
            page.Id,
            page.ProjectId,
            page.Title,
            page.Slug,
            page.Description,
            page.PageState,
            page.IsHome,
            page.AuthenticatedOnly,
            page.AuthenticatedUserRole,
            page.ParentPageId,
            page.HasNavigation,
            page.QueryStrings,
            page.Actions?.Select(a => new PageActionDto
            {
                Id = a.Id,
                Trigger = a.Trigger,
                Action = a.Action,
                SequentialTo = a.SequentialTo
            }).ToList());
    }

    public PageResponse _getResponse(PageModel model)
    {
        return new PageResponse(
            model.Id,
            model.ProjectId,
            model.Title,
            model.Slug,
            model.Description,
            model.PageState,
            model.IsHome,
            model.AuthenticatedOnly,
            model.AuthenticatedUserRole,
            model.ParentPageId,
            model.HasNavigation,
            model.QueryStrings,
            model.Actions?.Select(a => new PageActionDto
            {
                Id = a.Id,
                Trigger = a.Trigger,
                Action = a.Action,
                SequentialTo = a.SequentialTo
            }).ToList());
    }

    public static IResponse _getResponse(RepositoryActionResultModel<PageModel> actionResult)
    {
        return actionResult.ActionResult<PageResponse>(
            actionResult,
            m => new PageResponse(
                m.Id, 
                m.ProjectId, 
                m.Title, 
                m.Slug, 
                m.Description,
                m.PageState,
                m.IsHome,
                m.AuthenticatedOnly,
                m.AuthenticatedUserRole,
                m.ParentPageId,
                m.HasNavigation,
                m.QueryStrings,
                m.Actions));
    }
}