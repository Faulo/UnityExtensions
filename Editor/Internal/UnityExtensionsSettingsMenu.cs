using UnityEditor;

namespace Slothsoft.UnityExtensions.Internal {
    public class UnityExtensionsSettingsMenu {
        [MenuItem("Edit/Custom Project Settings/Slothsoft.UnityExtensions")]
        public static void ShowInspector() {
            UnityExtensionsSettings.Select();
        }
    }
}