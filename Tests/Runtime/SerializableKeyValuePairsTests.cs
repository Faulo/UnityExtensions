using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Slothsoft.UnityExtensions.Tests.Runtime {
    [TestFixture(TestOf = typeof(SerializableKeyValuePairs<,>))]
    sealed class SerializableKeyValuePairsTests {
        [TestCase("0", "1")]
        [TestCase("1", "2")]
        public void TestSerializableThis(string key, string value) {
            var dict = CreateDictionary(key, value);

            Assert.AreEqual(value, dict[key]);
        }

        [TestCase("0", "1")]
        [TestCase("1", "2")]
        public void TestSerializableKeys(string key, string value) {
            var dict = CreateDictionary(key, value);

            CollectionAssert.AreEqual(new[] { key }, dict.Keys);
        }

        [TestCase("0", "1")]
        [TestCase("1", "2")]
        public void TestSerializableValues(string key, string value) {
            var dict = CreateDictionary(key, value);

            CollectionAssert.AreEqual(new[] { value }, dict.Values);
        }

        [TestCase("0", "1")]
        [TestCase("1", "2")]
        public void TestSerializableSerialization(string key, string value) {
            var dict = CreateDictionary(key, value);

            dict.OnAfterDeserialize();

            dict.OnBeforeSerialize();

            Assert.AreEqual(value, dict[key]);
        }

        [TestCase("0", "1")]
        [TestCase("1", "2")]
        public void TestSerializableCount(string key, string value) {
            var dict = CreateDictionary(key, value);

            Assert.AreEqual(1, dict.Count);
        }

        [TestCase("0", "1")]
        [TestCase("1", "2")]
        public void TestSerializableContainsKey(string key, string value) {
            var dict = CreateDictionary(key, value);

            Assert.IsFalse(dict.ContainsKey(value));

            Assert.IsTrue(dict.ContainsKey(key));
        }

        [TestCase("0", "1")]
        [TestCase("1", "2")]
        public void TestSerializableTryGetValue(string key, string value) {
            var dict = CreateDictionary(key, value);

            Assert.IsFalse(dict.TryGetValue(value, out string _));

            Assert.IsTrue(dict.TryGetValue(key, out string result));
            Assert.AreEqual(value, result);
        }

        [TestCase("0", "1")]
        [TestCase("1", "2")]
        public void TestSerializableGetGenericEnumerator(string key, string value) {
            var dict = CreateDictionary(key, value);

            int i = 0;

            foreach (var (actualKey, actualValue) in dict) {
                Assert.AreEqual(key, actualKey);
                Assert.AreEqual(value, actualValue);
                i++;
            }

            Assert.AreEqual(1, i);
        }

        [TestCase("0", "1")]
        [TestCase("1", "2")]
        public void TestSerializableGetObjectEnumerator(string key, string value) {
            var dict = CreateDictionary(key, value);

            int i = 0;

            foreach (object actual in (IEnumerable)dict) {
                Assert.IsInstanceOf(typeof(KeyValuePair<string, string>), actual);
                i++;
            }

            Assert.AreEqual(1, i);
        }

        SerializableKeyValuePairs<string, string> CreateDictionary(string key, string value) {
            var dict = new SerializableKeyValuePairs<string, string>();
            dict.SetItems(new Dictionary<string, string> { [key] = value });
            return dict;
        }
    }
}