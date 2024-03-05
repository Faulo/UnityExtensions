using UnityObject = UnityEngine.Object;

namespace Slothsoft.UnityExtensions.Editor {
    public static class Test {
        public static void MissingReferences() {
        }

        internal static bool IsValidReference(UnityObject obj) => obj is null || obj;
    }
}