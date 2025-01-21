namespace vortexUserConfig.UsersConfig.Presentation.Services.JwtConfig;

public class JwtSettings
{
    public const string SectionName = "JwtSettings";

    public string Issuer = null!;

    public string Secret { get; init; } = null!;

    public int ExpiryMinutes { get; init; }

    public string Audience { get; init; } = null!;
}