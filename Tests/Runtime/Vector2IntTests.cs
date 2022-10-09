using NUnit.Framework;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Tests.Runtime {
    [TestFixture(TestOf = typeof(Vector2IntExtensions))]
    sealed class Vector2IntTests {
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
        [TestCase(1, 2, 1, 2, 0)]
        [TestCase(3, 2, 3, 2, 0)]
        public void TestSwizzleXY(int x, int y, int expectedX, int expectedY, int expectedZ) {
            var vector = new Vector2Int(x, y);
            Assert.AreEqual(new Vector3Int(expectedX, expectedY, expectedZ), vector.SwizzleXY());
        }
        [TestCase(1, 2, 1, 0, 2)]
        [TestCase(3, 2, 3, 0, 2)]
        public void TestSwizzleXZ(int x, int y, int expectedX, int expectedY, int expectedZ) {
            var vector = new Vector2Int(x, y);
            Assert.AreEqual(new Vector3Int(expectedX, expectedY, expectedZ), vector.SwizzleXZ());
        }
        [TestCase(1, 2, 0, 1, 2)]
        [TestCase(3, 2, 0, 3, 2)]
        public void TestSwizzleYZ(int x, int y, int expectedX, int expectedY, int expectedZ) {
            var vector = new Vector2Int(x, y);
            Assert.AreEqual(new Vector3Int(expectedX, expectedY, expectedZ), vector.SwizzleYZ());
        }
    }
}