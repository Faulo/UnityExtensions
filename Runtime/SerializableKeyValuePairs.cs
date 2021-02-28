using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slothsoft.UnityExtensions {
    [Serializable]
    public class SerializableKeyValuePairs<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>, ISerializationCallbackReceiver {
        [Serializable]
        struct Pair {
            public TKey key;
            public TValue value;
        }
        [SerializeField]
        Pair[] items = Array.Empty<Pair>();

        Dictionary<TKey, TValue> dictionary {
            get {
                if (dictionaryCache == null) {
                    LoadItems();
                }
                return dictionaryCache;
            }
        }
        Dictionary<TKey, TValue> dictionaryCache;

        public void OnBeforeSerialize() {
            dictionaryCache = null;
        }
        public void OnAfterDeserialize() {
            dictionaryCache = null;
        }

        public void SetItems(IReadOnlyDictionary<TKey, TValue> dictionary) {
            items = new Pair[dictionary.Count];
            int i = 0;
            foreach (var pair in dictionary) {
                items[i].key = pair.Key;
                items[i].value = pair.Value;
                i++;
            }
            dictionaryCache = null;
        }

        void LoadItems() {
            dictionaryCache = new Dictionary<TKey, TValue>();
            for (int i = 0; i < items.Length; i++) {
                dictionaryCache[items[i].key] = items[i].value;
            }
        }

        public IEnumerable<TKey> Keys => dictionary.Keys;
        public IEnumerable<TValue> Values => dictionary.Values;
        public int Count => dictionary.Count;
        public TValue this[TKey key] => dictionary[key];
        public bool ContainsKey(TKey key) => dictionary.ContainsKey(key);
        public bool TryGetValue(TKey key, out TValue value) => dictionary.TryGetValue(key, out value);
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => dictionary.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => dictionary.GetEnumerator();
    }
}