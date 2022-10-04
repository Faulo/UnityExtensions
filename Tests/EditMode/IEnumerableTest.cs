using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Slothsoft.UnityExtensions.Tests.EditMode {
    sealed class IEnumerableTest {
        #region SelectNotNull
        [Test]
        public void TestSelectNotNull() {
            IEnumerable<int> testArray = new int[] { 1, 2, 3, 4, 5 };

            string[] expected = new string[] { "1", "3", "5" };
            var result = testArray.SelectNotNull(i => (i % 2) == 0 ? null : i.ToString());

            Assert.AreEqual(expected, result);
        }
        #endregion

        #region ForAll
        [Test]
        public void TestForAll() {
            int testSum = 0;
            IEnumerable<int> testArray = new int[] { 1, 2, 3, 4, 5 };

            testArray.ForAll(i => testSum += i);

            Assert.AreEqual(testSum, testArray.Sum());
        }
        [Test]
        public void TestForAllReturnsCollection() {
            IEnumerable<int> testArray = new int[] { 1, 2, 3, 4, 5 };

            CollectionAssert.AreEqual(testArray, testArray.ForAll(i => { }));
        }
        #endregion

        #region Log
        static IEnumerable<string[]> logMessages = new string[][] {
            new string[0],
            new[] { "Hello", "World!" },
            new[] { "" },
        };
        [Test]
        public void TestLog([ValueSource(nameof(logMessages))] string[] messages) {

            for (int i = 0; i < messages.Length; i++) {
                LogAssert.Expect(LogType.Log, messages[i]);
            }

            messages.Log();
        }
        #endregion

        #region Except
        [Test]
        public void TestExcept() {
            IEnumerable<int> testArray = new int[] { 1, 2, 3, 4 };

            Assert.AreEqual(6, testArray.Except(1, 3, 5).Sum());
        }
        #endregion

        #region  RandomElement
        int[] testArray = new int[] { 1, 2, 3 };
        [Test]
        public void TestRandomElementWithArray() {
            Assert.Contains(
                testArray.RandomElement(),
                testArray
            );
        }
        [Test]
        public void TestRandomElementWithGenerator() {
            IEnumerable<int> testGenerator() {
                for (int i = 0; i < testArray.Length; i++) {
                    yield return testArray[i];
                }
            }

            Assert.Contains(
                testGenerator().RandomElement(),
                testArray
            );
        }
        [Test]
        public void TestRandomElementThrowsWhenNull() {
            Assert.Throws<UnityEngine.Assertions.AssertionException>(
                () => default(IEnumerable<object>).RandomElement(),
                "Source must not be null."
            );
        }
        [Test]
        public void TestRandomElementReturnsDefaultWhenEmpty() {
            Assert.AreEqual(null, Array.Empty<object>().RandomElement());
        }
        [Test]
        public void TestRandomElementReturnsZeroWhenEmpty() {
            Assert.AreEqual(0, Array.Empty<int>().RandomElement());
        }
        #endregion

        #region Shuffle
        [Test]
        public void TestShuffle() {
            IEnumerable<int> testArray = new int[] { 1, 2, 3 };

            Assert.Contains(
                string.Join("", testArray.Shuffle()),
                new string[] { "123", "132", "213", "231", "312", "321" }
            );
        }
        #endregion

        #region None
        [Test]
        public void TestNoneWithoutPredicate() {
            Assert.IsTrue(new int[] { }.None());
            Assert.IsFalse(new int[] { 1, 2, 3 }.None());
        }
        [Test]
        public void TestNoneWithPredicate() {
            Assert.IsFalse(new int[] { 0 }.None(i => i == 0));
            Assert.IsTrue(new int[] { 1 }.None(i => i == 0));
        }
        #endregion

        #region RandomWeightedElement
        readonly int randomNumberIterations = 1000;
        [Test]
        public void TestRandomWeightedElementThrowsWhenNull() {
            Assert.Throws<UnityEngine.Assertions.AssertionException>(
                () => default(IEnumerable<object>).RandomWeightedElement(key => 0),
                "Source must not be null."
            );
        }
        [Test]
        public void TestRandomWeightedElementFromListWith0Elements() {
            for (int i = 0; i < randomNumberIterations; i++) {
                Assert.IsNull(new object[0].RandomWeightedElement(o => 0));
            }
        }
        [Test]
        public void TestRandomWeightedElementFromListWith1ElementZeroChance() {
            for (int i = 0; i < randomNumberIterations; i++) {
                Assert.IsNull(new object[1] { new object() }.RandomWeightedElement(o => 0));
            }
        }
        [Test]
        public void TestRandomWeightedElementFromListWith1ElementNegativeChance() {
            for (int i = 0; i < randomNumberIterations; i++) {
                Assert.Throws<UnityEngine.Assertions.AssertionException>(
                    () => new object[1] { new object() }.RandomWeightedElement(o => -1),
                    "Weight must be positive or zero."
                );
            }
        }
        [Test]
        public void TestRandomWeightedElementFromListWith2ElementsZeroChance() {
            for (int i = 0; i < randomNumberIterations; i++) {
                Assert.AreEqual(1, new int[] { 0, 1 }.RandomWeightedElement(number => number));
            }
        }
        [Test]
        public void TestRandomWeightedElementFromListWithSuperfluousWeights() {
            for (int i = 0; i < randomNumberIterations; i++) {
                Assert.AreEqual(1, new int[] { 0, 1 }.RandomWeightedElement(
                    new Dictionary<int, int> { [0] = 0, [1] = 1, [2] = 2 }
                ));
            }
        }
        [TestCase(true)]
        [TestCase(false)]
        public void TestRandomWeightedElementFromLootTable(bool useLambda) {
            var table = new Dictionary<string, int> {
                ["Uncommon"] = 9,
                ["Common"] = 90,
                ["Mythic"] = 0,
                ["Rare"] = 1
            };
            var results = new Dictionary<string, int> {
                ["Common"] = 0,
                ["Uncommon"] = 0,
                ["Rare"] = 0,
                ["Mythic"] = 0
            };
            int probabilitySum = table.Values.Sum();
            int numberOfElements = 1000 * 1000;

            for (int j = 0; j < numberOfElements; j++) {
                if (useLambda) {
                    results[table.Keys.RandomWeightedElement(key => table[key])]++;
                } else {
                    results[table.Keys.RandomWeightedElement(table)]++;
                }
            }

            foreach (string key in table.Keys) {
                Assert.AreEqual(table[key], probabilitySum * results[key] / (double)numberOfElements, 0.1);
            }
        }
        #endregion

        #region RandomWeightedElementDescending
        [Test]
        public void TestRandomWeightedElementDescendingThrowsWhenNull() {
            Assert.Throws<UnityEngine.Assertions.AssertionException>(
                () => default(IEnumerable<object>).RandomWeightedElementDescending(key => 0),
                "Source must not be null."
            );
        }
        [TestCase(true)]
        [TestCase(false)]
        public void TestRandomWeightedElementDescending(bool useLambda) {
            var weights = new Dictionary<int, int> {
                [0] = 1,
                [1] = 0,
                [2] = 2,
                [3] = 3
            };

            var l = new List<int>();

            int n = 1000 * 1000;

            for (int i = 0; i < n; i++) {
                if (useLambda) {
                    l.Add(weights.Keys.RandomWeightedElementDescending(key => weights[key]));
                } else {
                    l.Add(weights.Keys.RandomWeightedElementDescending(weights));
                }
            }

            int a = l.Where(v => v == 1).Count();

            int b = l.Where(v => v == 0).Count();

            int c = l.Where(v => v == 2).Count();

            int d = l.Where(v => v == 3).Count();

            Assert.Greater(a, b);
            Assert.Greater(b, c);
            Assert.Greater(c, d);
        }
        #endregion

        #region ToHashSet
        [Test]
        public void TestToHashSet() {
            CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, new[] { 1, 2, 3, 4, 4, 1, 2 }.ToHashSet());
        }
        #endregion

        #region IsEquivalentTo
        [Test]
        public void TestIsEquivalentToTrue() {
            Assert.IsTrue(new[] { 1, 2, 3, 4 }.IsEquivalentTo(new[] { 4, 3, 2, 1 }));
        }
        [Test]
        public void TestIsEquivalentToFalseWrongCount() {
            Assert.IsFalse(new[] { 1, 2, 3, 4 }.IsEquivalentTo(new[] { 1, 2, 3, 4, 4 }));
        }
        [Test]
        public void TestIsEquivalentToFalseWrongElements() {
            Assert.IsFalse(new[] { 1, 2, 3, 4 }.IsEquivalentTo(new[] { 1, 2, 4, 4 }));
        }
        #endregion

        #region Without
        [Test]
        public void TestWithout() {
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, new[] { 1, 2, 3, 4, 4 }.Without(4));
        }
        #endregion

        #region TrySelect
        [Test]
        public void TestTrySelect() {
            CollectionAssert.AreEqual(
                new[] { 2, 4 },
                new[] { 1, 2, 3, 4 }
                    .TrySelect<int, int>(i => {
                        bool selector(out int output) {
                            output = i;
                            return i % 2 == 0;
                        }
                        return selector;
                    })
            );
        }
        #endregion
    }
}