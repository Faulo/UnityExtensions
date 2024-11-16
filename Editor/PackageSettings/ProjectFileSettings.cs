using System;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Editor.PackageSettings {
    [Serializable]
    sealed class ProjectFileSettings {
        enum WarningLevel {
            _0TurnOffAllWarnings = 0,
            _1DisplayOnlySevereWarnings = 1,
            _2DisplaySomeWarnings = 2,
            _3DiplayLotsOfWarnings = 3,
            _4DisplayAllWarnings = 4
        }
        [Header("Project file configuration")]
        [SerializeField, Tooltip("Assemblies to apply these settings to")]
        AssemblyDefinitionAsset[] affectedAssemblies = default;
        [SerializeField, Tooltip("Version number to write in <LangVersion>. Recommended: '7.3' for VS2019, 'latest' for VS2017")]
        internal string setCSharpVersionTo = "7.3";
        [SerializeField, Tooltip("Warning level to write in <WarningLevel>. Recommended: '4' for project, '0' for plugins")]
        WarningLevel m_setWarningLevelTo = WarningLevel._4DisplayAllWarnings;
        internal string setWarningLevelTo => ((int)m_setWarningLevelTo).ToString();

        internal bool Matches(string assemblyName) => affectedAssemblies.Any(assembly => assembly ? assembly.name == assemblyName : false);
    }
}