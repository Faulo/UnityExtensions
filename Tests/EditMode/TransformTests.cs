using System;
using NUnit.Framework;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Tests.EditMode {
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

            bool actualresult = type switch {
                nameof(Transform) => context.TryGetComponentInChildren<Transform>(out _),
                nameof(Renderer) => context.TryGetComponentInChildren<Renderer>(out _),
                nameof(BoxCollider) => context.TryGetComponentInChildren<BoxCollider>(out _),
                nameof(SphereCollider) => context.TryGetComponentInChildren<SphereCollider>(out _),
                _ => throw new NotImplementedException(),
            };

            Assert.AreEqual(expectedResult, actualresult);
        }

        [TestCase(nameof(Transform), true)]
        [TestCase(nameof(Renderer), false)]
        [TestCase(nameof(BoxCollider), true)]
        [TestCase(nameof(SphereCollider), false)]
        public void TestTryGetComponentInParent(string type, bool expectedResult) {
            var context = CreateHierarchy();

            bool actualresult = type switch {
                nameof(Transform) => context.TryGetComponentInParent<Transform>(out _),
                nameof(Renderer) => context.TryGetComponentInParent<Renderer>(out _),
                nameof(BoxCollider) => context.TryGetComponentInParent<BoxCollider>(out _),
                nameof(SphereCollider) => context.TryGetComponentInParent<SphereCollider>(out _),
                _ => throw new NotImplementedException(),
            };

            Assert.AreEqual(expectedResult, actualresult);
        }

        [TestCase(nameof(Transform), true)]
        [TestCase(nameof(Renderer), false)]
        [TestCase(nameof(BoxCollider), true)]
        [TestCase(nameof(SphereCollider), true)]
        public void TestTryGetComponentInHierarchy(string type, bool expectedResult) {
            var context = CreateHierarchy();

            bool actualresult = type switch {
                nameof(Transform) => context.TryGetComponentInHierarchy<Transform>(out _),
                nameof(Renderer) => context.TryGetComponentInHierarchy<Renderer>(out _),
                nameof(BoxCollider) => context.TryGetComponentInHierarchy<BoxCollider>(out _),
                nameof(SphereCollider) => context.TryGetComponentInHierarchy<SphereCollider>(out _),
                _ => throw new NotImplementedException(),
            };

            Assert.AreEqual(expectedResult, actualresult);
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