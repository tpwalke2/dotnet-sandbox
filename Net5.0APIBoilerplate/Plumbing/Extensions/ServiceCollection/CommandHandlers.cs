using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Net5._0APIBoilerplate.Plumbing.Commands;
using Net5._0APIBoilerplate.Plumbing.Helpers;

namespace Net5._0APIBoilerplate.Plumbing.Extensions.ServiceCollection
{
    public static class CommandHandlers
    {
        public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            var commandHandlerTypes = TypeHelpers
                                      .GetAllImplementingTypes(typeof(ICommandHandler<>))
                                      .Concat(TypeHelpers.GetAllImplementingTypes(
                                                  typeof(ICommandHandler<,>)))
                                      .ToList();

            var handlerDependencyTypes = commandHandlerTypes
                                         .SelectMany(t1 => t1.Item2.GetConstructorParameterTypes())
                                         .GroupBy(t => t)
                                         .Select(grp => grp.First())
                                         .Select(t => t.IsInterface
                                                     ? (t, TypeHelpers.GetAllTypesInheritedFromType(t).First())
                                                     : (t, t));
            
            commandHandlerTypes
                .Concat(handlerDependencyTypes)
                .ForEach(services.TryAdd);

            return services;
        }
    }
}
