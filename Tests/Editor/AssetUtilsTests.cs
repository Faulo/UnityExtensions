using System.IO;
using NUnit.Framework;
using Slothsoft.UnityExtensions.Editor;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Slothsoft.UnityExtensions.Tests.Editor {
    [TestFixture(typeof(DefaultAsset), "Assets/Scenes", true, "Assets")]
    [TestFixture(typeof(DefaultAsset), "Packages/net.slothsoft.unity-extensions/Runtime", true, "Packages")]
    [TestFixture(typeof(AssemblyDefinitionAsset), "Packages/net.slothsoft.unity-extensions/Runtime/Slothsoft.UnityExtensions.asmdef", false, "Packages/net.slothsoft.unity-extensions")]
    sealed class AssetUtilsTests<T> where T : UnityObject {
        readonly string assetPath;
        readonly bool isDirectory;
        readonly string searchFolder;
        readonly T expectedAsset;

        public AssetUtilsTests(string assetPath, bool isDirectory, string searchFolder) {
            this.assetPath = assetPath;
            this.isDirectory = isDirectory;
            this.searchFolder = searchFolder;

            expectedAsset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
        }

        [Test]
        public void TestLoadAssetAtFile() {
            var actualAsset = AssetUtils.LoadAssetAtFile<T>(isDirectory ? new DirectoryInfo(assetPath) : new FileInfo(assetPath));

            Assert.AreEqual(expectedAsset, actualAsset);
        }

        [Test]
        public void TestLoadAssetsOfTypeNoSearchFolders() {
            var actualAssets = AssetUtils.LoadAssetsOfType<T>();

            CollectionAssert.Contains(actualAssets, expectedAsset);
        }

        [Test]
        public void TestLoadAssetsOfTypeCorrectSearchFolder() {
            var actualAssets = AssetUtils.LoadAssetsOfType<T>(searchFolder);

            CollectionAssert.Contains(actualAssets, expectedAsset);
        }

        [Test]
        public void TestLoadAssetsOfTypeWrongSearchFolders() {
            var actualAssets = AssetUtils.LoadAssetsOfType<T>("ProjectSettings", "FolderThatDoesNotExist");

            CollectionAssert.DoesNotContain(actualAssets, expectedAsset);
        }

        [Test]
        public void TestLoadAssetsOfTypeWrongType() {
            var actualAssets = AssetUtils.LoadAssetsOfType<GameObject>();

            CollectionAssert.DoesNotContain(actualAssets, expectedAsset);
        }

        [Test]
        public void TestLoadAssetsOfTypeDoesNotContainNull() {
            var actualAssets = AssetUtils.LoadAssetsOfType<T>();

            CollectionAssert.AllItemsAreNotNull(actualAssets);
        }
    }
}
