using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor;

namespace Slothsoft.UnityExtensions {
    /// <summary>
    /// Draws the property field for any field marked with ExpandableAttribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(ExpandableAttribute), true)]
    public class ExpandableAttributeDrawer : PropertyDrawer {
        // Use the following area to change the style of the expandable ScriptableObject drawers;
        #region Style Setup
        /// <summary>
        /// Whether the default editor Script field should be shown.
        /// </summary>
        private const bool SHOW_SCRIPT_FIELD = true;

        /// <summary>
        /// The spacing on the inside of the background rect.
        /// </summary>
        private const float INNER_SPACING = 4.0f;

        /// <summary>
        /// The spacing on the outside of the background rect.
        /// </summary>
        private const float OUTER_SPACING = 2.0f;
        #endregion

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            float totalHeight = 0.0f;

            totalHeight += EditorGUIUtility.singleLineHeight;

            if (property.objectReferenceValue == null)
                return totalHeight;

            if (!property.isExpanded)
                return totalHeight;

            SerializedObject targetObject = new SerializedObject(property.objectReferenceValue);

            if (targetObject == null)
                return totalHeight;

            SerializedProperty field = targetObject.GetIterator();

            field.NextVisible(true);

            if (SHOW_SCRIPT_FIELD) {
                totalHeight += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            }

            while (field.NextVisible(false)) {
                totalHeight += EditorGUI.GetPropertyHeight(field, true) + EditorGUIUtility.standardVerticalSpacing;
            }

            totalHeight += INNER_SPACING * 2;
            totalHeight += OUTER_SPACING * 2;

            return totalHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            Rect fieldRect = new Rect(position);
            fieldRect.height = EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField(fieldRect, property, label, true);

            if (property.objectReferenceValue == null)
                return;

            property.isExpanded = EditorGUI.Foldout(fieldRect, property.isExpanded, GUIContent.none, true);

            if (!property.isExpanded)
                return;

            SerializedObject targetObject = new SerializedObject(property.objectReferenceValue);

            if (targetObject == null)
                return;


            #region Format Field Rects
            List<Rect> propertyRects = new List<Rect>();
            Rect marchingRect = new Rect(fieldRect);

            Rect bodyRect = new Rect(fieldRect);
            bodyRect.xMin += EditorGUI.indentLevel * 14;
            bodyRect.yMin += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing
                + OUTER_SPACING;

            SerializedProperty field = targetObject.GetIterator();
            field.NextVisible(true);

            marchingRect.y += INNER_SPACING + OUTER_SPACING + EditorGUIUtility.standardVerticalSpacing;


            if (SHOW_SCRIPT_FIELD) {
                marchingRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                propertyRects.Add(marchingRect);
            }

            while (field.NextVisible(false)) {
                marchingRect.y += marchingRect.height + EditorGUIUtility.standardVerticalSpacing;
                marchingRect.height = EditorGUI.GetPropertyHeight(field, true);
                propertyRects.Add(marchingRect);
            }

            marchingRect.y += INNER_SPACING;

            bodyRect.yMax = marchingRect.yMax;
            #endregion

            // Draw the background
            EditorGUI.HelpBox(bodyRect, "", MessageType.None);


            #region Draw Fields
            EditorGUI.indentLevel++;

            int index = 0;
            field = targetObject.GetIterator();
            field.NextVisible(true);

            if (SHOW_SCRIPT_FIELD) {
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
                    Debug.LogError("Detected self-nesting cauisng a StackOverflowException, avoid using the same object iside a nested structure.");
                }

                index++;
            }

            targetObject.ApplyModifiedProperties();

            EditorGUI.indentLevel--;
            #endregion
        }
    }
}