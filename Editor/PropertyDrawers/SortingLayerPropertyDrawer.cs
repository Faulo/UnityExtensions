﻿using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Editor.PropertyDrawers {
    [CustomPropertyDrawer(typeof(SortingLayerAttribute))]
    public class SortingLayerPropertyDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            typeof(EditorGUI)
                .GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
                .First(m => m.Name == "SortingLayerField")
                .Invoke(null, new object[] { position, label, property, EditorStyles.popup, EditorStyles.label });
        }
    }
}