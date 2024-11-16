using UnityEngine;

namespace Slothsoft.UnityExtensions {
    public static class Vector2IntExtensions {
        public static void Deconstruct(this Vector2Int vector, out int x, out int y) {
            x = vector.x;
            y = vector.y;
        }

        public static Vector2Int WithX(this Vector2Int vector, int x) => new(x, vector.y);
        public static Vector2Int WithY(this Vector2Int vector, int y) => new(vector.x, y);

        public static Vector3Int SwizzleXZ(this Vector2Int vector) => new(vector.x, 0, vector.y);
        public static Vector3Int SwizzleXY(this Vector2Int vector) => new(vector.x, vector.y, 0);
        public static Vector3Int SwizzleYZ(this Vector2Int vector) => new(0, vector.x, vector.y);
    }
}