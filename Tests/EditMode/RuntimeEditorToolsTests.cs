using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Tests.EditMode {
    sealed class RuntimeEditorToolsTests {
        class TestObject : ScriptableObject {
        }
        class TestEditor : RuntimeEditorTools<TestObject> {
            protected override void DrawEditorTools() {
            }

            public void TestTarget(TestObject obj) {
                Assert.AreEqual(obj, target);
            }

            public void TestIndentLevel() {
                EditorGUI.indentLevel = 0;

                Assert.AreEqual(0, indentLevel);

                indentLevel++;

                Assert.AreEqual(1, indentLevel);
            }
        }

        [SetUp]
        public void SetUpEditor() {
            obj = ScriptableObject.CreateInstance<TestObject>();
            editor = Editor.CreateEditor(obj, typeof(TestEditor)) as TestEditor;

            Assert.IsNotNull(editor);
        }

        [TearDown]
        public void CleanUpEditor() {
        }

        TestObject obj;
        TestEditor editor;

        [Test]
        public void TestTarget() {
            editor.TestTarget(obj);
        }

        [Test]
        public void TestIndentLevel() {
            editor.TestIndentLevel();
        }
    }
}