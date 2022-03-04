using System;
using NUnit.Framework;

namespace Slothsoft.UnityExtensions.Tests.EditMode {
    public class SerializableDictionaryTests {
        [TestCase("0", "1")]
        [TestCase("1", "2")]
        [Obsolete]
        public void TestSerializable(string key, string value) {
            var dict = new SerializableDictionary<string, string> {
                [key] = value
            };

            dict.OnBeforeSerialize();

            dict.Clear();

            dict.OnAfterDeserialize();

            Assert.AreEqual(value, dict[key]);
        }
    }
}