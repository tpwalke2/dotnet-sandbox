using Net6APIBoilerplate.Plumbing.Models;

namespace Net6APIBoilerplate.Plumbing.Commands;

public interface ICommand<TOutcome>
    where TOutcome : Outcome
{
}