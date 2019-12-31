using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomProjectSettings;

namespace Slothsoft.UnityExtensions.Internal {
    public class UnityExtensionsSettings : CustomSettings<UnityExtensionsSettings> {
        public override void OnWillSave() { }
        protected override void OnInitialise() { }

        [SerializeField, Tooltip("Use the following options to change the style of the [Expandable] ScriptableObject drawers")]
        public ExpandableSettings expandableSettings = new ExpandableSettings();
    }
}