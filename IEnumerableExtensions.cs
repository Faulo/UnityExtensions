using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Slothsoft.UnityExtensions {
    public static class IEnumerableExtensions {
        private static readonly Random random = new Random();
        public static void ForAll<T>(this IEnumerable<T> source, Action<T> action) {
            foreach (T item in source) {
                action(item);
            }
        }
        public static IEnumerable<T> Log<T>(this IEnumerable<T> source) {
            foreach (T item in source) {
                UnityEngine.Debug.Log(item);
            }
            return source;
        }
        public static T RandomElement<T>(this IEnumerable<T> source) {
            if (source == null || source.Count() == 0) {
                return default;
            } else {
                return source.Skip(random.Next(source.Count())).FirstOrDefault();
            }
        }
        public static T RandomWeightedElement<T>(this IEnumerable<T> source, Func<T, int> weighting) {
            var weights = new Dictionary<T, int>();
            foreach (var element in source) {
                weights[element] = weighting(element);
            }
            return RandomWeightedElement(source, weights);
        }
        public static T RandomWeightedElement<T>(this IEnumerable<T> source, Dictionary<T, int> weights) {
            if (source == null || source.Count() == 0) {
                return default;
            }
            int totalWeight = weights.Values.Sum();
            int randomWeight = random.Next(totalWeight);

            foreach (var element in weights.Keys) {
                if (weights[element] > randomWeight) {
                    return element;
                }
                randomWeight -= weights[element];
            }
            throw new Exception();
        }
        //https://stackoverflow.com/questions/1287567/is-using-random-and-orderby-a-good-shuffle-algorithm
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source) {
            T[] elements = source.ToArray();
            for (int i = elements.Length - 1; i >= 0; i--) {
                int swapIndex = random.Next(i + 1);
                yield return elements[swapIndex];
                elements[swapIndex] = elements[i];
            }
        }
        //https://stackoverflow.com/questions/8741439/what-is-the-opposite-method-of-anyt
        public static bool None<TSource>(this IEnumerable<TSource> source) {
            return !source.Any();
        }
        public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) {
            return !source.Any(predicate);
        }
    }
}
