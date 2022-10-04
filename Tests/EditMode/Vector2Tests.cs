using System;
using NUnit.Framework;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Tests.EditMode {
    sealed class Vector2Tests {
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
        [Obsolete]
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
        [TestCase(1, 2, 1, 2, 0)]
        [TestCase(3, 2, 3, 2, 0)]
        public void TestSwizzleXY(float x, float y, float expectedX, float expectedY, float expectedZ) {
            var vector = new Vector2(x, y);
            Assert.AreEqual(new Vector3(expectedX, expectedY, expectedZ), vector.SwizzleXY());
        }
        [TestCase(1, 2, 1, 0, 2)]
        [TestCase(3, 2, 3, 0, 2)]
        public void TestSwizzleXZ(float x, float y, float expectedX, float expectedY, float expectedZ) {
            var vector = new Vector2(x, y);
            Assert.AreEqual(new Vector3(expectedX, expectedY, expectedZ), vector.SwizzleXZ());
        }
        [TestCase(1, 2, 0, 1, 2)]
        [TestCase(3, 2, 0, 3, 2)]
        public void TestSwizzleYZ(float x, float y, float expectedX, float expectedY, float expectedZ) {
            var vector = new Vector2(x, y);
            Assert.AreEqual(new Vector3(expectedX, expectedY, expectedZ), vector.SwizzleYZ());
        }
        [TestCase(1, -1, 0, 0)]
        [TestCase(0, 10, 0, 1)]
        [TestCase(5, -1, 1, 0)]
        [TestCase(0.9f, -1, 0, -1)]
        [TestCase(0.6f, -0.6f, 0, 0)]
        public void TestSnapToCardinal(float x, float y, int expectedX, int expectedY) {
            var vector = new Vector2(x, y);
            Assert.AreEqual(new Vector2Int(expectedX, expectedY), vector.SnapToCardinal());
        }
    }
}