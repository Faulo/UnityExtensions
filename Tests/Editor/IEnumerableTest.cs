using NUnit.Framework;
using Slothsoft.UnityExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

public class IEnumerableTest {
    [Test]
    public void TestSelectNotNull() {
        IEnumerable<int> testArray = new int[] { 1, 2, 3, 4, 5 };

        var expected = new string[] { "1", "3", "5" };
        var result = testArray.SelectNotNull(i => (i % 2) == 0 ? null : i.ToString());

        Assert.AreEqual(expected, result);
    }
    [Test]
    public void TestForAll() {
        int testSum = 0;
        IEnumerable<int> testArray = new int[] { 1, 2, 3, 4, 5 };

        testArray.ForAll(i => testSum += i);

        Assert.AreEqual(testSum, testArray.Sum());
    }
    [Test]
    public void TestExcept() {
        IEnumerable<int> testArray = new int[] { 1, 2, 3, 4};

        Assert.AreEqual(6, testArray.Except(1, 3, 5).Sum());
    }
    [Test]
    public void TestRandomElement() {
        IEnumerable<int> testArray = new int[] { 1, 2, 3 };

        Assert.Contains(
            testArray.RandomElement(),
            new int[] { 1, 2, 3 }
        );
    }
    [Test]
    public void TestShuffle() {
        IEnumerable<int> testArray = new int[] { 1, 2, 3 };

        Assert.Contains(
            string.Join("", testArray.Shuffle()),
            new string[] { "123", "132", "213", "231", "312", "321" }
        );
    }
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


    private readonly int randomNumberIterations = 1000;
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
            Assert.Throws<ArgumentOutOfRangeException>(() => new object[1] { new object() }.RandomWeightedElement(o => -1));
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
    [Test]
    public void TestRandomWeightedElementFromLootTable() {
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
            results[table.Keys.RandomWeightedElement(table)]++;
        }

        foreach (var key in table.Keys) {
            Assert.AreEqual(table[key], probabilitySum * results[key] / (double)numberOfElements, 0.1);
        }
    }
    [Test]
    public void TestRandomWeightedElementDescending() {
        var weights = new Dictionary<int, int> {
            [0] = 1,
            [1] = 0,
            [2] = 2,
            [3] = 3
        };

        var l = new List<int>();

        int n = 1000 * 1000;

        for (int i = 0; i < n; i++) {
            l.Add(weights.Keys.RandomWeightedElementDescending(weights));
        }

        int a = l.Where(v => v == 1).Count();

        int b = l.Where(v => v == 0).Count();

        int c = l.Where(v => v == 2).Count();

        int d = l.Where(v => v == 3).Count();

        Assert.Greater(a, b);
        Assert.Greater(b, c);
        Assert.Greater(c, d);
    }
}
