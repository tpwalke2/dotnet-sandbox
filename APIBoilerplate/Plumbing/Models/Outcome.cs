namespace APIBoilerplate.Plumbing.Models;

public record Outcome
{
    public bool Success { get; init; }
    public string ErrorMessage { get; init; }
}
    
public record Outcome<T> : Outcome
{
    public T Result { get; init; }
}