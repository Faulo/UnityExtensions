using UnityEditor;

namespace Slothsoft.UnityExtensions.Internal {
    internal class UnityExtensionsSettingsMenu {
        [MenuItem("Edit/Custom Project Settings/Slothsoft.UnityExtensions")]
        public static void ShowInspector() {
            UnityExtensionsSettings.Select();
        }
    }
}