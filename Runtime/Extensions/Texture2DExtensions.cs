using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Assertions;
using UnityObject = UnityEngine.Object;

namespace Slothsoft.UnityExtensions {
    public static class Texture2DExtensions {
        static RectInt RoundToInt(Rect rect) {
            return new(
                Mathf.RoundToInt(rect.x),
                Mathf.RoundToInt(rect.y),
                Mathf.RoundToInt(rect.width),
                Mathf.RoundToInt(rect.height)
            );
        }
        #region Color32
        public static Color32[] GetPixels32(this Texture2D texture, in RectInt rect, in Color32 clearColor = default) {
            int width = rect.width;
            int height = rect.height;

            var pixels = texture.GetPixels32();
            var croppedPixels = new Color32[width * height];

            for (int row = 0; row < height; row++) {
                int sourceIndex = ((rect.y + row) * texture.width) + rect.x;
                int destIndex = row * width;

                Array.Copy(pixels, sourceIndex, croppedPixels, destIndex, width);
            }

            ClearAlpha(croppedPixels, clearColor);

            return croppedPixels;
        }
        public static Color32[] GetPixels32(this Texture2D texture, in Rect rect, in Color32 clearColor = default) {
            return texture.GetPixels32(RoundToInt(rect), clearColor);
        }
        public static void SetPixels32(this Texture2D texture, in RectInt rect, Color32[] pixels) {
            texture.SetPixels32(rect.x, rect.y, rect.width, rect.height, pixels);
        }
        static void ClearAlpha(in Color32[] pixels, in Color32 clearColor) {
            for (int i = 0; i < pixels.Length; i++) {
                if (pixels[i].a == 0) {
                    pixels[i] = clearColor;
                }
            }
        }
        public static bool IsFullyTransparent(this Texture2D texture) {
            return texture
                .GetPixels32()
                .All(pixel => pixel.a == 0);
        }
        #endregion

        #region Color
        internal static Color[] GetPixels(this Texture2D texture, in RectInt rect) {
            var pixels = texture.GetPixels(rect.x, rect.y, rect.width, rect.height);
            ClearAlpha(pixels);
            return pixels;
        }
        internal static Color[] GetPixels(this Texture2D texture, in Rect rect) {
            return texture.GetPixels(RoundToInt(rect));
        }
        internal static void SetPixels(this Texture2D texture, in RectInt rect, Color[] pixels) {
            texture.SetPixels(rect.x, rect.y, rect.width, rect.height, pixels);
        }
        static void ClearAlpha(in Color[] pixels) {
            for (int i = 0; i < pixels.Length; i++) {
                if (pixels[i].a == 0) {
                    pixels[i] = default;
                }
            }
        }
        #endregion

#if UNITY_EDITOR
        public static Texture2D AsReadable(this Texture2D texture) {
            return texture.isReadable
                ? texture
                : UnityObject.Instantiate(texture);
        }
        public static Sprite[] GetSpritesRow(this Texture2D texture, int rowIndex) {
            var sprites = texture.GetSprites();
            Assert.AreNotEqual(0, sprites.Length);
            int spritesPerRow = Mathf.RoundToInt(texture.width / sprites[0].rect.width);
            return sprites[(rowIndex * spritesPerRow)..((rowIndex + 1) * spritesPerRow)]
                .Where(sprite => !sprite.IsFullyTransparent())
                .ToArray();
        }
        public static Sprite[][] GetSpritesMatrix(this Texture2D texture) {
            var sprites = texture.GetSprites();
            Assert.AreNotEqual(0, sprites.Length);
            var sprite = sprites[0];
            int rows = Mathf.RoundToInt(texture.height / sprite.rect.height);
            int columns = Mathf.RoundToInt(texture.width / sprite.rect.width);
            var matrix = new Sprite[rows][];
            for (int i = 0; i < rows; i++) {
                matrix[i] = sprites[(i * columns)..((i + 1) * columns)]
                    .Where(sprite => !sprite.IsFullyTransparent())
                    .ToArray();
            }

            return matrix;
        }
        public static Sprite[] GetSprites(this Texture2D texture) {
            return UnityEditor.AssetDatabase
                .LoadAllAssetsAtPath(UnityEditor.AssetDatabase.GetAssetPath(texture))
                .OfType<Sprite>()
                .OrderBy(Comparer)
                .ToArray();
        }
        static int Comparer(UnityObject obj) {
            var match = Regex.Match(obj.name, "\\d+$");
            return match.Success
                ? int.Parse(match.Value)
                : 0;
        }
#endif
    }
}
