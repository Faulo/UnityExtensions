using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Internal {
    [Serializable]
    internal class ExpandableSettings {
        [Header("Additional Rows")]
        [SerializeField, Tooltip("Whether the default editor Script field should be shown.")]
        public bool showSourceFile = true;

        [Header("Layout")]
        [SerializeField, Tooltip("The spacing on the inside of the background rect."), Range(0, 10)]
        public int innerSpacing = 4;

        [SerializeField, Tooltip("The spacing on the outside of the background rect."), Range(0, 10)]
        public int outerSpacing = 2;

        public int totalSpacing => innerSpacing + outerSpacing;

        [Header("Background")]
        [SerializeField, Tooltip("The style the background uses.")]
        public ExpandableBackgroundStyle backgroundStyle = ExpandableBackgroundStyle.HelpBox;

        [SerializeField, Tooltip("The colour that is used to darken the background.")]
        public Color darkenColor = new Color(0, 0, 0, 0.2f);

        [SerializeField, Tooltip("The colour that is used to lighten the background.")]
        public Color lightenColor = new Color(1, 1, 1, 0.2f);
    }
}