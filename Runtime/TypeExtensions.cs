using System;
using System.Collections.Generic;
using System.Linq;

namespace Slothsoft.UnityExtensions {
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
