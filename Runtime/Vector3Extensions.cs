using System;
using UnityEngine;

namespace Slothsoft.UnityExtensions {
    public static class Vector3Extensions {
        public static void Deconstruct(this Vector3 vector, out float x, out float y, out float z) {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public static Vector3 WithX(this Vector3 vector, float x) => new Vector3(x, vector.y, vector.z);
        public static Vector3 WithY(this Vector3 vector, float y) => new Vector3(vector.x, y, vector.z);
        public static Vector3 WithZ(this Vector3 vector, float z) => new Vector3(vector.x, vector.y, z);

        public static Vector2 SwizzleXY(this Vector3 vector) => new Vector2(vector.x, vector.y);
        public static Vector2 SwizzleXZ(this Vector3 vector) => new Vector2(vector.x, vector.z);
        public static Vector2 SwizzleYZ(this Vector3 vector) => new Vector2(vector.y, vector.z);

        [Obsolete("RoundToInt extension method is obsolete, use Vector3Int.RoundToInt instead.")]
        public static Vector3Int RoundToInt(this Vector3 vector) => new Vector3Int(
            Mathf.RoundToInt(vector.x),
            Mathf.RoundToInt(vector.y),
            Mathf.RoundToInt(vector.z)
        );
    }
}