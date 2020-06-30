using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Editor {
    public class Build : MonoBehaviour {
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


            BuildPipeline.BuildPlayer(options);

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