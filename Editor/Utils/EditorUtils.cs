using System.Reflection;
using UnityEditor;

namespace Slothsoft.UnityExtensions.Editor {
    public static class EditorUtils {
        public static void ClearConsole() {
            var assembly = Assembly.GetAssembly(typeof(SceneView));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
        }
    }
}