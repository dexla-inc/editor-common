﻿using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Interfaces;
using Dexla.Common.Editor.Models;
using Dexla.Common.Editor.Responses;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Repository.Types.Models;
using Dexla.Common.Types;
using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Implementations;

public class ReadOnlyApiService : DexlaService<Api, ApiModel>, IReadOnlyApiService
{
    private readonly IRepository<ApiEndpoint, ApiEndpointModel> _endpointRepository;
    private readonly IContext _context;

    public ReadOnlyApiService(
        IRepository<Api, ApiModel> repository,
        IRepository<ApiEndpoint, ApiEndpointModel> endpointRepository,
        IContext context) : base(repository)
    {
        _endpointRepository = endpointRepository;
        _context = context;
    }

    public async Task<IResponse> Get(string id, bool? withAuth)
    {
        return await Get(id, ApiResponse.ModelToResponse);
    }

    public async Task<IResponse> List(
        string projectId,
        string? type,
        string? search,
        int offset,
        int limit)
    {
        FilterConfiguration filterConfiguration = new(projectId);

        if (search != null)
            filterConfiguration.Append(nameof(Api.Name), search, SearchTypes.PARTIAL);

        if (type != null)
            filterConfiguration.Append(nameof(Api.Type), type, SearchTypes.EXACT);

        (IReadOnlyList<Api> entities, int totalRecords) = await _context.GetEntities<Api>(filterConfiguration);

        List<ApiResponse> results = entities.Select(m =>
            new ApiResponse(
                m.Id,
                m.Name,
                m.AuthenticationScheme,
                m.Environment,
                m.BaseUrl,
                m.SwaggerUrl,
                m.Updated,
                m.Type,
                m.AuthValue,
                m.IsTested)
        ).ToList();

        return new PagedResponse<ApiResponse>
        {
            Results = results,
            Paging = new PagingModel(totalRecords, results.Count, offset, limit)
        };
    }

    public async Task<IResponse> ListEndpoints(
        string projectId,
        string? dataSourceId,
        string? methodType,
        bool authOnly,
        int offset,
        int limit)
    {
        FilterConfiguration filterConfiguration = _addFilterConfiguration(projectId, dataSourceId);

        if (methodType != null)
        {
            bool valid = Enum.TryParse(methodType, true, out MethodTypes methodTypeParsed);
            if (valid)
                filterConfiguration.Append(nameof(ApiEndpoint.MethodType), methodTypeParsed, SearchTypes.EXACT);
        }

        if (authOnly)
        {
            filterConfiguration.AppendArray(
                "authentication.endpointType",
                ApiEndpoint.AllowedEndpointTypes().ToList(),
                SearchTypes.ONE_OF);
        }

        (IReadOnlyList<ApiEndpoint> entities, int totalRecords) =
            await _context.GetEntities<ApiEndpoint>(filterConfiguration);

        FilterConfiguration filterConfig = new(projectId);
        if (dataSourceId != null)
            filterConfig.Append(nameof(Api.Id), dataSourceId, SearchTypes.EXACT);
        (IReadOnlyList<Api> apis, int _) = await _context.GetEntities<Api>(filterConfig);
        List<Api> dataSources = apis.ToList();

        var joinedResults = entities.Join(
            dataSources,
            entity => entity.ApiId,
            dataSource => dataSource.Id,
            (entity, dataSource) => new { Entity = entity, DataSource = dataSource }
        );
        
        List<ApiEndpointResponse> results = joinedResults
            .Select(m => new ApiEndpointResponse(
                m.Entity.Id,
                dataSourceId ?? m.DataSource.Id,
                string.IsNullOrEmpty(m.Entity.BaseUrl) ? m.DataSource.BaseUrl : m.Entity.BaseUrl,
                string.Join("/", m.DataSource.BaseUrl, m.Entity.RelativeUrl),
                m.Entity.RelativeUrl,
                m.Entity.Description,
                m.Entity.MethodType,
                m.Entity.AuthenticationScheme != null
                    ? Enum.Parse<AuthenticationSchemes>(m.Entity.AuthenticationScheme, true)
                    : AuthenticationSchemes.NONE,
                m.Entity.WithCredentials,
                m.Entity.MediaType,
                m.Entity.Authentication,
                m.Entity.Headers,
                m.Entity.Parameters,
                m.Entity.RequestBody,
                m.Entity.Body,
                m.Entity.ExampleResponse,
                m.Entity.ErrorExampleResponse,
                m.Entity.IsServerRequest)
            )
            .ToList();

        return new PagedResponse<ApiEndpointResponse>
        {
            Results = results,
            Paging = new PagingModel(totalRecords, results.Count, offset, limit)
        };
    }

    public async Task<IResponse> GetEndpoint(string id)
    {
        RepositoryActionResultModel<ApiEndpointModel> actionResult = await _endpointRepository.Get(id);

        return ApiResponse.ModelToResponse(actionResult);
    }

    public async Task<IResponse> GetAuthConfig(string projectId, string id)
    {
        IResponse response = await ListEndpoints(projectId, id, null, true, 0, 3);

        if (response is not PagedResponse<ApiEndpointResponse> authEndpoints)
            return response;

        ApiEndpointResponse? accessTokenEndpoint =
            authEndpoints.Results.FirstOrDefault(e => e.Authentication?.EndpointType == EndpointTypes.ACCESS);
        ApiEndpointResponse? refreshTokenEndpoint =
            authEndpoints.Results.FirstOrDefault(e => e.Authentication?.EndpointType == EndpointTypes.REFRESH);
        ApiEndpointResponse? userEndpoint =
            authEndpoints.Results.FirstOrDefault(e => e.Authentication?.EndpointType == EndpointTypes.USER);

        return new DatasourceAuthConfigurationResponse(
            accessTokenEndpoint?.AuthenticationScheme ?? AuthenticationSchemes.NONE,
            accessTokenEndpoint?.Url,
            refreshTokenEndpoint?.Url,
            userEndpoint?.Url,
            accessTokenEndpoint?.Authentication?.TokenKey,
            refreshTokenEndpoint?.Authentication?.TokenKey,
            accessTokenEndpoint?.Authentication?.TokenSecondaryKey);
    }
    
    public FilterConfiguration _addFilterConfiguration(
        string projectId,
        string? dataSourceId,
        string? relativeUrl = null,
        string? methodType = null)
    {
        FilterConfiguration filterConfiguration = new(projectId);

        if (dataSourceId != null)
            filterConfiguration.Append(nameof(ApiEndpoint.ApiId), dataSourceId, SearchTypes.EXACT);

        if (relativeUrl != null)
            filterConfiguration.Append(nameof(ApiEndpoint.RelativeUrl), relativeUrl, SearchTypes.EXACT);

        if (methodType != null)
            filterConfiguration.Append(nameof(ApiEndpoint.MethodType), methodType, SearchTypes.EXACT);

        return filterConfiguration;
    }
}