using UnityEngine;

namespace Slothsoft.UnityExtensions {
    public static class ColorExtensions {
        public static void Deconstruct(this Color color, out float red, out float green, out float blue) {
            red = color.r;
            green = color.g;
            blue = color.b;
        }

        public static void Deconstruct(this Color color, out float red, out float green, out float blue, out float alpha) {
            red = color.r;
            green = color.g;
            blue = color.b;
            alpha = color.a;
        }

        /// <summary>
        /// Create a copy of <paramref name="color"/>, replacing <paramref name="red"/>.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="red"></param>
        /// <returns></returns>
        public static Color WithRed(this Color color, float red) => new(red, color.g, color.b, color.a);

        /// <summary>
        /// Create a copy of <paramref name="color"/>, replacing <paramref name="green"/>.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="red"></param>
        /// <returns></returns>
        public static Color WithGreen(this Color color, float green) => new(color.r, green, color.b, color.a);

        /// <summary>
        /// Create a copy of <paramref name="color"/>, replacing <paramref name="blue"/>.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="red"></param>
        /// <returns></returns>
        public static Color WithBlue(this Color color, float blue) => new(color.r, color.g, blue, color.a);

        /// <summary>
        /// Create a copy of <paramref name="color"/>, replacing <paramref name="alpha"/>.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="red"></param>
        /// <returns></returns>
        public static Color WithAlpha(this Color color, float alpha) => new(color.r, color.g, color.b, alpha);
    }
}