using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Editor {
    [CreateAssetMenu(menuName = "Slothsoft/Unity Extension Settings", fileName = "Slothsoft.UnityExtensionsSettings.asset")]
    internal class UnityExtensionsSettings : ScriptableObject {
        internal static UnityExtensionsSettings instance {
            get {
                if (instanceCache == null) {
                    instanceCache = Resources.LoadAll<UnityExtensionsSettings>("").FirstOrDefault();
                }
                return instanceCache;
            }
        }
        internal static UnityExtensionsSettings instanceCache;

        [SerializeField, Tooltip("Use the following options to change the style of the [Expandable] ScriptableObject drawers")]
        public ExpandableSettings expandableSettings = new ExpandableSettings();
    }
}