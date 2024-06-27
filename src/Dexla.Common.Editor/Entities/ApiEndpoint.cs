﻿using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Types.Enums;

namespace Dexla.Common.Editor.Entities;

public class ApiEndpoint : IEntity
{
    public string Id { get; set; } = string.Empty;
    public EntityStatus EntityStatus { get; set; }
    public BasicAuditInformation? AuditInformation { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
    public string ApiId { get; set; } = string.Empty;
    public string RelativeUrl { get; set; } = string.Empty;
    public MethodTypes MethodType { get; set; }
    public string Description { get; set; } = string.Empty;
    public string MediaType { get; set; } = string.Empty;
    public bool? WithCredentials { get; set; }
    public string? AuthenticationScheme { get; set; }
    public List<ApiHeaderDto> Headers { get; set; } = [];
    public List<ApiParameterDto> Parameters { get; set; } = [];
    public List<ApiBodyParameterDto> RequestBody { get; set; } = [];
    public string? Body { get; set; }
    public string? ExampleResponse { get; set; }
    public string? ErrorExampleResponse { get; set; }
    public EndpointAuthentication? Authentication { get; set; }
    public bool IsServerRequest { get; set; }

    public bool AuthOnly()
    {
        return Authentication?.EndpointType is EndpointTypes.ACCESS or EndpointTypes.REFRESH or EndpointTypes.USER;
    }

    public static string[] AllowedEndpointTypes()
    {
        return [EndpointTypes.ACCESS.ToString(), EndpointTypes.REFRESH.ToString(), EndpointTypes.USER.ToString()];
    }
}