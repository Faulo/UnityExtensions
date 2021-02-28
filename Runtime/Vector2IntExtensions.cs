using UnityEngine;

namespace Slothsoft.UnityExtensions {
    public static class Vector2IntExtensions {
        public static void Deconstruct(this Vector2Int vector, out int x, out int y) {
            x = vector.x;
            y = vector.y;
        }
        public static Vector2Int WithX(this Vector2Int vector, int x) => new Vector2Int(x, vector.y);
        public static Vector2Int WithY(this Vector2Int vector, int y) => new Vector2Int(vector.x, y);
    }
}