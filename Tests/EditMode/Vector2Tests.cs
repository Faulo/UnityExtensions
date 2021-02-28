using NUnit.Framework;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Tests {
    public class Vector2Tests {
        [Test]
        public void TestDeconstruct() {
            var (x, y) = new Vector2(1, 2);
            Assert.AreEqual(1f, x);
            Assert.AreEqual(2f, y);
        }
        [Test]
        public void TestWithX() {
            var vector = Vector2.zero;
            Assert.AreEqual(Vector2.right, vector.WithX(1));
            Assert.AreEqual(Vector2.zero, vector);
        }
        [Test]
        public void TestWithY() {
            var vector = Vector2.zero;
            Assert.AreEqual(Vector2.up, vector.WithY(1));
            Assert.AreEqual(Vector2.zero, vector);
        }
        static readonly (Vector2 source, Vector2Int target)[] ROUNDED_INTEGERS = new[] {
            (Vector2.zero, Vector2Int.zero),
            (new Vector2(0.6f, -0.6f), new Vector2Int(1, -1)),
        };
        [Test]
        public void TestRoundToInt([ValueSource(nameof(ROUNDED_INTEGERS))] (Vector2 source, Vector2Int target) pair) {
            Assert.AreEqual(pair.target, pair.source.RoundToInt());
        }
        [Test]
        public void TestSwizzle() {
            var vector = new Vector2(1, 2);
            Assert.AreEqual(new Vector3(1, 2, 0), vector.SwizzleXY());
            Assert.AreEqual(new Vector3(1, 0, 2), vector.SwizzleXZ());
            Assert.AreEqual(new Vector3(0, 1, 2), vector.SwizzleYZ());
        }
    }
}