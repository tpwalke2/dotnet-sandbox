using APIBoilerplate.Plumbing.Models;

namespace APIBoilerplate.Plumbing.Commands;

public interface ICommand<TOutcome>
    where TOutcome : Outcome
{
}