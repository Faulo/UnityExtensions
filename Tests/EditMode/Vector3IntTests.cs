using NUnit.Framework;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Tests {
    public class Vector3IntTests {
        [Test]
        public void TestDeconstruct() {
            var (x, y, z) = new Vector3Int(1, 2, 3);
            Assert.AreEqual(1, x);
            Assert.AreEqual(2, y);
            Assert.AreEqual(3, z);
        }
        [Test]
        public void TestWithX() {
            var vector = Vector3Int.zero;
            Assert.AreEqual(Vector3Int.right, vector.WithX(1));
            Assert.AreEqual(Vector3Int.zero, vector);
        }
        [Test]
        public void TestWithY() {
            var vector = Vector3Int.zero;
            Assert.AreEqual(Vector3Int.up, vector.WithY(1));
            Assert.AreEqual(Vector3Int.zero, vector);
        }
        [Test]
        public void TestWithZ() {
            var vector = Vector3Int.zero;
            Assert.AreEqual(new Vector3Int(0, 0, 1), vector.WithZ(1));
            Assert.AreEqual(Vector3Int.zero, vector);
        }
    }
}