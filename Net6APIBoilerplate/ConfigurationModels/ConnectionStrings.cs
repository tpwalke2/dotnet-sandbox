namespace Net6APIBoilerplate.ConfigurationModels;

[ConfigSection(nameof(ConnectionStrings))]
public record ConnectionStrings
{
    public string PrimaryConnection { get; init; }
}