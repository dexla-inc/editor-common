namespace Dexla.Common.Types.Configuration
{
    public record MongoDbSettings
    {
        public string ConnectionString { get; init; } = string.Empty;
        public string DatabaseName { get; init; } = string.Empty;
    }
}