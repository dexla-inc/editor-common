using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Types.Models;

public class DataSourceEndpointResponse : DataSourceEndpoint, ISuccess
{
    public string Id { get; set; }
    public string TrackingId { get; set; }
}

public class DataSourceEndpoint : ApiEndpointOpenApi
{
    public string Id { get; set; }
}