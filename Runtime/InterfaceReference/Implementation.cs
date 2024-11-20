using System;

namespace Slothsoft.UnityExtensions {
    public record Implementation<TInterface> : IComparable<Implementation<TInterface>> where TInterface : class {
        readonly Type classType;

        public bool IsInstanceOfType(TInterface instance) => classType.IsInstanceOfType(instance);
        public TInterface CreateInstance() => Activator.CreateInstance(classType) as TInterface;

        readonly ImplementationForAttribute attribute;

        public string label => attribute.label;

        public Implementation(Type classType, ImplementationForAttribute attribute) {
            this.classType = classType;
            this.attribute = attribute;
        }

        public int CompareTo(Implementation<TInterface> other) => attribute.priority.CompareTo(other.attribute.priority);
    }
}
