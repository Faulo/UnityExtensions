using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace Slothsoft.UnityExtensions {
    public static class IEnumerableExtensions {
        static readonly Random random = new Random();
        public static IEnumerable<TResult> SelectNotNull<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector) {
            return source.Select(selector).Where(result => result != null);
        }
        public static void ForAll<T>(this IEnumerable<T> source, Action<T> action) {
            foreach (var item in source) {
                action(item);
            }
        }
        public static IEnumerable<T> Log<T>(this IEnumerable<T> source) {
            foreach (var item in source) {
                UnityEngine.Debug.Log(item);
            }
            return source;
        }
        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, params T[] args) {
            return source.Except((IEnumerable<T>)args);
        }
        public static T RandomElement<T>(this IEnumerable<T> source) {
            Assert.IsNotNull(source, "Source must not be null.");
            if (source is not IReadOnlyList<T> elements) {
                elements = source.ToList();
            }
            return elements.Count == 0
                ? default
                : elements[random.Next(elements.Count)];
        }
        public static T RandomWeightedElement<T>(this IEnumerable<T> source, Func<T, int> weighting) {
            Assert.IsNotNull(source, "Source must not be null.");
            var weights = new Dictionary<T, int>();
            foreach (var element in source) {
                weights[element] = weighting(element);
            }
            return RandomWeightedElement(source, weights);
        }
        public static T RandomWeightedElement<T>(this IEnumerable<T> source, IReadOnlyDictionary<T, int> weights) {
            Assert.IsNotNull(source, "Source must not be null.");
            int totalWeight = source.CalculateTotalWeight(weights);
            int randomWeight = random.Next(totalWeight);
            foreach (var element in source) {
                if (weights[element] > randomWeight) {
                    return element;
                }
                randomWeight -= weights[element];
            }
            return default;
        }
        public static T RandomWeightedElementDescending<T>(this IEnumerable<T> source, Func<T, int> weighting) {
            Assert.IsNotNull(source, "Source must not be null.");
            var weights = new Dictionary<T, int>();
            foreach (var element in source) {
                weights[element] = weighting(element);
            }
            return source.RandomWeightedElementDescending(weights);
        }
        public static T RandomWeightedElementDescending<T>(this IEnumerable<T> source, IReadOnlyDictionary<T, int> weights) {
            Assert.IsNotNull(source, "Source must not be null.");
            int totalWeight = source.CalculateTotalWeight(weights);
            return RandomWeightedElement(source, key => totalWeight - weights[key]);
        }
        static int CalculateTotalWeight<T>(this IEnumerable<T> source, IReadOnlyDictionary<T, int> weights) {
            Assert.IsNotNull(source, "Source must not be null.");
            int totalWeight = 0;
            foreach (var key in source) {
                if (weights.TryGetValue(key, out int weight)) {
                    Assert.IsTrue(weight >= 0, "Weight must be positive or zero.");
                    totalWeight += weight;
                }
            }
            return totalWeight;
        }
        //https://stackoverflow.com/questions/1287567/is-using-random-and-orderby-a-good-shuffle-algorithm
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source) {
            var elements = source.ToArray();
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
        public static IEnumerable<Tuple<TFirst, TSecond>> Multiply<TFirst, TSecond>(this IEnumerable<TFirst> firstList, IEnumerable<TSecond> secondList) {
            foreach (var first in firstList) {
                foreach (var second in secondList) {
                    yield return Tuple.Create(first, second);
                }
            }
        }
        public static IEnumerable<TReturn> Multiply<TFirst, TSecond, TReturn>(this IEnumerable<TFirst> firstList, IEnumerable<TSecond> secondList, Func<TFirst, TSecond, TReturn> callback) {
            foreach (var first in firstList) {
                foreach (var second in secondList) {
                    yield return callback(first, second);
                }
            }
        }
        public static ISet<TSource> ToHashSet<TSource>(this IEnumerable<TSource> source) {
            var result = new HashSet<TSource>();
            foreach (var value in source) {
                if (!result.Contains(value)) {
                    result.Add(value);
                }
            }
            return result;
        }
        public static bool Equals<T>(this IEnumerable<T> source, IEnumerable<T> target) {
            if (source.Count() != target.Count()) {
                return false;
            }
            foreach (var element in source) {
                if (!target.Contains(element)) {
                    return false;
                }
            }
            return true;
        }
        public static IEnumerable<TSource> Without<TSource>(this IEnumerable<TSource> source, TSource item)
            where TSource : class {
            return source
                .Where(testItem => testItem != item);
        }
        public delegate bool TrySelectFunc<T>(out T output);
        public static IEnumerable<TTarget> TrySelect<TSource, TTarget>(
            this IEnumerable<TSource> source,
            Func<TSource, TrySelectFunc<TTarget>> selector) {
            foreach (var element in source) {
                if (selector(element)(out var result)) {
                    yield return result;
                }
            }
        }
    }
}
