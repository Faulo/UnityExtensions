using UnityEditor;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Editor {
    [CustomPropertyDrawer(typeof(LayerAttribute))]
    sealed class LayerAttributeDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            property.intValue = EditorGUI.LayerField(position, label, property.intValue);
        }
    }
}