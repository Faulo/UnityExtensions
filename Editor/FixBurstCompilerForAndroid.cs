using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Slothsoft.UnityExtensions.Editor {
    public class FixBurstCompilerForAndroid : IPreprocessBuildWithReport {
        const string ANDROID_ENV_NDK = "ANDROID_NDK_ROOT";
        const string ANDROID_PREF_USE_EMBEDDED = "NdkUseEmbedded";
        const string ANDROID_PREF_PATH = "AndroidNdkRootR16b";

        public int callbackOrder { get { return 0; } }

        string ndkRoot => EditorPrefs.HasKey(ANDROID_PREF_USE_EMBEDDED) && !EditorPrefs.GetBool(ANDROID_PREF_USE_EMBEDDED)
            ? EditorPrefs.GetString(ANDROID_PREF_PATH)
            : Path.Combine(BuildPipeline.GetPlaybackEngineDirectory(BuildTarget.Android, BuildOptions.None), "NDK");

        public void OnPreprocessBuild(BuildReport report) {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(ANDROID_ENV_NDK))) {
                Environment.SetEnvironmentVariable(ANDROID_ENV_NDK, ndkRoot);
            }
        }
    }
}