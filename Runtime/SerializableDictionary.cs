using System.Collections.Generic;
using UnityEngine;

namespace Slothsoft.UnityExtensions {
    /// <summary>
    /// A <see cref="Dictionary&lt;TKey, TValue>"/>, but serializable.
    /// 
    /// Taken from <a href="https://answers.unity.com/questions/460727/how-to-serialize-dictionary-with-unity-serializati.html">the Internet</a>.
    /// </summary>
    /// <typeparam name="TKey"><seealso cref="Dictionary&lt;TKey, TValue>"/></typeparam>
    /// <typeparam name="TValue"><seealso cref="Dictionary&lt;TKey, TValue>"/></typeparam>
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver {
        [SerializeField]
        List<TKey> keys = new List<TKey>();

        [SerializeField]
        List<TValue> values = new List<TValue>();

        // save the dictionary to lists
        public void OnBeforeSerialize() {
            keys.Clear();
            values.Clear();
            foreach (var pair in this) {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }

        // load dictionary from lists
        public void OnAfterDeserialize() {
            Clear();

            while (keys.Count < values.Count) {
                keys.Add(default);
            }
            while (values.Count < keys.Count) {
                values.Add(default);
            }

            if (keys.Count != values.Count) {
                throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable.", keys.Count, values.Count));
            }

            for (int i = 0; i < keys.Count; i++) {
                Add(keys[i], values[i]);
            }
        }
    }
}