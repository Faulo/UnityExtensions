using System;
using System.Collections.Generic;

namespace Slothsoft.UnityExtensions {
    public static class CollectionExtensions {
        /// <summary>
        /// Retrieve an element of <paramref name="list"/>, assuming it's a 2d map of width <paramref name="width"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static T GetXY<T>(this IReadOnlyList<T> list, int x, int y, int width) {
            int i = x + (y * width);
            return list[i];
        }

        /// <summary>
        /// Retrieve an element of <paramref name="list"/>, assuming it's a 2d map of width <paramref name="width"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static T GetXY<T>(this Span<T> list, int x, int y, int width) {
            int i = x + (y * width);
            return list[i];
        }
    }
}