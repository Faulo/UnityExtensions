using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Slothsoft.UnityExtensions.Editor.PackageSettings;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Slothsoft.UnityExtensions.Editor {
    public static class PrefabUtils {
        static PrefabUtilsSettings settings => UnityExtensionsSettings.instance.prefabUtilsSettings;

        public static IEnumerable<GameObject> allPrefabs => AssetDatabase.GetAllAssetPaths()
            .Where(path => path.StartsWith(settings.assetsFolder))
            .Where(path => path.EndsWith(settings.prefabExtension))
            .Select(AssetDatabase.LoadMainAssetAtPath)
            .OfType<GameObject>();

        [Obsolete("Use 'AssetUtils.LoadAssetAtPath' instead.")]
        public static T LoadAssetAtPath<T>(FileSystemInfo file) where T : UnityObject
            => AssetUtils.LoadAssetAtFile<T>(file);

        [Obsolete("Use 'AssetUtils.LoadAssets' instead.")]
        public static IEnumerable<T> LoadAssets<T>(string searchFolder = "Assets") where T : UnityObject
            => AssetUtils.LoadAssetsOfType<T>(searchFolder);

        [Obsolete("Use 'CSharpUtils.GetAssembly' instead.")]
        public static AssemblyDefinitionAsset GetAssembly(MonoScript script, string assetPath = null)
            => string.IsNullOrEmpty(assetPath)
                ? CSharpUtils.GetAssembly(script)
                : CSharpUtils.GetAssembly(assetPath);

        [Obsolete("Use 'CSharpUtils.GetNamespace' instead.")]
        public static string GetNamespace(AssemblyDefinitionAsset assembly)
            => CSharpUtils.GetNamespace(assembly);

        [Obsolete("Use 'CSharpUtils.GetNamespace' instead.")]
        public static string GetNamespace(AssemblyDefinitionAsset assembly, string assetPath)
            => CSharpUtils.GetNamespace(assembly, assetPath);
    }
}