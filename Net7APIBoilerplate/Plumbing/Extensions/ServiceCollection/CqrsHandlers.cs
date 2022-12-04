using Microsoft.Extensions.DependencyInjection;
using Net7APIBoilerplate.Plumbing.Commands;
using Net7APIBoilerplate.Plumbing.Helpers;
using Net7APIBoilerplate.Plumbing.Queries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Net7APIBoilerplate.Plumbing.Extensions.ServiceCollection;

public static class CommandHandlers
{
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        var commandHandlerTypes = TypeHelpers
                                  .GetAllImplementingTypes(typeof(ICommandHandler<>))
                                  .Concat(TypeHelpers.GetAllImplementingTypes(
                                              typeof(ICommandHandler<,>)))
                                  .ToList();

        return services.AddHandlersWithDependencies(commandHandlerTypes);
    }
        
    public static IServiceCollection AddQueryHandlers(this IServiceCollection services)
    {
        var queryHandlerTypes = TypeHelpers
                                .GetAllImplementingTypes(typeof(IQueryHandler<>))
                                .Concat(TypeHelpers.GetAllImplementingTypes(
                                            typeof(IQueryHandler<,>)))
                                .ToList();

        return services.AddHandlersWithDependencies(queryHandlerTypes);
    }

    private static IServiceCollection AddHandlersWithDependencies(
        this IServiceCollection services,
        IList<(Type, Type)> handlerTypes)
    {
        var handlerDependencyTypes = handlerTypes
                                     .SelectMany(t1 => t1.Item2.GetConstructorParameterTypes())
                                     .GroupBy(t => t)
                                     .Select(grp => grp.First())
                                     .Select(t => t.IsInterface
                                                 ? (t, TypeHelpers.GetAllTypesInheritedFromType(t).First())
                                                 : (t, t));
            
        handlerTypes
            .Concat(handlerDependencyTypes)
            .ForEach(services.TryAdd);

        return services;
    }
}