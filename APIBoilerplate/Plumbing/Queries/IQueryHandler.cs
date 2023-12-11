using System.Threading.Tasks;

namespace APIBoilerplate.Plumbing.Queries;

public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
{
    Task<TResult> Handle(TQuery query);
}

public interface IQueryHandler<TResult>
{
    Task<TResult> Handle();
}