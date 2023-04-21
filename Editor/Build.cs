using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.CodeEditor;
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

        public static void Solution() {
            var editor = default(VisualStudioEditor);

            if (editor is null) {
                editor = new VisualStudioEditor();

                var editorType = typeof(VisualStudioEditor);
                var installationType = editorType.Assembly.GetType(INSTALLATION_TYPE);
                var generatorField = editorType.GetField(GENERATOR_FIELD, BindingFlags.Instance | BindingFlags.NonPublic);
                var generator = generatorField.GetValue(editor) as ProjectGeneration;
                var installationField = typeof(ProjectGeneration).GetField(INSTALLATION_FIELD, BindingFlags.Instance | BindingFlags.NonPublic);
                var installation = new FakeVisualStudioInstallation(
                    installationType,
                    info => {
                        /*
                        return info.Name switch {
                            "Path" => "",
                            "SupportsAnalyzers" => false,
                            "LatestLanguageVersionSupported" => "10.0",
                            _ => throw new NotImplementedException(),
                        };
                        //*/
                        return default;
                    }
                );

                installationField.SetValue(generator, installation);
            }

            editor.SyncAll();
        }
        const string GENERATOR_FIELD = "_generator";
        const string INSTALLATION_TYPE = "Microsoft.Unity.VisualStudio.Editor.IVisualStudioInstallation";
        const string INSTALLATION_FIELD = "m_CurrentInstallation";

        /*
        interface IVisualStudioInstallation {
            string Path { get; }
            bool SupportsAnalyzers { get; }
            Version LatestLanguageVersionSupported { get; }
            string[] GetAnalyzers();
            CodeEditor.Installation ToCodeEditorInstallation();
        }
        //*/

        sealed class FakeVisualStudioInstallation : RealProxy, IRemotingTypeInfo {
            readonly Type _type;
            readonly Func<MethodInfo, object> _callback;

            public FakeVisualStudioInstallation(Type type, Func<MethodInfo, object> callback) : base(type) {
                _callback = callback;
                _type = type;
            }

            public override IMessage Invoke(IMessage msg) {
                if (msg is IMethodCallMessage call) {
                    var method = (MethodInfo)call.MethodBase;

                    return new ReturnMessage(_callback(method), null, 0, call.LogicalCallContext, call);
                }

                throw new NotSupportedException();
            }

            public bool CanCastTo(Type fromType, object o) => fromType == _type;

            public string TypeName { get; set; }
        }
    }
}