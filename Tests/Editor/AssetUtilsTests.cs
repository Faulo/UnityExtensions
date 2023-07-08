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

        string inconclusiveMessage => $"Failed to load asset of type '{typeof(T)}' at location '{assetPath}'!";

        public AssetUtilsTests(string assetPath, bool isDirectory, string searchFolder) {
            this.assetPath = assetPath;
            this.isDirectory = isDirectory;
            this.searchFolder = searchFolder;

            expectedAsset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
        }

        [Test]
        public void TestLoadAssetAtFile() {
            if (!expectedAsset) {
                Assert.Inconclusive(inconclusiveMessage);
                return;
            }

            var actualAsset = isDirectory
                ? AssetUtils.LoadAssetAtFile<T>(new DirectoryInfo(assetPath))
                : AssetUtils.LoadAssetAtFile<T>(new FileInfo(assetPath));

            Assert.AreEqual(expectedAsset, actualAsset);
        }

        [Test]
        public void TestLoadAssetsOfTypeNoSearchFolders() {
            if (!expectedAsset) {
                Assert.Inconclusive(inconclusiveMessage);
                return;
            }

            var actualAssets = AssetUtils.LoadAssetsOfType<T>();

            CollectionAssert.Contains(actualAssets, expectedAsset);
        }

        [Test]
        public void TestLoadAssetsOfTypeCorrectSearchFolder() {
            if (!expectedAsset) {
                Assert.Inconclusive(inconclusiveMessage);
                return;
            }

            var actualAssets = AssetUtils.LoadAssetsOfType<T>(searchFolder);

            CollectionAssert.Contains(actualAssets, expectedAsset);
        }

        [Test]
        public void TestLoadAssetsOfTypeWrongSearchFolders() {
            if (!expectedAsset) {
                Assert.Inconclusive(inconclusiveMessage);
                return;
            }

            var actualAssets = AssetUtils.LoadAssetsOfType<T>("ProjectSettings", "FolderThatDoesNotExist");

            CollectionAssert.DoesNotContain(actualAssets, expectedAsset);
        }

        [Test]
        public void TestLoadAssetsOfTypeWrongType() {
            if (!expectedAsset) {
                Assert.Inconclusive(inconclusiveMessage);
                return;
            }

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