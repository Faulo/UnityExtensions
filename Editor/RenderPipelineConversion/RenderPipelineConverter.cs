using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering.Universal;

namespace Slothsoft.UnityExtensions.Editor.RenderPipelineConversion {
    public class RenderPipelineConverter : EditorWindow {
        [MenuItem("Window/Render Pipeline/Slothsoft's HDRP <=> URP Conversion Wizard")]
        public static void ShowWindow() {
            GetWindow<RenderPipelineConverter>();
        }

        public void OnGUI() {
            titleContent = EditorGUIUtility.TrTextContent("HDRP <=> URP Conversion");

            GUILayout.BeginVertical("box");
            GUILayout.Label("Switch from URP to HDRP:");
            if (GUILayout.Button("Upgrade materials to HDRP")) {
                UpgradeMaterialsHDRP();
            }
            if (GUILayout.Button("Upgrade lights to HDRP")) {
                UpgradeLightsHDRP();
            }
            if (GUILayout.Button("Remove URP components from project")) {
                UpgradeComponentsHDRP();
            }
            GUILayout.EndVertical();

            GUILayout.BeginVertical("box");
            GUILayout.Label("Switch from HDRP to URP:");
            if (GUILayout.Button("Upgrade materials to URP")) {
                UpgradeMaterialsURP();
            }
            if (GUILayout.Button("Upgrade lights to URP")) {
                UpgradeLightsURP();
            }
            if (GUILayout.Button("Remove HDRP components from project")) {
                UpgradeComponentsURP();
            }
            GUILayout.EndVertical();
        }
        internal static void UpgradeMaterialsHDRP() {
            UpgradeMaterials(true);
        }
        internal static void UpgradeLightsHDRP() {
            UpgradeLights(new LightUpgrader(true, UnityExtensionsSettings.instance.renderPipelineConversionSettings));
        }
        internal static void UpgradeComponentsHDRP() {
            RemoveComponents(IsURPComponent);
        }

        internal static void UpgradeMaterialsURP() {
            UpgradeMaterials(false);
        }
        internal static void UpgradeLightsURP() {
            UpgradeLights(new LightUpgrader(false, UnityExtensionsSettings.instance.renderPipelineConversionSettings));
        }
        internal static void UpgradeComponentsURP() {
            RemoveComponents(IsHDRPComponent);
        }

        static IEnumerable<MaterialUpgrader> GetMaterialUpgraders(bool toHDRP) {
            yield return new ToLitMaterialUpgrader(toHDRP, "Universal Render Pipeline/Lit", "HDRP/Lit");
            yield return new ToLitMaterialUpgrader(toHDRP, "Universal Render Pipeline/Unlit", "HDRP/Unlit");
        }

        static bool IsHDRPComponent(Component component) => component is HDAdditionalCameraData || component is HDAdditionalLightData || component is HDAdditionalReflectionData;

        static bool IsURPComponent(Component component) => component is UniversalAdditionalCameraData || component is UniversalAdditionalLightData;


        static void UpgradeMaterials(bool toHDRP) {
            MaterialUpgrader.UpgradeProjectFolder(GetMaterialUpgraders(toHDRP).ToList(), "Upgrade materials");
        }
        static void UpgradeLights(LightUpgrader upgrader) {
            PrefabUtils.allPrefabs
                .SelectMany(prefab => prefab.GetComponents<Light>())
                .ForAll(upgrader.UpgradeLight);
        }
        static void RemoveComponents(Func<Component, bool> predicate) {
            PrefabUtils.allPrefabs.ForAll(prefab => TryToRemoveComponent(prefab, predicate));
            Assert.IsEmpty(PrefabUtils.allPrefabs.SelectMany(prefab => prefab.GetComponentsInChildren<Component>()).Where(predicate));
        }
        static void TryToRemoveComponent(GameObject prefab, Func<Component, bool> predicate) {
            if (PrefabUtility.IsAnyPrefabInstanceRoot(prefab)) {
                var components = PrefabUtility.GetAddedComponents(prefab)
                    .Select(addedComponent => addedComponent.instanceComponent)
                    .Where(predicate);

                if (components.Count() > 0) {
                    foreach (var component in components) {
                        Debug.Log("Removing " + component, prefab);
                        PrefabUtility.RevertAddedComponent(component, InteractionMode.AutomatedAction);
                    }
                    PrefabUtility.SavePrefabAsset(prefab);
                }
            } else {
                var components = prefab.GetComponentsInChildren<Component>()
                    .Where(predicate);

                if (components.Count() > 0) {
                    foreach (var component in components) {
                        Debug.Log("Removing " + component, prefab);
                        DestroyImmediate(component, true);
                    }
                    PrefabUtility.SavePrefabAsset(prefab);
                }
            }
        }
    }
}