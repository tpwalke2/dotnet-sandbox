namespace Net5._0APIBoilerplate.Plumbing.Commands
{
    public record Outcome
    {
        public bool Success { get; init; }
        public string ErrorMessage { get; init; }
    }
    
    public record Outcome<T> : Outcome
    {
        public T Result { get; init; }
    }
}
