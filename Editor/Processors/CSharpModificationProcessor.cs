using UnityEditor;

namespace Slothsoft.UnityExtensions.Editor {
    sealed class CSharpModificationProcessor : UnityEditor.AssetModificationProcessor {
        static void OnWillCreateAsset(string path) {
            if (!UnityExtensionsSettings.instance.cSharpSettings.addNamespaceToCSharpFiles) {
                return;
            }

            path = AssetDatabase.GetAssetPathFromTextMetaFilePath(path);

            CSharpUtils.TryReplacePlaceholders(path);
        }
    }
}