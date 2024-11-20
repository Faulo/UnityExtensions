using UnityEditor;
using UnityEditor.AssetImporters;
using UnityObject = UnityEngine.Object;

namespace Slothsoft.UnityExtensions.Editor {
    public static class AssetImportContextExtensions {
        public static bool TryDependOnArtifact<T>(this AssetImportContext context, string path, out T artifact)
            where T : UnityObject {
            if (string.IsNullOrEmpty(path)) {
                artifact = default;
                return false;
            }

            context.DependsOnArtifact(path);
            artifact = AssetDatabase.LoadAssetAtPath<T>(path);
            return artifact;
        }
    }
}
