namespace Dexla.Common.Types;

public class RateLimitSettings
{
    public RateLimitRule GlobalRule { get; set; }
    public RateLimitRule AIRule { get; set; }
}