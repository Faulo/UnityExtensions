using NUnit.Framework;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Tests {
    public class TransformTests {
        [Test]
        public void TestSetX() {
            var gameObject = new GameObject();
            gameObject.transform.position = Vector3.zero;

            gameObject.transform.SetX(10);

            Assert.AreEqual(gameObject.transform.position, new Vector3(10, 0, 0));
        }
        [Test]
        public void TestSetY() {
            var gameObject = new GameObject();
            gameObject.transform.position = Vector3.zero;

            gameObject.transform.SetY(10);

            Assert.AreEqual(gameObject.transform.position, new Vector3(0, 10, 0));
        }
        [Test]
        public void TestSetZ() {
            var gameObject = new GameObject();
            gameObject.transform.position = Vector3.zero;

            gameObject.transform.SetZ(10);

            Assert.AreEqual(gameObject.transform.position, new Vector3(0, 0, 10));
        }

        [Test]
        public void TestSetScaleX() {
            var gameObject = new GameObject();
            gameObject.transform.localScale = Vector3.one;

            gameObject.transform.SetScaleX(10);

            Assert.AreEqual(gameObject.transform.localScale, new Vector3(10, 1, 1));
        }
        [Test]
        public void TestSetScaleY() {
            var gameObject = new GameObject();
            gameObject.transform.localScale = Vector3.one;

            gameObject.transform.SetScaleY(10);

            Assert.AreEqual(gameObject.transform.localScale, new Vector3(1, 10, 1));
        }
        [Test]
        public void TestSetScaleZ() {
            var gameObject = new GameObject();
            gameObject.transform.localScale = Vector3.one;

            gameObject.transform.SetScaleZ(10);

            Assert.AreEqual(gameObject.transform.localScale, new Vector3(1, 1, 10));
        }
    }
}