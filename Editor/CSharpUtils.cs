using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Slothsoft.UnityExtensions.Editor.PackageSettings;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Editor {
    public static class CSharpUtils {
        static readonly Regex invalidTypeCharacters = new(@"[^\w.]+");
        static bool stripRuntimeFromNamespace => UnityExtensionsSettings.instance.cSharpSettings.stripRuntimeFromNamespace;

        public static AssemblyDefinitionAsset GetAssembly(MonoScript script)
            => GetAssembly(AssetDatabase.GetAssetPath(script));
        public static AssemblyDefinitionAsset GetAssembly(string assetPath)
            => GetAssembly(new FileInfo(assetPath));
        public static AssemblyDefinitionAsset GetAssembly(FileInfo scriptFile)
            => GetAssembly(scriptFile.Directory);
        public static AssemblyDefinitionAsset GetAssembly(DirectoryInfo directory) {
            return AssetUtils.LoadAssetsOfType<AssemblyDefinitionAsset>()
                .Select(assembly => new { assembly, directory = new FileInfo(AssetDatabase.GetAssetPath(assembly)).Directory })
                .Where(info => directory.FullName.Contains(info.directory.FullName))
                .OrderByDescending(info => info.directory.FullName.Length)
                .Select(info => info.assembly)
                .FirstOrDefault();
        }
        public static string GetNamespace(AssemblyDefinitionAsset assembly) {
            string ns = assembly.name;
            if (stripRuntimeFromNamespace) {
                ns = assembly.name.Replace(".Runtime", "");
            }

            return CleanNamespace(ns);
        }
        public static string GetNamespace(AssemblyDefinitionAsset assembly, string assetPath) {
            string assemblyPath = new FileInfo(AssetDatabase.GetAssetPath(assembly)).Directory.FullName;
            string scriptPath = new FileInfo(assetPath).Directory.FullName;
            string scriptNamespace = scriptPath
[assemblyPath.Length..]
                .Replace(Path.DirectorySeparatorChar, '.');
            return CleanNamespace(GetNamespace(assembly) + scriptNamespace);
        }
        static string CleanNamespace(string ns) {
            return string.Join(".", invalidTypeCharacters.Replace(ns, "").Split('.').Where(segment => !string.IsNullOrEmpty(segment)));
        }

        const string EXTENSION_CSHARP = ".cs";
        const string PLACEHOLDER_NAMESPACE = "#NAMESPACE#";

        public static bool TryReplacePlaceholders(string path) {
            try {
                var file = new FileInfo(path);

                if (!file.Exists) {
                    return false;
                }

                if (file.Extension != EXTENSION_CSHARP) {
                    return false;
                }

                string contents = File.ReadAllText(file.FullName);
                if (!contents.Contains(PLACEHOLDER_NAMESPACE)) {
                    return false;
                }

                var assembly = GetAssembly(file);
                string ns = assembly
                    ? GetNamespace(assembly, path)
                    : "AssemblyCSharp";
                contents = contents.Replace(PLACEHOLDER_NAMESPACE, ns);
                File.WriteAllText(file.FullName, contents);
                return true;
            } catch (Exception e) {
                Debug.LogException(e);
                return false;
            }
        }
    }
}