using NUnit.Framework;
using Slothsoft.UnityExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SerializableDictionaryTest {
    [Test]
    public void TestSerializable() {
        var dict = new SerializableDictionary<int, int>();

        dict[0] = 1;

        Assert.AreEqual(dict[0], 1);
    }
}
