using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Editor {
    public class PrefabUtils {
        static PrefabUtilsSettings settings => UnityExtensionsSettings.instance.prefabUtilsSettings;

        public static IEnumerable<GameObject> allPrefabs => AssetDatabase.GetAllAssetPaths()
            .Where(path => path.StartsWith(settings.assetsFolder))
            .Where(path => path.EndsWith(settings.prefabExtension))
            .Select(AssetDatabase.LoadMainAssetAtPath)
            .OfType<GameObject>();

        public static T LoadAssetAtPath<T>(FileSystemInfo file) where T : Object {
            string path = file.FullName;
            string root = new DirectoryInfo(".").FullName;
            if (path.StartsWith(root)) {
                path = path.Substring(root.Length + 1);
            }
            return AssetDatabase.LoadAssetAtPath<T>(path);
        }

        public static IEnumerable<T> LoadAssets<T>(string searchFolder = "Assets") where T : Object {
            return AssetDatabase.FindAssets($"t:{typeof(T).Name}", new[] { searchFolder })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<T>);
        }


        static readonly Regex invalidTypeCharacters = new Regex(@"[^\w.]+");

        public static AssemblyDefinitionAsset GetAssembly(MonoScript script, string assetPath = null) {
            var directory = new FileInfo(assetPath ?? AssetDatabase.GetAssetPath(script)).Directory;
            return PrefabUtils.LoadAssets<AssemblyDefinitionAsset>()
                .Select(assembly => new { assembly, directory = new FileInfo(AssetDatabase.GetAssetPath(assembly)).Directory })
                .Where(info => directory.FullName.Contains(info.directory.FullName))
                .OrderByDescending(info => info.directory.FullName.Length)
                .Select(info => info.assembly)
                .FirstOrDefault();
        }
        public static string GetNamespace(AssemblyDefinitionAsset assembly) {
            return CleanNamespace(assembly.name.Replace(".Runtime", ""));
        }
        public static string GetNamespace(AssemblyDefinitionAsset assembly, string assetPath) {
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