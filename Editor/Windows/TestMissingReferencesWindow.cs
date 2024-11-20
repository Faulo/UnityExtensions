using UnityEditor;
using UnityObject = UnityEngine.Object;

namespace Slothsoft.UnityExtensions.Editor {
    sealed class TestMissingReferencesWindow : EditorWindow {
        sealed class AssetInfo {
            public string path;
            public UnityObject asset;
            public bool isValid;
        }
        [MenuItem("Window/Slothsoft's Unity Extensions/Find Missing References")]
        static void ShowWindow() {
            GetWindow<TestMissingReferencesWindow>();
        }

        UnityObject asset;
        string path;

        void OnGUI() {
            asset = EditorGUILayout.ObjectField("Asset", asset, typeof(UnityObject), true);

            if (asset) {
                path = AssetDatabase.GetAssetPath(asset);
                if (!string.IsNullOrEmpty(path)) {
                    string[] dependencies = AssetDatabase.GetDependencies(path, true);
                    foreach (string dependency in dependencies) {
                        EditorGUILayout.LabelField(dependency);
                    }
                }
            }
        }
    }
}
