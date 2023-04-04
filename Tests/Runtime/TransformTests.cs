using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Slothsoft.UnityExtensions.Tests.Runtime {
    [TestFixture(TestOf = typeof(TransformExtensions))]
    sealed class TransformTests {
        [TestCase(0, ExpectedResult = null)]
        [TestCase(1, ExpectedResult = null)]
        [TestCase(2, ExpectedResult = null)]
        [UnityTest]
        [RequiresPlayMode]
        public IEnumerator TestRuntimeClear(int childCount) {
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

        [Test]
        public void TestTryGetComponentInChildrenReturnsSelf() {
            var context = CreateHierarchy();

            bool success = context.TryGetComponentInChildren<Transform>(out var result);

            Assert.IsTrue(success);
            Assert.AreEqual(context, result);
        }

        [Test]
        public void TestTryGetComponentInParentReturnsSelf() {
            var context = CreateHierarchy();

            bool success = context.TryGetComponentInParent<Transform>(out var result);

            Assert.IsTrue(success);
            Assert.AreEqual(context, result);
        }

        [Test]
        public void TestTryGetComponentInHierarchyReturnsSelf() {
            var context = CreateHierarchy();

            bool success = context.TryGetComponentInHierarchy<Transform>(out var result);

            Assert.IsTrue(success);
            Assert.AreEqual(context, result);
        }

        [TestCase(nameof(Transform), true)]
        [TestCase(nameof(Renderer), false)]
        [TestCase(nameof(BoxCollider), false)]
        [TestCase(nameof(SphereCollider), true)]
        public void TestTryGetComponentInChildren(string type, bool expectedResult) {
            var context = CreateHierarchy();

            bool actualResult() {
                switch (type) {
                    case nameof(Transform):
                        return context.TryGetComponentInChildren<Transform>(out _);
                    case nameof(Renderer):
                        return context.TryGetComponentInChildren<Renderer>(out _);
                    case nameof(BoxCollider):
                        return context.TryGetComponentInChildren<BoxCollider>(out _);
                    case nameof(SphereCollider):
                        return context.TryGetComponentInChildren<SphereCollider>(out _);
                    default:
                        throw new NotImplementedException();
                }
            }

            Assert.AreEqual(expectedResult, actualResult());
        }

        [TestCase(nameof(Transform), true)]
        [TestCase(nameof(Renderer), false)]
        [TestCase(nameof(BoxCollider), true)]
        [TestCase(nameof(SphereCollider), false)]
        public void TestTryGetComponentInParent(string type, bool expectedResult) {
            var context = CreateHierarchy();

            bool actualResult() {
                switch (type) {
                    case nameof(Transform):
                        return context.TryGetComponentInParent<Transform>(out _);
                    case nameof(Renderer):
                        return context.TryGetComponentInParent<Renderer>(out _);
                    case nameof(BoxCollider):
                        return context.TryGetComponentInParent<BoxCollider>(out _);
                    case nameof(SphereCollider):
                        return context.TryGetComponentInParent<SphereCollider>(out _);
                    default:
                        throw new NotImplementedException();
                }
            }

            Assert.AreEqual(expectedResult, actualResult());
        }

        [TestCase(nameof(Transform), true)]
        [TestCase(nameof(Renderer), false)]
        [TestCase(nameof(BoxCollider), true)]
        [TestCase(nameof(SphereCollider), true)]
        public void TestTryGetComponentInHierarchy(string type, bool expectedResult) {
            var context = CreateHierarchy();

            bool actualResult() {
                switch (type) {
                    case nameof(Transform):
                        return context.TryGetComponentInHierarchy<Transform>(out _);
                    case nameof(Renderer):
                        return context.TryGetComponentInHierarchy<Renderer>(out _);
                    case nameof(BoxCollider):
                        return context.TryGetComponentInHierarchy<BoxCollider>(out _);
                    case nameof(SphereCollider):
                        return context.TryGetComponentInHierarchy<SphereCollider>(out _);
                    default:
                        throw new NotImplementedException();
                }
            }

            Assert.AreEqual(expectedResult, actualResult());
        }

        Transform CreateHierarchy() {
            var parent = new GameObject();
            var context = new GameObject();
            var child = new GameObject();

            context.transform.parent = parent.transform;
            child.transform.parent = context.transform;

            parent.AddComponent<BoxCollider>();
            child.AddComponent<SphereCollider>();

            return context.transform;
        }
    }
}