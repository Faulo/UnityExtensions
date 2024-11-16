using UnityEngine;

namespace Slothsoft.UnityExtensions {
    public static class BoundsIntExtensions {
        public static bool Contains2D(this in BoundsInt bounds, in Vector2Int position) {
            return position.x >= bounds.xMin && position.x < bounds.xMax
                && position.y >= bounds.yMin && position.y < bounds.yMax;
        }
        public static bool Contains2D(this in BoundsInt bounds, in Vector3Int position) {
            return position.x >= bounds.xMin && position.x < bounds.xMax
                && position.y >= bounds.yMin && position.y < bounds.yMax;
        }
    }
}
