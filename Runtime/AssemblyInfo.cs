using System.Runtime.CompilerServices;
using Slothsoft.UnityExtensions;

[assembly: InternalsVisibleTo(AssemblyInfo.NAMESPACE_EDITOR)]
[assembly: InternalsVisibleTo(AssemblyInfo.NAMESPACE_TESTS_RUNTIME)]
[assembly: InternalsVisibleTo(AssemblyInfo.NAMESPACE_TESTS_EDITOR)]

namespace Slothsoft.UnityExtensions {
    static class AssemblyInfo {
        public const string PACKAGE_ID = "net.slothsoft.unity-extensions";

        public const string NAMESPACE_RUNTIME = "Slothsoft.UnityExtensions";
        public const string NAMESPACE_EDITOR = "Slothsoft.UnityExtensions.Editor";
        public const string NAMESPACE_EDITOR_CONVERSION = "Slothsoft.UnityExtensions.Editor.RenderPipelineConversion";

        public const string NAMESPACE_TESTS_RUNTIME = "Slothsoft.UnityExtensions.Tests.Runtime";
        public const string NAMESPACE_TESTS_EDITOR = "Slothsoft.UnityExtensions.Tests.Editor";
    }
}