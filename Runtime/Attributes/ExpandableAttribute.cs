using System;
using UnityEngine;

namespace Slothsoft.UnityExtensions {
    /// <summary>
    /// Use this property on a ScriptableObject type to allow the editors drawing the field to draw an expandable
    /// area that allows for changing the values on the object without having to change editor.
    /// </summary>
    public sealed class ExpandableAttribute : PropertyAttribute {
        Type implements;
        public ExpandableAttribute(Type implements = null) {
            this.implements = implements;
        }
        public string label => implements == null
            ? ""
            : $" : {implements.Name}";
        public bool ValidateType(UnityEngine.Object obj) {
            if (implements != null) {
                var objType = obj.GetType();
                if (!implements.IsAssignableFrom(objType)) {
                    Debug.LogWarning(
                        $"Validation failed! Class <i>{objType.Name}</i> of object '{obj.name}' does not implement <i>{implements.Name}</i>.",
                        obj
                    );
                    return false;
                }
            }

            return true;
        }
    }
}