using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Slothsoft.UnityExtensions.Editor.PackageSettings {
    public class UnityExtensionsSettingsProvider : SettingsProvider {
        const string SETTINGS_MENU = "Project/Slothsoft's Unity Extensions";

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider() {
            return new UnityExtensionsSettingsProvider(SETTINGS_MENU);
        }

        public UnityExtensionsSettingsProvider(string path) : base(path, SettingsScope.Project) {
        }

        public override void OnActivate(string searchContext, VisualElement rootElement) {
            base.OnActivate(searchContext, rootElement);
            rootElement.Add(new InspectorElement(UnityExtensionsSettings.instance));
        }
    }
}