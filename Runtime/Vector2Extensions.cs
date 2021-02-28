using UnityEngine;

namespace Slothsoft.UnityExtensions {
    public static class Vector2Extensions {
        public static void Deconstruct(this Vector2 vector, out float x, out float y) {
            x = vector.x;
            y = vector.y;
        }
        public static Vector2 WithX(this Vector2 vector, float x) => new Vector2(x, vector.y);
        public static Vector2 WithY(this Vector2 vector, float y) => new Vector2(vector.x, y);
        public static Vector3 SwizzleXZ(this Vector2 vector) => new Vector3(vector.x, 0, vector.y);
        public static Vector3 SwizzleXY(this Vector2 vector) => new Vector3(vector.x, vector.y, 0);
        public static Vector3 SwizzleYZ(this Vector2 vector) => new Vector3(0, vector.x, vector.y);
        public static Vector2Int RoundToInt(this Vector2 vector) => new Vector2Int(
            Mathf.RoundToInt(vector.x),
            Mathf.RoundToInt(vector.y)
        );
    }
}