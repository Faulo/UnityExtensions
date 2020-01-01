using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Editor {
    [CreateAssetMenu(menuName = "Slothsoft/Unity Extension Settings", fileName = "Slothsoft.UnityExtensionsSettings.asset")]
    internal class UnityExtensionsSettings : ScriptableObject {
        internal static UnityExtensionsSettings instance => Resources.LoadAll<UnityExtensionsSettings>("").FirstOrDefault();
        [SerializeField, Tooltip("Use the following options to change the style of the [Expandable] ScriptableObject drawers")]
        public ExpandableSettings expandableSettings = new ExpandableSettings();
    }
}