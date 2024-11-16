using System;
using System.Collections.Generic;

namespace CursedBroom.Core.Extensions {
    public static class CollectionExtensions {
        public static T GetXY<T>(this IReadOnlyList<T> list, int x, int y, int width) {
            int i = x + (y * width);
            return list[i];
        }
        public static T GetXY<T>(this Span<T> list, int x, int y, int width) {
            int i = x + (y * width);
            return list[i];
        }
    }
}