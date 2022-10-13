using System.IO;
using Slothsoft.UnityExtensions.Editor.PackageSettings;
using UnityEditor;

namespace Slothsoft.UnityExtensions.Editor {
    sealed class CSharpFileFixer : UnityEditor.AssetModificationProcessor {
        const string EXTENSION_CSHARP = ".cs";
        const string PLACEHOLDER_NAMESPACE = "#NAMESPACE#";

        static void OnWillCreateAsset(string path) {
            if (!UnityExtensionsSettings.instance.cSharpSettings.addNamespaceToCSharpFiles) {
                return;
            }

            path = AssetDatabase.GetAssetPathFromTextMetaFilePath(path);
            var file = new FileInfo(path);

            if (file.Extension == EXTENSION_CSHARP) {
                var script = AssetDatabase.LoadAssetAtPath<MonoScript>(path);
                var assembly = PrefabUtils.GetAssembly(script, path);
                if (assembly) {
                    string contents = File.ReadAllText(file.FullName);
                    if (contents.Contains(PLACEHOLDER_NAMESPACE)) {
                        contents = contents.Replace(PLACEHOLDER_NAMESPACE, PrefabUtils.GetNamespace(assembly, path));
                        File.WriteAllText(file.FullName, contents);
                        AssetDatabase.Refresh();
                    }
                }
            }
        }
    }
}