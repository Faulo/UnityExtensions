using System.Collections.Generic;
using NUnit.Framework;

namespace Slothsoft.UnityExtensions.Tests.Runtime {
    [TestFixture(TestOf = typeof(KeyValuePairExtensions))]
    sealed class KeyValuePairTests {
        [Test]
        public void TestDeconstruct() {
            var dict = new Dictionary<int, string>() {
                [0] = "Hello",
                [10] = "World"
            };

            int i = 0;
            foreach (var keyval in dict) {
                KeyValuePairExtensions.Deconstruct(keyval, out int key, out string value);
                if (i == 0) {
                    Assert.AreEqual(0, key);
                    Assert.AreEqual(dict[0], value);
                } else {
                    Assert.AreEqual(10, key);
                    Assert.AreEqual(dict[10], value);
                }

                i++;
            }
        }
    }
}