using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
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
    }
}