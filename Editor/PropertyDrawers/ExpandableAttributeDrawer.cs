using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Editor {
    /// <summary>
    /// Draws the property field for any field marked with ExpandableAttribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(ExpandableAttribute), true)]
    sealed class ExpandableAttributeDrawer : PropertyDrawer {
        static ExpandableSettings settings => UnityExtensionsSettings.instance.expandableSettings;

        new ExpandableAttribute attribute => base.attribute as ExpandableAttribute;

        bool CanExpand(SerializedProperty property) {
            if (property.propertyType != SerializedPropertyType.ObjectReference) {
                return false;
            }

            if (property.isArray) {
                return false;
            }

            if (!property.objectReferenceValue) {
                return false;
            }

            return true;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            float totalHeight = EditorGUIUtility.standardVerticalSpacing;

            totalHeight += EditorGUIUtility.singleLineHeight;

            if (!CanExpand(property)) {
                return totalHeight;
            }

            if (!property.isExpanded) {
                return totalHeight;
            }

            var targetObject = new SerializedObject(property.objectReferenceValue);

            var field = targetObject.GetIterator();

            field.NextVisible(true);

            if (settings.showSourceFile) {
                totalHeight += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            }

            while (field.NextVisible(false)) {
                totalHeight += EditorGUI.GetPropertyHeight(field, true) + EditorGUIUtility.standardVerticalSpacing;
            }

            totalHeight += settings.totalSpacing * 2;
            totalHeight += EditorGUIUtility.standardVerticalSpacing;

            return totalHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var fieldRect = new Rect(position) {
                height = EditorGUIUtility.singleLineHeight
            };
            label.text += attribute.label;
            EditorGUI.PropertyField(fieldRect, property, label, true);

            if (!CanExpand(property)) {
                return;
            }

            if (!attribute.ValidateType(property.objectReferenceValue)) {
                property.objectReferenceValue = null;
                return;
            }

            property.isExpanded = EditorGUI.Foldout(fieldRect, property.isExpanded, GUIContent.none, true);

            if (!property.isExpanded) {
                return;
            }

            var targetObject = new SerializedObject(property.objectReferenceValue);

            #region Format Field Rects
            var propertyRects = new List<Rect>();
            var marchingRect = new Rect(fieldRect);

            var bodyRect = new Rect(fieldRect);
            bodyRect.xMin += EditorGUI.indentLevel * 14;
            bodyRect.yMin += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing + settings.outerSpacing;

            var field = targetObject.GetIterator();
            field.NextVisible(true);

            marchingRect.y += settings.totalSpacing + EditorGUIUtility.standardVerticalSpacing;

            if (settings.showSourceFile) {
                marchingRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                propertyRects.Add(marchingRect);
            }

            while (field.NextVisible(false)) {
                marchingRect.y += marchingRect.height + EditorGUIUtility.standardVerticalSpacing;
                marchingRect.height = EditorGUI.GetPropertyHeight(field, true);
                propertyRects.Add(marchingRect);
            }

            marchingRect.y += settings.innerSpacing;
            marchingRect.y += EditorGUIUtility.standardVerticalSpacing;

            bodyRect.yMax = marchingRect.yMax;
            #endregion

            DrawBackground(bodyRect);

            #region Draw Fields
            EditorGUI.indentLevel++;

            int index = 0;
            field = targetObject.GetIterator();
            field.NextVisible(true);

            if (settings.showSourceFile) {
                // Show the disabled script field
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.PropertyField(propertyRects[index], field, true);
                EditorGUI.EndDisabledGroup();
                index++;
            }

            // Replacement for "editor.OnInspectorGUI ();" so we have more control on how we draw the editor
            while (field.NextVisible(false)) {
                try {
                    EditorGUI.PropertyField(propertyRects[index], field, true);
                } catch (StackOverflowException) {
                    field.objectReferenceValue = null;
                    Debug.LogError("Detected self-nesting causing a StackOverflowException, avoid using the same object inside a nested structure.");
                }

                index++;
            }

            targetObject.ApplyModifiedProperties();

            EditorGUI.indentLevel--;
            #endregion
        }

        /// <summary>
        /// Draws the Background
        /// </summary>
        /// <param name="rect">The Rect where the background is drawn.</param>
        void DrawBackground(Rect rect) {
            switch (settings.backgroundStyle) {
                case ExpandableBackgroundStyle.None:
                    break;
                case ExpandableBackgroundStyle.HelpBox:
                    EditorGUI.HelpBox(rect, "", MessageType.None);
                    break;
                case ExpandableBackgroundStyle.Darken:
                    EditorGUI.DrawRect(rect, settings.darkenColor);
                    break;
                case ExpandableBackgroundStyle.Lighten:
                    EditorGUI.DrawRect(rect, settings.lightenColor);
                    break;
            }
        }
    }
}