using System;
using UnityEngine;

namespace Slothsoft.UnityExtensions {
    /// <summary>
    /// Use this property on a ScriptableObject type to allow the editors drawing the field to draw an expandable
    /// area that allows for changing the values on the object without having to change editor.
    /// </summary>
    public class ExpandableAttribute : PropertyAttribute {
        public Type[] restrictedTypes;
        public ExpandableAttribute(params Type[] restrictedTypes) {
            this.restrictedTypes = restrictedTypes;
        }
        public bool ValidateType(object obj) {
            var objType = obj.GetType();
            foreach (var type in restrictedTypes) {
                if (!type.IsAssignableFrom(objType)) {
                    Debug.LogWarning($"Validation failed! Class <i>{objType.Name}</i> does not implement <b>{type.Name}</b>.");
                    return false;
                }
            }
            return true;
        }
    }
}