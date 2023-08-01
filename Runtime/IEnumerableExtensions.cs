using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace Slothsoft.UnityExtensions {
    public static class IEnumerableExtensions {
        static readonly Random random = new Random();

        /// <summary>
        /// Like <see cref="Enumerable.Select"/>, but only return non-null values.
        /// </summary>
        public static IEnumerable<TResult> SelectNotNull<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector) {
            foreach (var item in source) {
                var result = selector(item);
                if (result != null) {
                    yield return result;
                }
            }
        }

        /// <summary>
        /// Call the supplied <paramref name="action"/> for each element in <paramref name="source"/>.
        /// </summary>
        public static IEnumerable<T> ForAll<T>(this IEnumerable<T> source, Action<T> action) {
            foreach (var item in source) {
                action(item);
            }

            return source;
        }

        /// <summary>
        /// Call <see cref="UnityEngine.Debug.Log"/> for each element in <paramref name="source"/>.
        /// </summary>
        public static IEnumerable<T> Log<T>(this IEnumerable<T> source) {
            foreach (var item in source) {
                UnityEngine.Debug.Log(item);
            }

            return source;
        }

        /// <summary>
        /// Params variant of <see cref="Enumerable.Except"/>.
        /// </summary>
        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, params T[] args) {
            return source.Except((IEnumerable<T>)args);
        }

        /// <summary>
        /// Picks a random element from <paramref name="source"/> and returns it.
        /// </summary>
        public static T RandomElement<T>(this IEnumerable<T> source) {
            Assert.IsNotNull(source, "Source must not be null.");
            if (!(source is IReadOnlyList<T> elements)) {
                elements = source.ToList();
            }

            return elements.Count == 0
                ? default
                : elements[random.Next(elements.Count)];
        }

        /// <summary>
        /// Picks a random element from <paramref name="source"/> and returns it, using the weights supplied by <paramref name="weighting"/>. The higher the weight, the more likely the element will be picked.
        /// </summary>
        public static T RandomWeightedElement<T>(this IEnumerable<T> source, Func<T, int> weighting) {
            Assert.IsNotNull(source, "Source must not be null.");
            var weights = new Dictionary<T, int>();
            foreach (var element in source) {
                weights[element] = weighting(element);
            }

            return RandomWeightedElement(source, weights);
        }

        /// <summary>
        /// Picks a random element from <paramref name="source"/> and returns it, using the weights supplied by <paramref name="weights"/>. The higher the weight, the more likely the element will be picked.
        /// </summary>
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

        /// <summary>
        /// Picks a random element from <paramref name="source"/> and returns it, using the weights supplied by <paramref name="weighting"/>. The higher the weight, the less likely the element will be picked.
        /// </summary>
        public static T RandomWeightedElementDescending<T>(this IEnumerable<T> source, Func<T, int> weighting) {
            Assert.IsNotNull(source, "Source must not be null.");
            var weights = new Dictionary<T, int>();
            foreach (var element in source) {
                weights[element] = weighting(element);
            }

            return source.RandomWeightedElementDescending(weights);
        }

        /// <summary>
        /// Picks a random element from <paramref name="source"/> and returns it, using the weights supplied by <paramref name="weights"/>. The higher the weight, the less likely the element will be picked.
        /// </summary>
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

        /// <summary>
        /// Returns all elements in <paramref name="source"/> in a random order. Uses the algorithm described in <seealso href="https://stackoverflow.com/questions/1287567/is-using-random-and-orderby-a-good-shuffle-algorithm"/>.
        /// </summary>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source) {
            var elements = source.ToArray();
            for (int i = elements.Length - 1; i >= 0; i--) {
                int swapIndex = random.Next(i + 1);
                yield return elements[swapIndex];
                elements[swapIndex] = elements[i];
            }
        }

        /// <summary>
        /// Inverse of <see cref="Enumerable.Any"/>.
        /// </summary>
        public static bool None<TSource>(this IEnumerable<TSource> source) {
            return !source.Any();
        }

        /// <summary>
        /// Inverse of <see cref="Enumerable.Any"/>.
        /// </summary>
        public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) {
            return !source.Any(predicate);
        }

        /// <summary>
        /// Merges two lists into one by creating a tuple of every combination of values.
        /// </summary>
        public static IEnumerable<Tuple<TFirst, TSecond>> Multiply<TFirst, TSecond>(this IEnumerable<TFirst> firstList, IEnumerable<TSecond> secondList) {
            foreach (var first in firstList) {
                foreach (var second in secondList) {
                    yield return Tuple.Create(first, second);
                }
            }
        }

        /// <summary>
        /// Merges two lists into one by calling <paramref name="callback"/> on every combination of values.
        /// </summary>
        public static IEnumerable<TReturn> Multiply<TFirst, TSecond, TReturn>(this IEnumerable<TFirst> firstList, IEnumerable<TSecond> secondList, Func<TFirst, TSecond, TReturn> callback) {
            foreach (var first in firstList) {
                foreach (var second in secondList) {
                    yield return callback(first, second);
                }
            }
        }

        /// <summary>
        /// Creates a unique set from <paramref name="source"/>.
        /// </summary>
        public static HashSet<TSource> ToHashSet<TSource>(this IEnumerable<TSource> source) {
            return new HashSet<TSource>(source);
        }

        /// <summary>
        /// Creates a dictionary from a list of tuples, using the first value of the tuple as key.
        /// </summary>
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<(TKey, TValue)> values) {
            return values.ToDictionary(value => value.Item1, value => value.Item2);
        }

        /// <summary>
        /// Checks whether every item in <paramref name="source"/> also exists in <paramref name="target"/>.
        /// </summary>
        public static bool IsEquivalentTo<T>(this IEnumerable<T> source, IEnumerable<T> target) {
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

        /// <summary>
        /// Creates a list with all elements from <paramref name="source"/> except for <paramref name="item"/>.
        /// </summary>
        public static IEnumerable<TSource> Without<TSource>(this IEnumerable<TSource> source, TSource item) {
            return source
                .Where(testItem => !testItem.Equals(item));
        }

        public delegate bool TrySelectFunc<T>(out T output);
        /// <summary>
        /// Calls <paramref name="selector"/> on every item in <paramref name="source"/> and returns its output, if succesful.
        /// </summary>
        public static IEnumerable<TTarget> TrySelect<TSource, TTarget>(this IEnumerable<TSource> source, Func<TSource, TrySelectFunc<TTarget>> selector) {
            foreach (var element in source) {
                if (selector(element)(out var result)) {
                    yield return result;
                }
            }
        }
    }
}