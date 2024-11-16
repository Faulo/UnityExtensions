using UnityEngine;

namespace Slothsoft.UnityExtensions {
    public static class Color32Extensions {
        public static bool IsEqualTo(this Color32 first, in Color32 second)
            => first.r == second.r
            && first.g == second.g
            && first.b == second.b
            && first.a == second.a;

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
