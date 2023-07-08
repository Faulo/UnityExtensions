using System;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Editor.PackageSettings {
    [Serializable]
    sealed class AndroidSettings {
        [SerializeField, Tooltip("Set Editor Pref for android building.")]
        internal bool fixBurstCompilerPathForAndroid = false;
    }
}