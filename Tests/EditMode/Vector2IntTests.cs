using NUnit.Framework;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Tests {
    public class Vector2IntTests {
        [Test]
        public void TestDeconstruct() {
            var (x, y) = new Vector2Int(1, 2);
            Assert.AreEqual(1, x);
            Assert.AreEqual(2, y);
        }
        [Test]
        public void TestWithX() {
            var vector = Vector2Int.zero;
            Assert.AreEqual(Vector2Int.right, vector.WithX(1));
            Assert.AreEqual(Vector2Int.zero, vector);
        }
        [Test]
        public void TestWithY() {
            var vector = Vector2Int.zero;
            Assert.AreEqual(Vector2Int.up, vector.WithY(1));
            Assert.AreEqual(Vector2Int.zero, vector);
        }
    }
}