using NUnit.Framework;
using UnityEngine;
using SuT = Slothsoft.UnityExtensions.Editor.Test;
using UnityObject = UnityEngine.Object;

namespace Slothsoft.UnityExtensions.Tests.Editor {
    sealed class TestMissingReferencesTests {
        [Test]
        public void GivenObject_WhenIsValid_ThenReturnTrue() {
            var obj = new PhysicMaterial();

            bool actual = SuT.IsValidReference(obj);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void GivenDestroyedObject_WhenIsValid_ThenReturnFalse() {
            var obj = new PhysicMaterial();

            UnityObject.DestroyImmediate(obj);

            bool actual = SuT.IsValidReference(obj);

            Assert.That(actual, Is.False);
        }
    }
}
