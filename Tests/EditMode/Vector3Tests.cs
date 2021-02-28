using NUnit.Framework;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Tests {
    public class Vector3Tests {
        [Test]
        public void TestDeconstruct() {
            var (x, y, z) = new Vector3(1, 2, 3);
            Assert.AreEqual(1f, x);
            Assert.AreEqual(2f, y);
            Assert.AreEqual(3f, z);
        }
        [Test]
        public void TestWithX() {
            var vector = Vector3.zero;
            Assert.AreEqual(Vector3.right, vector.WithX(1));
            Assert.AreEqual(Vector3.zero, vector);
        }
        [Test]
        public void TestWithY() {
            var vector = Vector3.zero;
            Assert.AreEqual(Vector3.up, vector.WithY(1));
            Assert.AreEqual(Vector3.zero, vector);
        }
        [Test]
        public void TestWithZ() {
            var vector = Vector3.zero;
            Assert.AreEqual(Vector3.forward, vector.WithZ(1));
            Assert.AreEqual(Vector3.zero, vector);
        }
        static readonly (Vector3 source, Vector3Int target)[] ROUNDED_INTEGERS = new[] {
            (Vector3.zero, Vector3Int.zero),
            (new Vector3(0.6f, -0.6f, 0.2f), new Vector3Int(1, -1, 0)),
        };
        [Test]
        public void TestRoundToInt([ValueSource(nameof(ROUNDED_INTEGERS))] (Vector3 source, Vector3Int target) pair) {
            Assert.AreEqual(pair.target, pair.source.RoundToInt());
        }
        [Test]
        public void TestSwizzle() {
            var vector = new Vector3(1, 2, 3);
            Assert.AreEqual(new Vector2(1, 2), vector.SwizzleXY());
            Assert.AreEqual(new Vector2(1, 3), vector.SwizzleXZ());
            Assert.AreEqual(new Vector2(2, 3), vector.SwizzleYZ());
        }
    }
}