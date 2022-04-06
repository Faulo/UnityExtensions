using UnityEditor;
using UnityEditor.SettingsManagement;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Slothsoft.UnityExtensions.Editor.PackageSettings {
    class UnityExtensionsSettings {
        const string SETTINGS_PACKAGE = "net.slothsoft.unity-extensions";
        const string SETTINGS_MENU = "Project/Slothsoft's Unity Extensions";
        const string SETTINGS_TEMPLATE = "Packages/net.slothsoft.unity-extensions/Editor/Templates/Settings.uxml";

        class SettingsObject : ScriptableObject {
            [Space]
            [SerializeField, Tooltip("Use the following options to change the style of the [Expandable] ScriptableObject drawers")]
            ExpandableSettings expandableSettings;

            [Space]
            [SerializeField, Tooltip("Use the following options to change render pipeline conversion")]
            RenderPipelineConversionSettings renderPipelineConversionSettings;

            [Space]
            [SerializeField, Tooltip("Use the following options to locate prefabs.")]
            PrefabUtilsSettings prefabUtilsSettings;

            [Space]
            [SerializeField, Tooltip("Use the following options to change how C# files are generated")]
            CSharpSettings cSharpSettings;

            void Load() {
                expandableSettings = m_expandableSettings.value;
                renderPipelineConversionSettings = m_renderPipelineConversionSettings.value;
                prefabUtilsSettings = m_prefabUtilsSettings.value;
                cSharpSettings = m_cSharpSettings.value;
            }
            void Save() {
                m_expandableSettings.ApplyModifiedProperties();
                m_renderPipelineConversionSettings.ApplyModifiedProperties();
                m_prefabUtilsSettings.ApplyModifiedProperties();
                m_cSharpSettings.ApplyModifiedProperties();
            }

            static SettingsObject instance;
            [SettingsProvider]
            protected static SettingsProvider CreateSettingsProvider() => new SettingsProvider(SETTINGS_MENU, SettingsScope.Project) {
                activateHandler = (searchContext, rootElement) => {
                    if (!instance) {
                        instance = CreateInstance<SettingsObject>();
                        instance.Load();
                    }
                    var template = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(SETTINGS_TEMPLATE);
                    template.CloneTree(rootElement);
                    rootElement.Bind(new SerializedObject(instance));
                },
                inspectorUpdateHandler = () => {
                    if (instance) {
                        instance.Save();
                    }
                },
            };
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

        [UserSetting]
        static UserSetting<ExpandableSettings> m_expandableSettings = new UserSetting<ExpandableSettings>(settings, nameof(m_expandableSettings), new ExpandableSettings());
        internal ExpandableSettings expandableSettings => m_expandableSettings.value;

        [UserSetting]
        static UserSetting<RenderPipelineConversionSettings> m_renderPipelineConversionSettings = new UserSetting<RenderPipelineConversionSettings>(settings, nameof(m_expandableSettings), new RenderPipelineConversionSettings());
        internal RenderPipelineConversionSettings renderPipelineConversionSettings => m_renderPipelineConversionSettings.value;

        [UserSetting]
        static UserSetting<PrefabUtilsSettings> m_prefabUtilsSettings = new UserSetting<PrefabUtilsSettings>(settings, nameof(m_expandableSettings), new PrefabUtilsSettings());
        internal PrefabUtilsSettings prefabUtilsSettings => m_prefabUtilsSettings.value;

        [UserSetting]
        static UserSetting<CSharpSettings> m_cSharpSettings = new UserSetting<CSharpSettings>(settings, nameof(m_expandableSettings), new CSharpSettings());
        internal CSharpSettings cSharpSettings => m_cSharpSettings.value;
    }
}