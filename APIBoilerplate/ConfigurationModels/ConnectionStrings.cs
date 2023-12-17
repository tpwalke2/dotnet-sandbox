namespace APIBoilerplate.ConfigurationModels;

[ConfigSection(nameof(ConnectionStrings))]
public record ConnectionStrings
{
    public string PrimaryConnection { get; init; }
}