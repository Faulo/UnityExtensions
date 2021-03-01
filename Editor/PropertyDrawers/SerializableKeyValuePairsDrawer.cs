using System;
using UnityEditor;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Editor.PropertyDrawers {
    [CustomPropertyDrawer(typeof(SerializableKeyValuePairs<,>))]
    public class SerializableKeyValuePairsDrawer : PropertyDrawer {

        bool isSingleLine {
            get {
                var dictionaryTypes = fieldInfo.FieldType.GetGenericArguments();
                if (dictionaryTypes.Length != 2) {
                    Debug.LogWarning($"Should have 2 generic types from {fieldInfo}, what's up?");
                }
                var keyType = dictionaryTypes[0];
                var valueType = dictionaryTypes[1];

                return IsInlineType(keyType) || IsInlineType(valueType);
            }
        }
        bool IsInlineType(Type type) => type.IsEnum || type.IsValueType;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            float totalHeight = EditorGUIUtility.standardVerticalSpacing;

            totalHeight += EditorGUIUtility.singleLineHeight;

            if (property.isExpanded) {
                var items = property.FindPropertyRelative("items");
                for (int i = 0; i < items.arraySize; i++) {
                    var item = items.GetArrayElementAtIndex(i);
                    var key = item.FindPropertyRelative("key");
                    var value = item.FindPropertyRelative("value");

                    if (isSingleLine) {
                        totalHeight += Mathf.Max(EditorGUI.GetPropertyHeight(key, true), EditorGUI.GetPropertyHeight(value, true));
                    } else {
                        totalHeight += EditorGUI.GetPropertyHeight(key, true);
                        totalHeight += EditorGUI.GetPropertyHeight(value, true);
                        totalHeight += EditorGUIUtility.standardVerticalSpacing;
                    }
                }
            }

            return totalHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var items = property.FindPropertyRelative("items");

            var rect = position;

            rect.width = position.width - EditorGUIUtility.fieldWidth;
            rect.height = EditorGUIUtility.singleLineHeight;

            var style = new GUIStyle(EditorStyles.foldoutHeader);
            if (items.arraySize == 0) {
                style = new GUIStyle(EditorStyles.label) {
                    contentOffset = style.contentOffset,
                    margin = style.margin,
                    padding = style.padding,
                };
            }
            property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(rect, property.isExpanded, label, style);

            rect.x = position.x + position.width - EditorGUIUtility.fieldWidth;
            rect.y = position.y;
            rect.width = EditorGUIUtility.fieldWidth;
            rect.height = EditorGUIUtility.singleLineHeight;

            items.arraySize = EditorGUI.IntField(rect, items.arraySize);

            position.y += EditorGUIUtility.singleLineHeight;

            EditorGUI.EndFoldoutHeaderGroup();


            if (property.isExpanded) {
                EditorGUI.indentLevel++;
                position = EditorGUI.IndentedRect(position);
                for (int i = 0; i < items.arraySize; i++) {
                    var item = items.GetArrayElementAtIndex(i);
                    var key = item.FindPropertyRelative("key");
                    var value = item.FindPropertyRelative("value");


                    if (isSingleLine) {
                        rect.x = position.x - (EditorGUIUtility.fieldWidth / 2);
                        rect.y = position.y;
                        rect.width = EditorGUIUtility.fieldWidth;
                        rect.height = Mathf.Max(EditorGUI.GetPropertyHeight(key, true), EditorGUI.GetPropertyHeight(value, true));

                        EditorGUI.PrefixLabel(rect, new GUIContent($"{i}"), EditorStyles.miniLabel);
                        rect.x = position.x;
                        rect.width = position.width / 2;
                        EditorGUI.PropertyField(rect, key, GUIContent.none, true);
                        rect.x += position.width / 2;
                        EditorGUI.PropertyField(rect, value, GUIContent.none, true);
                        position.y += rect.height;
                    } else {
                        rect.x = position.x - (EditorGUIUtility.fieldWidth / 2);
                        rect.y = position.y;
                        rect.width = EditorGUIUtility.fieldWidth;
                        rect.height = EditorGUI.GetPropertyHeight(key, true);

                        EditorGUI.PrefixLabel(rect, new GUIContent($"{i}"), EditorStyles.miniLabel);
                        rect.x = position.x;
                        rect.width = position.width;
                        EditorGUI.PropertyField(rect, key, true);
                        position.y += rect.height;
                        rect.y = position.y;
                        rect.height = EditorGUI.GetPropertyHeight(value, true);
                        EditorGUI.PropertyField(rect, value, true);
                        position.y += rect.height;
                        position.y += EditorGUIUtility.standardVerticalSpacing;
                    }
                }
                EditorGUI.indentLevel--;
            }
        }
    }
}