using System;
using NUnit.Framework;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Tests.EditMode {
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
        [TestCase(0, -0, 10, 0, 0, 10)]
        [TestCase(0.6f, -0.6f, 0.2f, 1, -1, 0)]
        [Obsolete]
        public void TestRoundToInt(float x, float y, float z, int expectedX, int expectedY, int expectedZ) {
            var vector = new Vector3(x, y, z);
            Assert.AreEqual(new Vector3Int(expectedX, expectedY, expectedZ), vector.RoundToInt());
        }
        [TestCase(1, 2, 3, 1, 2)]
        [TestCase(3, 2, 1, 3, 2)]
        public void TestSwizzleXY(float x, float y, float z, float expectedX, float expectedY) {
            var vector = new Vector3(x, y, z);
            Assert.AreEqual(new Vector2(expectedX, expectedY), vector.SwizzleXY());
        }
        [TestCase(1, 2, 3, 1, 3)]
        [TestCase(3, 2, 1, 3, 1)]
        public void TestSwizzleXZ(float x, float y, float z, float expectedX, float expectedY) {
            var vector = new Vector3(x, y, z);
            Assert.AreEqual(new Vector2(expectedX, expectedY), vector.SwizzleXZ());
        }
        [TestCase(1, 2, 3, 2, 3)]
        [TestCase(3, 2, 1, 2, 1)]
        public void TestSwizzleYZ(float x, float y, float z, float expectedX, float expectedY) {
            var vector = new Vector3(x, y, z);
            Assert.AreEqual(new Vector2(expectedX, expectedY), vector.SwizzleYZ());
        }
        [TestCase(1, -1, 1, 0, 0, 0)]
        [TestCase(0, -1, 10, 0, 0, 1)]
        [TestCase(5, -1, -4, 1, 0, 0)]
        [TestCase(0.9f, -1, 0.4f, 0, -1, 0)]
        [TestCase(0.6f, -0.6f, 0.2f, 0, 0, 0)]
        public void SnapToCardinal(float x, float y, float z, int expectedX, int expectedY, int expectedZ) {
            var vector = new Vector3(x, y, z);
            Assert.AreEqual(new Vector3Int(expectedX, expectedY, expectedZ), vector.SnapToCardinal());
        }
    }
}