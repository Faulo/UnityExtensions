using UnityEngine;

namespace Slothsoft.UnityExtensions {
    public static class Color32Extensions {
        /// <summary>
        /// Check for equality (all 4 channels must match).
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool IsEqualTo(this Color32 first, in Color32 second)
            => first.r == second.r
            && first.g == second.g
            && first.b == second.b
            && first.a == second.a;

        /// <summary>
        /// Instantiate a <see cref="Texture2D"/> and set its pixels to <paramref name="bitmap"/>.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Texture2D CreateTexture(this Color32[] bitmap, int width, int height) {
            var texture = new Texture2D(width, height, TextureFormat.RGBA32, false) {
                filterMode = FilterMode.Point,
                anisoLevel = 0,
                wrapMode = TextureWrapMode.Clamp,
            };
            texture.SetPixels32(bitmap);
            texture.Apply();
            return texture;
        }
    }
}
