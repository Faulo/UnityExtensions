using NUnit.Framework;
using Slothsoft.UnityExtensions;


public class SerializableDictionaryTest {
    [Test]
    public void TestSerializable() {
        var dict = new SerializableDictionary<int, int> {
            [0] = 1
        };

        Assert.AreEqual(dict[0], 1);
    }
}
