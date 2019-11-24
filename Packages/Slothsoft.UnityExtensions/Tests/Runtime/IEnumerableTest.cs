using NUnit.Framework;
using Slothsoft.UnityExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IEnumerableTest {
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
    [Test]
    public void TestRandomWeightedElement() {
        var weights = new Dictionary<int, int> {
            [0] = 1,
            [1] = 0,
            [2] = 2,
            [3] = 3
        };

        var l = new List<int>();

        int n = 1000 * 1000;

        for (int i = 0; i < n; i++) {
            l.Add(weights.Keys.RandomWeightedElement(weights));
        }

        int a = l.Where(v => v == 0).Count();

        int b = l.Where(v => v == 2).Count();

        int c = l.Where(v => v == 3).Count();

        int z = l.Where(v => v == 1).Count();

        Assert.AreEqual(0, z, "Entry with probability 0 should not happen ever.");

        Assert.AreEqual(n, a + b + c, "Sum of entries should match probability distribution.");

        Assert.Less(Math.Abs((double)b / a - 2), 0.1);

        Assert.Less(Math.Abs((double)c / a - 3), 0.1);
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

        int a = l.Where(v => v == 0).Count();

        int b = l.Where(v => v == 2).Count();

        int c = l.Where(v => v == 3).Count();

        int z = l.Where(v => v == 1).Count();

        Assert.AreEqual(n, z, "Entry with probability 0 should always.");

        Assert.AreEqual(n, a + b + c, "Sum of entries should match probability distribution.");

        Assert.Less(Math.Abs((double)b / a - 2), 0.1);

        Assert.Less(Math.Abs((double)c / a - 3), 0.1);
    }
}
