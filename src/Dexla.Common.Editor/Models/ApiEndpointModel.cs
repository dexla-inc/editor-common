﻿using Dexla.Common.Editor.Entities;
using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Types;
using Dexla.Common.Types.Enums;
using Dexla.Common.Utilities;

namespace Dexla.Common.Editor.Models;

public class ApiEndpointModel : IModelWithProjectId, IModelWithOnboarding
{
    public string? Id { get; set; } = UtilityExtensions.GetId();
    public EntityStatus EntityStatus { get; set; }
    public BasicAuditInformation? AuditInformation { get; set; }

    public string UserId { get; set; } = string.Empty;
    public string CompanyId { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string ApiId { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
    public string RelativeUrl { get; set; } = string.Empty;
    public string MethodType { get; set; } = string.Empty;
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
    public EndpointAuthenticationDto Authentication { get; set; } = new();
    public bool IsServerRequest { get; set; }
    public bool AutoGenerated { get; set; }

    public void SetUserId(string value)
    {
        UserId = value;
    }

    public void SetProjectId(string projectId)
    {
        ProjectId = projectId;
    }

    public void SetCompanyId(string value)
    {
        CompanyId = value;
    }
    
    public void SetApiId(string apiId)
    {
        ApiId = apiId;
    }

    public void SetBaseUrl(string baseUrl)
    {
        BaseUrl = baseUrl;
    }

    public void SetRelativeUrl(string relativeUrl)
    {
        RelativeUrl = relativeUrl;
    }

    public void SetMethodType(string methodType)
    {
        MethodType = methodType;
    }

    public void SetNoAuthenticationScheme()
    {
        AuthenticationScheme = AuthenticationSchemes.NONE.ToString();
    }

    public void SetDescription(string value)
    {
        Description = value;
    }

    public void CleanRelativeUrl()
    {
        RelativeUrl = RelativeUrl.TrimStart('/');
    }

    public void SetOnboarding(bool value)
    {
        IsOnboarding = value;
    }
    
    public void SetAuthentication(EndpointAuthenticationDto value)
    {
        Authentication = value;
    }

    public bool IsOnboarding { get; set; }
}