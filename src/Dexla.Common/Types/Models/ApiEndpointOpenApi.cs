namespace Dexla.Common.Types.Models;

public class ApiEndpointOpenApi
{
    public string RelativeUrl { get; set; } = string.Empty;
    public string MethodType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string MediaType { get; set; } = string.Empty;
    public bool? WithCredentials { get; set; }
    public string? AuthenticationScheme { get; set; }
    public List<ApiHeader> Headers { get; set; } = new();
    public List<ApiParameter> Parameters { get; set; } = new();
    public List<ApiBodyParameter> RequestBody { get; set; } = new();
    public string? Body { get; set; } = string.Empty;
    public string? ExampleResponse { get; set; } = string.Empty;
    public string? ErrorExampleResponse { get; set; } = string.Empty;
}