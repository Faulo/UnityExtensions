using System;
using System.Collections.Generic;
using System.Linq;

namespace CursedBroom.Core.Extensions {
    public static class TypeExtensions {
        public static IEnumerable<Type> FindImplementations(this Type type) {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(type.IsAssignableFrom)
                .Where(t => !t.IsInterface && !t.IsAbstract);
        }
    }
}
