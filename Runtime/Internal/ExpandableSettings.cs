using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Internal {
    [Serializable]
    public class ExpandableSettings {
        [SerializeField, Tooltip("Whether the default editor Script field should be shown.")]
        public bool showScriptRow = true;

        [SerializeField, Tooltip("The spacing on the inside of the background rect."), Range(0, 10)]
        public int innerSpacing = 4;

        [SerializeField, Tooltip("The spacing on the outside of the background rect."), Range(0, 10)]
        public int outerSpacing = 2;
        public int totalSpacing => innerSpacing + outerSpacing;
    }
}