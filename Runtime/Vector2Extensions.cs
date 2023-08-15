using System;

namespace Slothsoft.UnityExtensions {
    public static class Vector2Extensions {
        public static void Deconstruct(this UnityEngine.Vector2 vector, out float x, out float y) {
            x = vector.x;
            y = vector.y;
        }

        public static UnityEngine.Vector2 WithX(this UnityEngine.Vector2 vector, float x) => new UnityEngine.Vector2(x, vector.y);
        public static UnityEngine.Vector2 WithY(this UnityEngine.Vector2 vector, float y) => new UnityEngine.Vector2(vector.x, y);

        public static UnityEngine.Vector3 SwizzleXZ(this UnityEngine.Vector2 vector) => new UnityEngine.Vector3(vector.x, 0, vector.y);
        public static UnityEngine.Vector3 SwizzleXY(this UnityEngine.Vector2 vector) => new UnityEngine.Vector3(vector.x, vector.y, 0);
        public static UnityEngine.Vector3 SwizzleYZ(this UnityEngine.Vector2 vector) => new UnityEngine.Vector3(0, vector.x, vector.y);

        [Obsolete("RoundToInt extension method is obsolete, use UnityEngine.Vector2Int.RoundToInt instead.")]
        public static UnityEngine.Vector2Int RoundToInt(this UnityEngine.Vector2 vector) => new UnityEngine.Vector2Int(
            UnityEngine.Mathf.RoundToInt(vector.x),
            UnityEngine.Mathf.RoundToInt(vector.y)
        );

        /// <summary>
        /// Aligns a vector to one of the 4 cardinal directions.
        /// </summary>
        /// <param name="direction">The vector to align.</param>
        /// <returns>One of [<see cref="UnityEngine.Vector2Int.zero"/>, <see cref="UnityEngine.Vector2Int.up"/>, <see cref="UnityEngine.Vector2Int.down"/>, <see cref="UnityEngine.Vector2Int.left"/>, <see cref="UnityEngine.Vector2Int.right"/>].</returns>
        public static UnityEngine.Vector2Int SnapToCardinal(this UnityEngine.Vector2 direction) {
            float x = Math.Abs(direction.x);
            float y = Math.Abs(direction.y);
            if (UnityEngine.Mathf.Approximately(x, y)) {
                return UnityEngine.Vector2Int.zero;
            }

            return x > y
                ? UnityEngine.Vector2Int.right * Math.Sign(direction.x)
                : UnityEngine.Vector2Int.up * Math.Sign(direction.y);
        }
    }
}