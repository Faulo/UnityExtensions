using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Slothsoft.UnityExtensions {
    public static class SpriteExtensions {
        public static RectInt GetRect(this Sprite sprite, bool subtractBorder = false) {
            var rect = sprite.rect;

            if (subtractBorder) {
                var border = sprite.border;
                rect.xMin += border.x;
                rect.yMin += border.y;
                rect.xMax -= border.z;
                rect.yMax -= border.w;
            }

            return new RectInt(
                Mathf.RoundToInt(rect.x),
                Mathf.RoundToInt(rect.y),
                Mathf.RoundToInt(rect.width),
                Mathf.RoundToInt(rect.height)
            );
        }
        public static Color32[] GetPixels32(this Sprite sprite, in Color32 clearColor = default) {
            return sprite
                .texture
                .GetPixels32(sprite.GetRect(), clearColor);
        }
        public static IEnumerable<(Vector2 position, Color32 color)> GetPixelsWithPosition(this Sprite sprite) {
            var rect = sprite.GetRect();
            var pixels = sprite.texture.GetPixels32(rect);
            for (int y = 0; y < rect.height; y++) {
                for (int x = 0; x < rect.width; x++) {
                    var position = new Vector2(
                        (x + 0.5f) / sprite.pixelsPerUnit,
                        (y + 0.5f) / sprite.pixelsPerUnit
                    );
                    var color = pixels[(y * rect.width) + x];
                    yield return (position, color);
                }
            }
        }
        public static bool IsFullyTransparent(this Sprite sprite) {
            return sprite
                .GetPixels32()
                .All(pixel => pixel.a == 0);
        }
    }
}
