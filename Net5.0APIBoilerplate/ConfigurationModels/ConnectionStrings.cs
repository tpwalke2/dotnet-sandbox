namespace Net5._0APIBoilerplate.ConfigurationModels
{
    [ConfigSection(nameof(ConnectionStrings))]
    public record ConnectionStrings
    {
        public string PrimaryConnection { get; init; }
    }
}
