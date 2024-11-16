using System;

namespace Slothsoft.UnityExtensions {
    public sealed class ImplementationForAttribute : Attribute {
        internal readonly Type interfaceType;
        internal readonly string label;
        internal readonly int priority;

        public ImplementationForAttribute(Type interfaceType, string label, int priority = 0) {
            this.interfaceType = interfaceType;
            this.label = label;
            this.priority = priority;
        }
    }
}
