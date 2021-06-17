using System;
using System.Linq;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Editor.PackageSettings {
    [Serializable]
    public class CSharpSettings {
        [SerializeField, Tooltip("Add C# namespace based on assembly and folder hierarchy to every new .cs file.")]
        internal bool addNamespaceToCSharpFiles = true;

        [SerializeField, Tooltip("Rewrite the auto-generated .csproj files to add some features.")]
        internal bool rewriteProjectFiles = true;

        [SerializeField, Tooltip("Use the following options to adjust .csproj file generation.")]
        internal ProjectFileSettings[] projectFileSettings = new ProjectFileSettings[0];
        internal ProjectFileSettings ProjectFileSettingsForAssembly(string assemblyName) {
            return projectFileSettings
                .FirstOrDefault(settings => settings == null ? false : settings.Matches(assemblyName));
        }
    }
}