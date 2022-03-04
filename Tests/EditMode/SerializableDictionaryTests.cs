using System;
using NUnit.Framework;

namespace Slothsoft.UnityExtensions.Tests.EditMode {
    public class SerializableDictionaryTests {
        [Test]
        [Obsolete]
        public void TestSerializable() {
            var dict = new SerializableDictionary<int, int> {
                [0] = 1
            };

            dict.OnBeforeSerialize();

            dict.OnAfterDeserialize();

            Assert.AreEqual(1, dict[0]);
        }
    }
}