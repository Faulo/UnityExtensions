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
        public static Color WithRed(this Color color, float red) => new(red, color.g, color.b, color.a);
        public static Color WithGreen(this Color color, float green) => new(color.r, green, color.b, color.a);
        public static Color WithBlue(this Color color, float blue) => new(color.r, color.g, blue, color.a);
        public static Color WithAlpha(this Color color, float alpha) => new(color.r, color.g, color.b, alpha);
    }
}