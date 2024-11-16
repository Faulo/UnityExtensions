using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CursedBroom.Core.Extensions;

namespace CursedBroom.Core {
    public sealed class ImplementationLocator<TInterface> where TInterface : class {

        static IEnumerable<Implementation<TInterface>> CreateImplementations(Type type) {
            foreach (var attribute in type.GetCustomAttributes<ImplementationForAttribute>()) {
                if (attribute.interfaceType == typeof(TInterface)) {
                    yield return new Implementation<TInterface>(type, attribute);
                }
            }
        }

        public IReadOnlyList<Implementation<TInterface>> implementations {
            get {
                return m_locators ??= typeof(TInterface)
                    .FindImplementations()
                    .SelectMany(CreateImplementations)
                    .OrderBy(implementation => implementation)
                    .ToArray();
            }
        }

        Implementation<TInterface>[] m_locators;
    }
}
