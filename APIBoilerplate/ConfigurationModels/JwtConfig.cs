namespace APIBoilerplate.ConfigurationModels;

[ConfigSection("JWT")]
public record JwtConfig
{
    public string ValidAudience { get; init; }
    public string ValidIssuer { get; init; }
    public string Secret { get; init; }
}