using Net7APIBoilerplate.Plumbing.Models;
using System.Threading.Tasks;

namespace Net7APIBoilerplate.Plumbing.Commands;

public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommand<Outcome<TResult>>
{
    Task<Outcome<TResult>> Handle(TCommand command);
}

public interface ICommandHandler<in TCommand> where TCommand : ICommand<Outcome>
{
    Task<Outcome> Handle(TCommand command);
}