using System;
using System.IO;
using System.Linq;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.Compilation;

namespace Slothsoft.UnityExtensions.Editor {
    public static class Build {
        static void BuildNow(BuildTarget target, BuildTargetGroup targetGroup) {
            string path = Environment.GetCommandLineArgs().Last();

            var options = new BuildPlayerOptions {
                locationPathName = path,
                target = target,
                targetGroup = targetGroup,
                scenes = EditorBuildSettings.scenes
                    .Where(scene => scene.enabled)
                    .Select(scene => scene.path)
                    .ToArray(),
            };

            var report = BuildPipeline.BuildPlayer(options);

            if (report.summary.result != BuildResult.Succeeded) {
                throw new Exception($"Build failed with result '{report.summary.result}', encountered{report.summary.totalWarnings} warnings and {report.summary.totalErrors} errors.");
            }

            EditorApplication.Exit(0);
        }

        public static void Linux() => BuildNow(BuildTarget.StandaloneLinux64, BuildTargetGroup.Standalone);
        public static void Win32() => BuildNow(BuildTarget.StandaloneWindows, BuildTargetGroup.Standalone);
        public static void Win64() => BuildNow(BuildTarget.StandaloneWindows64, BuildTargetGroup.Standalone);
        public static void WSA() => BuildNow(BuildTarget.WSAPlayer, BuildTargetGroup.WSA);
        public static void WebGL() => BuildNow(BuildTarget.WebGL, BuildTargetGroup.WebGL);
        public static void Android() => BuildNow(BuildTarget.Android, BuildTargetGroup.Android);
        public static void Apple() => BuildNow(BuildTarget.iOS, BuildTargetGroup.iOS);

        public static void Solution() {
            var generator = new ProjectGeneration();
            generator.Sync();

            string oldVersion = "<LangVersion>latest</LangVersion>";
            string newVersion = $"<LangVersion>{GetCSharpVersion()}</LangVersion>";
            foreach (var assembly in CompilationPipeline.GetAssemblies()) {
                var projectFile = new FileInfo(assembly.name + ".csproj");
                if (projectFile.Exists) {
                    string contents = File.ReadAllText(projectFile.FullName);
                    if (contents.Contains(oldVersion)) {
                        contents = contents.Replace(oldVersion, newVersion);
                        File.WriteAllText(projectFile.FullName, contents);
                    }
                }
            }
        }

        /// <summary>
        /// <see cref="Microsoft.Unity.VisualStudio.Editor.UnityInstallation"/>
        /// </summary>
        /// <returns></returns>
        internal static Version GetCSharpVersion() {
            var assembly = CompilationPipeline
                .GetAssemblies(AssembliesType.Player)
                .First(a => a.name == AssemblyInfo.NAMESPACE_RUNTIME);

#if UNITY_2020_2_OR_NEWER
            if (assembly.compilerOptions is ScriptCompilerOptions options && Version.TryParse(options.LanguageVersion, out var result)) {
                return result;
            }
            return new Version(8, 0);
#else
            return new Version(7, 3);
#endif
        }
    }
}