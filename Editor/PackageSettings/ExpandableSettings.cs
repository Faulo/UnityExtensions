using System;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Editor.PackageSettings {
    [Serializable]
    class ExpandableSettings {
        [Header("Additional Rows")]
        [SerializeField, Tooltip("Whether the default editor Script field should be shown.")]
        internal bool showSourceFile = true;

        [Header("Layout")]
        [SerializeField, Tooltip("The spacing on the inside of the background rect."), Range(0, 10)]
        internal int innerSpacing = 4;

        [SerializeField, Tooltip("The spacing on the outside of the background rect."), Range(0, 10)]
        internal int outerSpacing = 2;

        internal int totalSpacing => innerSpacing + outerSpacing;

        [Header("Background")]
        [SerializeField, Tooltip("The style the background uses.")]
        internal ExpandableBackgroundStyle backgroundStyle = ExpandableBackgroundStyle.HelpBox;

        [SerializeField, Tooltip("The colour that is used to darken the background.")]
        internal Color darkenColor = new Color(0, 0, 0, 0.2f);

        [SerializeField, Tooltip("The colour that is used to lighten the background.")]
        internal Color lightenColor = new Color(1, 1, 1, 0.2f);
    }
}