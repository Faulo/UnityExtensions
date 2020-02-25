using System;
using System.Collections.Generic;
using System.Linq;

namespace Slothsoft.UnityExtensions {
    public static class IEnumerableExtensions {
        static readonly Random random = new Random();
        public static IEnumerable<TResult> SelectNotNull<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector) {
            return source.Select(selector).Where(result => result != null);
        }
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
        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, params T[] args) {
            return source.Except((IEnumerable<T>) args);
        }
        public static T RandomElement<T>(this IEnumerable<T> source) {
            if (source == null || source.Count() == 0) {
                return default;
            } else {
                return source.Skip(random.Next(source.Count())).FirstOrDefault();
            }
        }
        public static T RandomWeightedElement<T>(this IEnumerable<T> source, Func<T, int> weighting) {
            source = source.ToArray();
            if (source == null || source.Count() == 0) {
                return default;
            }
            var weights = new Dictionary<T, int>();
            foreach (var element in source) {
                weights[element] = weighting(element);
                if (weights[element] < 0) {
                    throw new ArgumentOutOfRangeException("Weight cannot be negative.");
                }
            }
            int totalWeight = weights.Values.Sum();
            if (totalWeight == 0) {
                return default;
            }
            int randomWeight = random.Next(totalWeight);
            foreach (var element in weights.Keys) {
                if (weights[element] > randomWeight) {
                    return element;
                }
                randomWeight -= weights[element];
            }
            throw new Exception();
        }
        public static T RandomWeightedElement<T>(this IEnumerable<T> source, Dictionary<T, int> weights) {
            return RandomWeightedElement(source, key => weights[key]);
        }
        public static T RandomWeightedElementDescending<T>(this IEnumerable<T> source, Func<T, int> weighting) {
            source = source.ToArray();
            var weights = new Dictionary<T, int>();
            foreach (var element in source) {
                weights[element] = weighting(element);
            }
            var sum = weights.Values.Sum();
            foreach (var element in source) {
                weights[element] = sum - weights[element];
            }
            return RandomWeightedElement(source, weights);
        }
        public static T RandomWeightedElementDescending<T>(this IEnumerable<T> source, Dictionary<T, int> weights) {
            return source.RandomWeightedElementDescending(key => weights[key]);
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
