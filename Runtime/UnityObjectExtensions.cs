using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace CursedBroom.Core.Extensions {
    public static class UnityObjectExtensions {
#if UNITY_EDITOR
        public static void RenameTo(this UnityObject obj, string newName) {
            if (UnityEditor.AssetDatabase.IsSubAsset(obj)) {
                obj.name = newName;
                UnityEditor.EditorUtility.SetDirty(obj);
            } else {
                string oldPath = UnityEditor.AssetDatabase.GetAssetPath(obj);
                UnityEditor.AssetDatabase.RenameAsset(oldPath, newName);
            }
        }
#endif
        public static void SmartDestroy(this UnityObject obj) {
            if (obj) {
                if (Application.isPlaying) {
                    UnityObject.Destroy(obj);
                } else {
                    UnityObject.DestroyImmediate(obj);
                }
            }
        }
    }
}