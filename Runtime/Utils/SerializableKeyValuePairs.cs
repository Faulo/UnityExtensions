using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

namespace Slothsoft.UnityExtensions {
    [Serializable]
    public class SerializableKeyValuePairs<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>, ISerializationCallbackReceiver {
        [Serializable]
        struct Pair {
            [SerializeField, Expandable]
            public TKey key;
            [SerializeField, Expandable]
            public TValue value;
        }
        [SerializeField]
        Pair[] items = Array.Empty<Pair>();

        ConcurrentDictionary<TKey, TValue> dictionary {
            get {
                if (isDirty) {
                    isDirty = false;
                    LoadItems();
                }

                return dictionaryCache;
            }
        }
        ConcurrentDictionary<TKey, TValue> dictionaryCache = new();
        bool isDirty = true;

        public void OnBeforeSerialize() {
            isDirty = true;
        }
        public void OnAfterDeserialize() {
            isDirty = true;
        }

        public void SetItems(IReadOnlyDictionary<TKey, TValue> dictionary) {
            lock (dictionaryCache) {
                items = new Pair[dictionary.Count];
                int i = 0;
                foreach (var pair in dictionary) {
                    items[i].key = pair.Key;
                    items[i].value = pair.Value;
                    i++;
                }

                isDirty = true;
            }
        }

        void LoadItems() {
            lock (dictionaryCache) {
                dictionaryCache.Clear();
                for (int i = 0; i < items.Length; i++) {
                    dictionaryCache[items[i].key] = items[i].value;
                }
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