using System;
using System.Linq;
using UnityEditor;
using UnityEditor.SettingsManagement;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Editor.PackageSettings {
    [Serializable]
    class UnityExtensionsSettings {
        const string SETTINGS_PACKAGE = "net.slothsoft.unity-extensions";
        const string SETTINGS_MENU = "Project/Slothsoft's Unity Extensions";
        const string SETTINGS_PATH = "ProjectSettings/UnityExtensionsSettings.asset";

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider() {
            var provider = new UserSettingsProvider(
                SETTINGS_MENU,
                settings,
                new[] { typeof(UnityExtensionsSettings).Assembly },
                SettingsScope.Project
            );
            return provider;
        }

        static Settings settings {
            get {
                if (settingsCache == null) {
                    settingsCache = new Settings(SETTINGS_PACKAGE);
                }
                return settingsCache;
            }
        }
        static Settings settingsCache;

        internal static UnityExtensionsSettings instance {
            get {
                if (instanceCache == null) {
                    instanceCache = settings.Get(nameof(instance), SettingsScope.Project, new UnityExtensionsSettings());
                }
                return instanceCache;
            }
        }
        static UnityExtensionsSettings instanceCache;

        [UserSetting("[Expandable] Settings", "[Expandable] Settings")]
        static UserSetting<int[]> m_expandableSettings = new UserSetting<int[]>(settings, nameof(m_expandableSettings), default);

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