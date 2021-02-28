using System.IO;
using Slothsoft.UnityExtensions.Editor.PackageSettings;
using UnityEditor;

namespace Slothsoft.UnityExtensions.Editor {
    public class CSharpFileFixer : UnityEditor.AssetModificationProcessor {
        const string META_EXTENSION = ".meta";
        const string CSHARP_EXTENSION = ".cs";

        public static void OnWillCreateAsset(string path) {
            if (!UnityExtensionsSettings.instance.addNamespaceToCSharpFiles) {
                return;
            }
            path = AssetDatabase.GetAssetPathFromTextMetaFilePath(path);
            var file = new FileInfo(path);
            if (file.Extension == CSHARP_EXTENSION) {
                var script = AssetDatabase.LoadAssetAtPath<MonoScript>(path);
                var assembly = PrefabUtils.GetAssembly(script, path);
                if (assembly) {
                    string contents = File.ReadAllText(file.FullName);
                    if (!contents.Contains("namespace ") && contents.Contains("public class ")) {
                        contents = contents.Replace("public class ", $"namespace {PrefabUtils.GetNamespace(assembly, path)} {{\r\npublic class ") + "}";
                        File.WriteAllText(file.FullName, contents);
                        AssetDatabase.Refresh();
                    }
                }
            }
        }
    }
}