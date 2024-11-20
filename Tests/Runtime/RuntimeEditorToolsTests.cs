using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Slothsoft.UnityExtensions.Tests.Runtime {
    [TestFixture(typeof(TestObject), typeof(TestObjectEditor), false, TestOf = typeof(RuntimeEditorTools<>))]
    [TestFixture(typeof(TestComponent), typeof(TestComponentEditor), true, TestOf = typeof(RuntimeEditorTools<>))]
    sealed class RuntimeEditorToolsTests<TObject, TEditor>
        where TObject : UnityObject
        where TEditor : TestEditor<TObject> {

        TObject obj;
        TEditor editor;
        GameObject gameObject;
        readonly bool isComponent;

        public RuntimeEditorToolsTests(bool isComponent) {
            this.isComponent = isComponent;
        }

        [SetUp]
        public void SetUpEditor() {
            if (isComponent) {
                gameObject = new GameObject();
                obj = gameObject.AddComponent(typeof(TObject)) as TObject;
            } else {
                obj = ScriptableObject.CreateInstance(typeof(TObject)) as TObject;
            }

            editor = Editor.CreateEditor(obj, typeof(TEditor)) as TEditor;

            Assert.IsNotNull(obj);
            Assert.IsNotNull(editor);
        }

        [TearDown]
        public void CleanUpEditor() {
            if (gameObject) {
                UnityObject.DestroyImmediate(gameObject);
            }
        }

        [Test]
        public void TestTarget() {
            Assert.AreEqual(obj, editor.target);
        }

        [Test]
        public void TestComponent() {
            Assert.AreEqual(obj, editor.component);
        }

        [Test]
        public void TestGameObject() {
            if (isComponent) {
                Assert.AreEqual(gameObject, editor.gameObject);
            } else {
                Assert.IsNull(editor.gameObject);
            }
        }

        [Test]
        public void TestIndentLevel() {
            EditorGUI.indentLevel = 0;

            Assert.AreEqual(0, editor.indentLevel);

            editor.indentLevel++;

            Assert.AreEqual(1, editor.indentLevel);
        }

        [Test]
        public void TestLabel() {
            StringAssert.Contains(typeof(TObject).ToString(), editor.label);
        }
    }

    sealed class TestObject : ScriptableObject {
    }

    sealed class TestComponent : MonoBehaviour {
    }

    abstract class TestEditor<T> : RuntimeEditorTools<T> where T : UnityObject {

        public new string label => base.label;

        public new T target => base.target;

#pragma warning disable CS0618 // Typ oder Element ist veraltet
        public new T component => base.component;
        public new GameObject gameObject => base.gameObject;
#pragma warning restore CS0618 // Typ oder Element ist veraltet

        public new int indentLevel {
            get => base.indentLevel;
            set => base.indentLevel = value;
        }

        protected override void DrawEditorTools() {
        }
    }

    sealed class TestObjectEditor : TestEditor<TestObject> {
    }

    sealed class TestComponentEditor : TestEditor<TestComponent> {
    }
}