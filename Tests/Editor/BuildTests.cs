using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Slothsoft.UnityExtensions.Editor;

namespace Slothsoft.UnityExtensions.Tests.Editor {
    [TestFixture(TestOf = typeof(Build))]
    sealed class BuildTests {
        IEnumerable<string> projectFiles => new[] {
            AssemblyInfo.NAMESPACE_RUNTIME,
            AssemblyInfo.NAMESPACE_EDITOR,
            AssemblyInfo.NAMESPACE_TESTS_RUNTIME,
            AssemblyInfo.NAMESPACE_TESTS_EDITOR
        }.Select(ns => ns + ".csproj");

        [Test]
        public void TestBuildSolution() {
            /*
            foreach (string file in projectFiles) {
                File.Delete(file);
            }
            //*/
            Build.Solution();

            foreach (string file in projectFiles) {
                FileAssert.Exists(file);
            }
        }
    }
}
