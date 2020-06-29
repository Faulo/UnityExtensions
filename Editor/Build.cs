using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Editor {
    public class Build : MonoBehaviour {
        static BuildPlayerOptions GetDefaultBuildPlayerOptions() {
            var type = typeof(BuildPlayerWindow.DefaultBuildMethods);
            var method = type.GetMethod("GetBuildPlayerOptionsInternal", BindingFlags.NonPublic | BindingFlags.Static);
            return (BuildPlayerOptions)method.Invoke(null, new object[] { false, new BuildPlayerOptions() });
        }

        static void BuildNow(BuildTarget target) {
            string path = Environment.GetCommandLineArgs().Last();

            var options = GetDefaultBuildPlayerOptions();
            options.locationPathName = path;
            options.target = target;

            BuildPipeline.BuildPlayer(options);

            EditorApplication.Exit(0);
        }

        public static void Win32() => BuildNow(BuildTarget.StandaloneWindows);
        public static void Win64() => BuildNow(BuildTarget.StandaloneWindows64);
        public static void WSA() => BuildNow(BuildTarget.WSAPlayer);
        public static void WebGL() => BuildNow(BuildTarget.WebGL);
        public static void Android() => BuildNow(BuildTarget.Android);
        public static void Apple() => BuildNow(BuildTarget.iOS);
    }
}