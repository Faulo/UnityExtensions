using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Editor.PackageSettings {
    class UnityExtensionsSettings : ScriptableObject {
        const string SETTINGS_PATH = "Assets/Plugins/Slothsoft/UnityExtensionsSettings.asset";

        internal static UnityExtensionsSettings instance {
            get {
                if (!instanceCache && File.Exists(SETTINGS_PATH)) {
                    instanceCache = AssetDatabase.LoadAssetAtPath<UnityExtensionsSettings>(SETTINGS_PATH);
                }
                if (!instanceCache) {
                    instanceCache = CreateInstance<UnityExtensionsSettings>();
                    var directory = new FileInfo(SETTINGS_PATH).Directory;
                    if (!directory.Exists) {
                        directory.Create();
                    }
                    AssetDatabase.CreateAsset(instanceCache, SETTINGS_PATH);
                    AssetDatabase.Refresh();
                }
                return instanceCache;
            }
        }
        internal static UnityExtensionsSettings instanceCache;
        [Header("Slothsoft's Unity Extensions Settings")]
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