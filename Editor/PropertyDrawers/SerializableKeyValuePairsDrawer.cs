using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Editor {
    [CustomPropertyDrawer(typeof(SerializableKeyValuePairs<,>), true)]
    public class SerializableKeyValuePairsDrawer : PropertyDrawer {
        enum LineMode {
            ShortKeyShortValue,
            ShortKeyLongValue,
            LongKeyShortValue,
            LongKeyLongValue
        }
        protected virtual (Type key, Type value) GetGenericTypes() {
            var types = fieldInfo.FieldType.GetGenericArguments();
            if (types.Length != 2) {
                Debug.LogWarning($"Unable to determine generic types from {fieldInfo}! If you extended {typeof(SerializableKeyValuePairsDrawer)}, you should override {nameof(SerializableKeyValuePairsDrawer)}::{nameof(GetGenericTypes)}!");
            }

            return (types[0], types[1]);
        }

        (Type key, Type value) genericTypes;
        LineMode lineMode;

        bool isSingleLine => IsInlineType(genericTypes.key) || IsInlineType(genericTypes.value);
        bool IsInlineType(Type type) => !(type.IsClass || type.IsInterface);

        void Initialize() {
            genericTypes = GetGenericTypes();
            if (IsInlineType(genericTypes.key)) {
                lineMode = IsInlineType(genericTypes.value)
                    ? LineMode.ShortKeyShortValue
                    : LineMode.ShortKeyLongValue;
            } else {
                lineMode = IsInlineType(genericTypes.value)
                    ? LineMode.LongKeyShortValue
                    : LineMode.LongKeyLongValue;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            Initialize();

            float totalHeight = EditorGUIUtility.standardVerticalSpacing;

            totalHeight += EditorGUIUtility.singleLineHeight;

            if (property.isExpanded) {
                var items = property.FindPropertyRelative("items");
                for (int i = 0; i < items.arraySize; i++) {
                    var item = items.GetArrayElementAtIndex(i);
                    var key = item.FindPropertyRelative("key");
                    var value = item.FindPropertyRelative("value");

                    if (lineMode == LineMode.LongKeyLongValue) {
                        totalHeight += EditorGUI.GetPropertyHeight(key, true);
                        totalHeight += EditorGUI.GetPropertyHeight(value, true);
                        totalHeight += EditorGUIUtility.standardVerticalSpacing;
                    } else {
                        totalHeight += Mathf.Max(EditorGUI.GetPropertyHeight(key, true), EditorGUI.GetPropertyHeight(value, true));
                    }
                }
            }

            return totalHeight;
        }

        static readonly Dictionary<LineMode, (float key, float value)> lineSettings = new() {
            [LineMode.ShortKeyShortValue] = (0.5f, 0.5f),
            [LineMode.ShortKeyLongValue] = (0.3f, 0.7f),
            [LineMode.LongKeyShortValue] = (0.7f, 0.3f),
            [LineMode.LongKeyLongValue] = (0.5f, 0.5f),
        };

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            Initialize();

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

                    rect.x = position.x - EditorGUIUtility.fieldWidth;
                    rect.y = position.y;
                    rect.width = EditorGUIUtility.fieldWidth;
                    rect.height = EditorGUIUtility.singleLineHeight;
                    EditorGUI.LabelField(rect, new GUIContent($"#{i}"), new GUIStyle(EditorStyles.miniLabel) { alignment = TextAnchor.MiddleRight });

                    if (lineMode == LineMode.LongKeyLongValue) {
                        rect.x = position.x;
                        rect.width = position.width;
                        rect.height = EditorGUI.GetPropertyHeight(key, true);
                        EditorGUI.PropertyField(rect, key, true);
                        position.y += rect.height;
                        rect.y = position.y;
                        rect.height = EditorGUI.GetPropertyHeight(value, true);
                        EditorGUI.PropertyField(rect, value, true);
                        position.y += rect.height;
                        position.y += EditorGUIUtility.standardVerticalSpacing;
                    } else {
                        var ratio = lineSettings[lineMode];
                        rect.x = position.x;
                        rect.width = position.width * ratio.key;
                        rect.height = Mathf.Max(EditorGUI.GetPropertyHeight(key, true), EditorGUI.GetPropertyHeight(value, true));
                        EditorGUI.PropertyField(rect, key, GUIContent.none, true);
                        rect.x += rect.width;
                        rect.width = position.width * ratio.value;
                        EditorGUI.PropertyField(rect, value, GUIContent.none, true);
                        position.y += rect.height;
                    }
                }

                EditorGUI.indentLevel--;
            }
        }
    }
}