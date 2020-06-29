using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditorInternal;

namespace Slothsoft.UnityExtensions.Editor {
    public class CSharpFileFixer : UnityEditor.AssetModificationProcessor {
        const string META_EXTENSION = ".meta";
        const string CSHARP_EXTENSION = ".cs";

        static readonly Regex invalidTypeCharacters = new Regex(@"[^\w.]+");

        public static void OnWillCreateAsset(string path) {
            if (!UnityExtensionsSettings.instance.addNamespaceToCSharpFiles) {
                return;
            }
            path = AssetDatabase.GetAssetPathFromTextMetaFilePath(path);
            var file = new FileInfo(path);
            if (file.Extension == CSHARP_EXTENSION) {
                var script = AssetDatabase.LoadAssetAtPath<MonoScript>(path);
                var assembly = GetAssembly(script, path);
                if (assembly) {
                    string contents = File.ReadAllText(file.FullName);
                    if (!contents.Contains("namespace ") && contents.Contains("public class ")) {
                        contents = contents.Replace("public class ", $"namespace {GetNamespace(assembly, path)} {{\r\npublic class ") + "}";
                        File.WriteAllText(file.FullName, contents);
                        AssetDatabase.Refresh();
                    }
                }
            }
        }
        static AssemblyDefinitionAsset GetAssembly(MonoScript script, string assetPath = null) {
            var directory = new FileInfo(assetPath ?? AssetDatabase.GetAssetPath(script)).Directory;
            return PrefabUtils.LoadAssets<AssemblyDefinitionAsset>()
                .Select(assembly => new { assembly, directory = new FileInfo(AssetDatabase.GetAssetPath(assembly)).Directory })
                .Where(info => directory.FullName.Contains(info.directory.FullName))
                .OrderByDescending(info => info.directory.FullName.Length)
                .Select(info => info.assembly)
                .FirstOrDefault();
        }
        static string GetNamespace(AssemblyDefinitionAsset assembly) {
            return CleanNamespace(assembly.name.Replace(".Runtime", ""));
        }
        static string GetNamespace(AssemblyDefinitionAsset assembly, string assetPath) {
            string assemblyPath = new FileInfo(AssetDatabase.GetAssetPath(assembly)).Directory.FullName;
            string scriptPath = new FileInfo(assetPath).Directory.FullName;
            string scriptNamespace = scriptPath
                .Substring(assemblyPath.Length)
                .Replace(Path.DirectorySeparatorChar, '.');
            return CleanNamespace(GetNamespace(assembly) + scriptNamespace);
        }
        static string GetNamespace(AssemblyDefinitionAsset assembly, MonoScript script) {
            return GetNamespace(assembly, AssetDatabase.GetAssetPath(script));
        }
        static string CleanNamespace(string ns) {
            return invalidTypeCharacters.Replace(ns, "").Trim();
        }
    }
}