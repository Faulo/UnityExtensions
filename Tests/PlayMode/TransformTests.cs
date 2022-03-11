using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Slothsoft.UnityExtensions.Tests.PlayMode {
    public class TransformTests {
        [TestCase(0, ExpectedResult = null)]
        [TestCase(1, ExpectedResult = null)]
        [TestCase(2, ExpectedResult = null)]
        [UnityTest]
        public IEnumerator TestClear(int childCount) {
            var context = new GameObject();

            var children = new Transform[childCount];
            for (int i = 0; i < childCount; i++) {
                children[i] = new GameObject().transform;
                children[i].parent = context.transform;
            }

            yield return null;

            context.transform.Clear();

            yield return null;

            Assert.AreEqual(0, context.transform.childCount);
        }
    }
}