using System.Linq;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Editor {
    [CreateAssetMenu(menuName = "Slothsoft/Unity Extension Settings", fileName = "Slothsoft.UnityExtensionsSettings.asset")]
    internal class UnityExtensionsSettings : ScriptableObject {
        internal static UnityExtensionsSettings instance {
            get {
                if (instanceCache == null) {
                    instanceCache = Resources.LoadAll<UnityExtensionsSettings>("").FirstOrDefault();
                }
                return instanceCache ?? CreateInstance<UnityExtensionsSettings>();
            }
        }
        internal static UnityExtensionsSettings instanceCache;

        [SerializeField, Tooltip("Use the following options to change the style of the [Expandable] ScriptableObject drawers")]
        internal ExpandableSettings expandableSettings = new ExpandableSettings();

        [SerializeField, Tooltip("Use the following options to change render pipeline conversion")]
        internal RenderPipelineConversionSettings renderPipelineConversionSettings = new RenderPipelineConversionSettings();

        [SerializeField, Tooltip("Use the following options to locate prefabs.")]
        internal PrefabUtilsSettings prefabUtilsSettings = new PrefabUtilsSettings();
    }
}