using System.Runtime.CompilerServices;
using UnityEngine;

namespace Slothsoft.UnityExtensions {
    public static class BoundsIntExtensions {
        /// <summary>
        /// Same as <see cref="Bounds.Contains(Vector3)"/>, but only compare x and y of <paramref name="position"/>.
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains2D(this in BoundsInt bounds, in Vector2Int position) {
            return position.x >= bounds.xMin && position.x < bounds.xMax
                && position.y >= bounds.yMin && position.y < bounds.yMax;
        }

        /// <summary>
        /// Same as <see cref="Bounds.Contains(Vector3)"/>, but only compare x and y of <paramref name="position"/>.
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains2D(this in BoundsInt bounds, in Vector3Int position) {
            return position.x >= bounds.xMin && position.x < bounds.xMax
                && position.y >= bounds.yMin && position.y < bounds.yMax;
        }
    }
}
