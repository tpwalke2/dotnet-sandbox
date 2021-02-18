using System;
using System.Collections.Generic;
using System.Linq;
using Net5._0APIBoilerplate.Plumbing.Extensions;
using Net5._0APIBoilerplate.Plumbing.Validation;

namespace Net5._0APIBoilerplate.Plumbing.Helpers
{
    public static class TypeHelpers
    {
        public static IEnumerable<(TAttr, Type)> GetTypesWithAttribute<TAttr>()  where TAttr: Attribute
        {
            return from a in AppDomain.CurrentDomain.GetAssemblies()
                   from t in a.GetTypes()
                   from attr in t.GetCustomAttributes(typeof(TAttr), false)
                   select ((TAttr) attr, t);
        }

        public static IEnumerable<Type> GetConstructorParameterTypes<T>() where T : class
        {
            return typeof(T).GetConstructorParameterTypes();
        }
        
        public static IEnumerable<(Type, Type)> GetAllImplementingTypes(Type type)
        {
            if (!type.IsInterface && !type.IsGenericType)
                throw new InvalidArgumentsException("Type must be a generic interface");

            return GetAllTypesInheritedFromType(type)
                .SelectMany(t => t
                                 .GetInterfaces()
                                 .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == type)
                                 .Select(i => (i, t)));
        }

        public static IEnumerable<Type> GetAllTypesInheritedFromType(Type type)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(s => s.GetTypes())
                            .Where(p => IsAssignableFromType(type, p));
        }

        private static bool IsAssignableFromType(Type type, Type p)
        {
            return !p.IsInterface
                   && type.IsAssignableFrom(p)
                   || p.GetInterfaces()
                       .Any(i => i.IsGenericType
                                 && i.GetGenericTypeDefinition() == type);
        }
    }
}
