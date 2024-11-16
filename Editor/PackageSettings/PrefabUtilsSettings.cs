using System;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Editor.PackageSettings {
    [Serializable]
    sealed class PrefabUtilsSettings {
        [Header("File system configuration")]
        [SerializeField, Tooltip("Root path to load prefabs from when using PrefabUtils.allPrefabs.")]
        internal string assetsFolder = "Assets/";

        [SerializeField, Tooltip("File extension of prefabs to load, if any.")]
        internal string prefabExtension = ".prefab";
    }
}