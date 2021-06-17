using System.Linq;
using UnityEditor;
using UnityEditor.SettingsManagement;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Slothsoft.UnityExtensions.Editor.PackageSettings {
    class UnityExtensionsSettings {
        const string SETTINGS_PACKAGE = "net.slothsoft.unity-extensions";
        const string SETTINGS_MENU = "Project/Slothsoft's Unity Extensions";
        const string SETTINGS_TEMPLATE = "Packages/net.slothsoft.unity-extensions/Templates/Settings.uxml";

        class SettingsObject : ScriptableObject {
            [SerializeField, Tooltip("Use the following options to change the style of the [Expandable] ScriptableObject drawers")]
            public ExpandableSettings expandableSettings;

            public void Load() {
                expandableSettings = m_expandableSettings.value;
            }
            public void Save() {
                m_expandableSettings.ApplyModifiedProperties();
            }

            static SettingsObject instance;
            [SettingsProvider]
            public static SettingsProvider CreateSettingsProvider() => new SettingsProvider(SETTINGS_MENU, SettingsScope.Project) {
                activateHandler = (searchContext, rootElement) => {
                    if (!instance) {
                        instance = CreateInstance<SettingsObject>();
                        instance.Load();
                    }
                    var template = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(SETTINGS_TEMPLATE);
                    template.CloneTree(rootElement);
                    rootElement.Bind(new SerializedObject(instance));
                }
            };
        }

        internal static Settings settings {
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

        [UserSetting]
        static UserSetting<ExpandableSettings> m_expandableSettings = new UserSetting<ExpandableSettings>(settings, nameof(m_expandableSettings), new ExpandableSettings());
        internal ExpandableSettings expandableSettings => m_expandableSettings.value;

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