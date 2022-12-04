using Net7APIBoilerplate.Plumbing.Models;

namespace Net7APIBoilerplate.Plumbing.Commands;

public interface ICommand<TOutcome>
    where TOutcome : Outcome
{
}