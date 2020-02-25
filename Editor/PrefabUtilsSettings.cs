using System;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Editor {
    [Serializable]
    internal class PrefabUtilsSettings {
        [Header("File system configuration")]
        [SerializeField, Tooltip("Root path to load prefabs from, if load.")]
        internal string assetsFolder = "Assets/";
        [SerializeField, Tooltip("File extension of prefabs to load, if any.")]
        internal string prefabExtension = ".prefab";
    }
}