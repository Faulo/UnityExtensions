using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;

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
    }
}