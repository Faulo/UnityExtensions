using UnityEditor;
using UnityEditor.SettingsManagement;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Slothsoft.UnityExtensions.Editor {
    sealed class UnityExtensionsSettings {
        const string SETTINGS_PACKAGE = "net.slothsoft.unity-extensions";
        const string SETTINGS_MENU = "Project/Slothsoft's Unity Extensions";
        const string SETTINGS_TEMPLATE = "Packages/net.slothsoft.unity-extensions/Editor/Templates/Settings.uxml";

        sealed class SettingsObject : ScriptableObject {
            [Space]
            [SerializeField, Tooltip("Use the following options to change the style of the [Expandable] ScriptableObject drawers")]
            ExpandableSettings expandableSettings;

            [Space]
            [SerializeField, Tooltip("Use the following options to locate prefabs.")]
            PrefabUtilsSettings prefabUtilsSettings;

            [Space]
            [SerializeField, Tooltip("Use the following options to change how C# files are generated")]
            CSharpSettings cSharpSettings;

            [Space]
            [SerializeField]
            AndroidSettings androidSettings;

            void Load() {
                expandableSettings = m_expandableSettings.value;
                prefabUtilsSettings = m_prefabUtilsSettings.value;
                cSharpSettings = m_cSharpSettings.value;
                androidSettings = m_androidSettings.value;
            }
            void Save() {
                m_expandableSettings.ApplyModifiedProperties();
                m_prefabUtilsSettings.ApplyModifiedProperties();
                m_cSharpSettings.ApplyModifiedProperties();
                m_androidSettings.ApplyModifiedProperties();
            }

            static SettingsObject instance;
            [SettingsProvider]
            static SettingsProvider CreateSettingsProvider() => new(SETTINGS_MENU, SettingsScope.Project) {
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
                settingsCache ??= new Settings(SETTINGS_PACKAGE);

                return settingsCache;
            }
        }
        static Settings settingsCache;

        internal static UnityExtensionsSettings instance {
            get {
                instanceCache ??= settings.Get(nameof(instance), SettingsScope.Project, new UnityExtensionsSettings());

                return instanceCache;
            }
        }
        static UnityExtensionsSettings instanceCache;

        [UserSetting]
        static UserSetting<ExpandableSettings> m_expandableSettings = new(settings, nameof(m_expandableSettings), new ExpandableSettings());
        internal ExpandableSettings expandableSettings => m_expandableSettings.value;

        [UserSetting]
        static UserSetting<PrefabUtilsSettings> m_prefabUtilsSettings = new(settings, nameof(m_prefabUtilsSettings), new PrefabUtilsSettings());
        internal PrefabUtilsSettings prefabUtilsSettings => m_prefabUtilsSettings.value;

        [UserSetting]
        static UserSetting<CSharpSettings> m_cSharpSettings = new(settings, nameof(m_cSharpSettings), new CSharpSettings());
        internal CSharpSettings cSharpSettings => m_cSharpSettings.value;

        [UserSetting]
        static UserSetting<AndroidSettings> m_androidSettings = new(settings, nameof(m_androidSettings), new AndroidSettings());
        internal AndroidSettings androidSettings => m_androidSettings.value;
    }
}