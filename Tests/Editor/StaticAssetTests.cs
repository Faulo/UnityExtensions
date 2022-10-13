using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Tests.Editor {
    [TestFixture(typeof(TextAsset), "CHANGELOG.md")]
    [TestFixture(typeof(TextAsset), "README.md")]
    [TestFixture(typeof(DefaultAsset), "WebGL IFrame Template.unitypackage")]
    sealed class StaticAssetTests<T> {
        string name;

        public StaticAssetTests(string name) {
            this.name = name;
        }

        [Test]
        public void TestAssetExists() {
            string path = $"Packages/{AssemblyInfo.PACKAGE_ID}/{name}";
            var asset = AssetDatabase.LoadMainAssetAtPath(path);
            Assert.IsTrue(asset, $"Missing asset '{path}'.");
            Assert.IsInstanceOf<T>(asset, $"Expected asset '{path}' to be of type '{typeof(T)}'.");
        }
    }
}
