using System.Runtime.CompilerServices;
using Slothsoft.UnityExtensions;

[assembly: InternalsVisibleTo(AssemblyInfo.NAMESPACE_EDITOR)]
[assembly: InternalsVisibleTo(AssemblyInfo.NAMESPACE_TESTS_PLAYMODE)]
[assembly: InternalsVisibleTo(AssemblyInfo.NAMESPACE_TESTS_EDITMODE)]

namespace Slothsoft.UnityExtensions {
    static class AssemblyInfo {
        public const string NAMESPACE_RUNTIME = "Slothsoft.UnityExtensions";
        public const string NAMESPACE_EDITOR = "Slothsoft.UnityExtensions.Editor";
        public const string NAMESPACE_TESTS_PLAYMODE = "Slothsoft.UnityExtensions.Tests.PlayMode";
        public const string NAMESPACE_TESTS_EDITMODE = "Slothsoft.UnityExtensions.Tests.EditMode";
    }
}