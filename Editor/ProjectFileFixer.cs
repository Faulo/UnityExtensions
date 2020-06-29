using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEditor;

namespace Slothsoft.UnityExtensions.Editor {
    [InitializeOnLoad]
    public class ProjectFileFixer {
        const string PROJECT_FILE_EXTENSION = ".csproj";
        const string NS_CSPROJ = "http://schemas.microsoft.com/developer/msbuild/2003";

        class Utf8StringWriter : StringWriter {
            public override Encoding Encoding => Encoding.UTF8;
        }
        static ProjectFileFixer() {
#if ENABLE_VSTU
            SyntaxTree.VisualStudio.Unity.Bridge.ProjectFilesGenerator.ProjectFileGeneration += ProjectFileGenerationListener;
#endif
        }
        static string ProjectFileGenerationListener(string fileName, string fileContent) {
            if (UnityExtensionsSettings.instance.projectFileSettingsEnabled) {
                var file = new FileInfo(fileName);
                if (file.Extension == PROJECT_FILE_EXTENSION) {
                    var projectDocument = XDocument.Parse(fileContent);

                    string assemblyName = projectDocument
                        .Descendants(XName.Get("AssemblyName", NS_CSPROJ))
                        .First()
                        .Value;

                    var settings = UnityExtensionsSettings.instance.ProjectFileSettingsForAssembly(assemblyName);
                    if (settings != default) {
                        foreach (var element in projectDocument.Descendants(XName.Get("LangVersion", NS_CSPROJ))) {
                            element.Value = settings.setCSharpVersionTo;
                        }
                        foreach (var element in projectDocument.Descendants(XName.Get("WarningLevel", NS_CSPROJ))) {
                            element.Value = settings.setWarningLevelTo;
                        }

                        using (var str = new Utf8StringWriter()) {
                            projectDocument.Save(str);
                            fileContent = str.ToString();
                        }
                    }
                }
            }
            return fileContent;
        }
    }
}