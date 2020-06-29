using System.Linq;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Editor {
    [CreateAssetMenu(menuName = "Slothsoft/Unity Extension Settings", fileName = "Slothsoft.UnityExtensionsSettings.asset")]
    class UnityExtensionsSettings : ScriptableObject {
        internal static UnityExtensionsSettings instance {
            get {
                if (instanceCache == null) {
                    instanceCache = Resources.LoadAll<UnityExtensionsSettings>("").FirstOrDefault();
                }
                return instanceCache
                    ? instanceCache
                    : CreateInstance<UnityExtensionsSettings>();
            }
        }
        internal static UnityExtensionsSettings instanceCache;

        [SerializeField, Tooltip("Use the following options to change the style of the [Expandable] ScriptableObject drawers")]
        internal ExpandableSettings expandableSettings = new ExpandableSettings();

        [SerializeField, Tooltip("Use the following options to change render pipeline conversion")]
        internal RenderPipelineConversionSettings renderPipelineConversionSettings = new RenderPipelineConversionSettings();

        [SerializeField, Tooltip("Use the following options to locate prefabs.")]
        internal PrefabUtilsSettings prefabUtilsSettings = new PrefabUtilsSettings();

        [SerializeField, Tooltip("Use the following options to adjust .csproj file generation.")]
        internal ProjectFileSettings[] projectFileSettings = new ProjectFileSettings[0];
        internal bool projectFileSettingsEnabled => projectFileSettings.Length > 0;
        internal ProjectFileSettings ProjectFileSettingsForAssembly(string assemblyName) {
            return projectFileSettings
                .FirstOrDefault(settings => settings == null ? false : settings.Matches(assemblyName));
        }

        [SerializeField, Tooltip("Add C# namespace based on assembly and folder hierarchy to every new .cs file.")]
        internal bool addNamespaceToCSharpFiles = false;
    }
}