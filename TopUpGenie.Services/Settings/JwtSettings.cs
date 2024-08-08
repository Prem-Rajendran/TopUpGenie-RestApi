namespace TopUpGenie.Services.Settings;

/// <summary>
/// JwtSettings
/// </summary>
[ExcludeFromCodeCoverage]
public class JwtSettings
{
    public string? Key { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public int ExpireMinutes { get; set; }
}

