using NUnit.Framework;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Tests {
    public class TransformTests {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void TestGetChildren(int childCount) {
            var context = new GameObject();

            var children = new Transform[childCount];
            for (int i = 0; i < childCount; i++) {
                children[i] = new GameObject().transform;
                children[i].parent = context.transform;
            }

            CollectionAssert.AreEqual(children, context.transform.GetChildren());
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void TestClear(int childCount) {
            var context = new GameObject();

            var children = new Transform[childCount];
            for (int i = 0; i < childCount; i++) {
                children[i] = new GameObject().transform;
                children[i].parent = context.transform;
            }

            context.transform.Clear();

            Assert.AreEqual(0, context.transform.childCount);
        }
    }
}