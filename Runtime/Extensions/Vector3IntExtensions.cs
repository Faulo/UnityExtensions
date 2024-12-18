using UnityEngine;

namespace Slothsoft.UnityExtensions {
    public static class Vector3IntExtensions {
        public static void Deconstruct(this Vector3Int vector, out int x, out int y, out int z) {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public static Vector3Int WithX(this Vector3Int vector, int x) => new(x, vector.y, vector.z);
        public static Vector3Int WithY(this Vector3Int vector, int y) => new(vector.x, y, vector.z);
        public static Vector3Int WithZ(this Vector3Int vector, int z) => new(vector.x, vector.y, z);

        public static Vector2Int SwizzleXY(this Vector3Int vector) => new(vector.x, vector.y);
        public static Vector2Int SwizzleXZ(this Vector3Int vector) => new(vector.x, vector.z);
        public static Vector2Int SwizzleYZ(this Vector3Int vector) => new(vector.y, vector.z);
    }
}