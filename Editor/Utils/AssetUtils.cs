using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityObject = UnityEngine.Object;

namespace Slothsoft.UnityExtensions.Editor {
    public static class AssetUtils {
        /// <summary>
        /// Attempts to load an asset by its <see cref="FileSystemInfo"/> location.
        /// </summary>
        /// <typeparam name="T">The type of the asset to load.</typeparam>
        /// <param name="file">The location of the asset. Can be a <see cref="FileInfo"/> or a <see cref="DirectoryInfo"/>.</param>
        /// <returns>The asset if it exists, null otherwise.</returns>
        public static T LoadAssetAtFile<T>(FileSystemInfo file) where T : UnityObject {
            var asset = AssetDatabase.LoadAssetAtPath<T>(file.ToString());
            if (!asset) {
                string path = file.FullName;
                string root = new DirectoryInfo(".").FullName;
                if (path.StartsWith(root)) {
                    path = path[(root.Length + 1)..];
                }

                asset = AssetDatabase.LoadAssetAtPath<T>(path);
            }

            return asset;
        }

        /// <summary>
        /// Loads all assets of type <typeparamref name="T"/> that reside in the folder(s) <paramref name="searchFolders"/>.
        /// </summary>
        /// <typeparam name="T">The type of asset to load.</typeparam>
        /// <param name="searchFolders">The folder to search in. Should be a Unity asset path, e.g. 'Assets/Prefabs' or 'Packages/package-id'.</param>
        /// <returns>All assets matching the above criteria.</returns>
        public static IEnumerable<T> LoadAssetsOfType<T>(params string[] searchFolders) where T : UnityObject {
            var assets = AssetDatabase
                .FindAssets($"t:{typeof(T).Name}")
                .Select(AssetDatabase.GUIDToAssetPath);

            // The 2nd parameter of FindAssets is buggy for the 'Packages' folder, so we have to do the filtering ourselves (https://forum.unity.com/threads/assetdatabase-findassets-not-working-for-packages.539121/).
            if (searchFolders.Length != 0) {
                assets = assets.Where(path => {
                    for (int i = 0; i < searchFolders.Length; i++) {
                        if (path.StartsWith(searchFolders[i])) {
                            return true;
                        }
                    }

                    return false;
                });
            }

            return assets
                .Select(AssetDatabase.LoadAssetAtPath<T>)
                .Where(asset => asset);
        }
    }
}