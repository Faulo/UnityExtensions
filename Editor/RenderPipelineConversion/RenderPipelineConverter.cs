using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

namespace Slothsoft.UnityExtensions.Editor.RenderPipelineConversion {
    public class RenderPipelineConverter : MonoBehaviour {
        static IEnumerable<MaterialUpgrader> GetMaterialUpgraders(bool toHDRP) {
            yield return new ToLitMaterialUpgrader(toHDRP, "Universal Render Pipeline/Lit", "HDRP/Lit");
            yield return new ToLitMaterialUpgrader(toHDRP, "Universal Render Pipeline/Unlit", "HDRP/Unlit");
        }
        static bool IsHDRPComponent(Component component) => component is UnityEngine.Rendering.HighDefinition.HDAdditionalCameraData || component is UnityEngine.Rendering.HighDefinition.HDAdditionalLightData || component is UnityEngine.Rendering.HighDefinition.HDAdditionalReflectionData;
        static bool IsURPComponent(Component component) => component is UnityEngine.Rendering.Universal.UniversalAdditionalCameraData || component is UnityEngine.Rendering.Universal.UniversalAdditionalLightData;
        [MenuItem("Edit/Render Pipeline/HDRP <=> URP Conversion/Upgrade materials to HDRP", priority = CoreUtils.editMenuPriority1)]
        internal static void UpgradeMaterialsHDRP() {
            UpgradeMaterials(true);
        }
        [MenuItem("Edit/Render Pipeline/HDRP <=> URP Conversion/Upgrade lights to HDRP", priority = CoreUtils.editMenuPriority1)]
        internal static void UpgradeLightsHDRP() {
            UpgradeLights(new LightUpgrader(true, UnityExtensionsSettings.instance.renderPipelineConversionSettings));
        }
        [MenuItem("Edit/Render Pipeline/HDRP <=> URP Conversion/Remove URP components from project", priority = CoreUtils.editMenuPriority1)]
        internal static void UpgradeComponentsHDRP() {
            RemoveComponents(IsURPComponent);
        }

        [MenuItem("Edit/Render Pipeline/HDRP <=> URP Conversion/Upgrade materials to URP", priority = CoreUtils.editMenuPriority2)]
        internal static void UpgradeMaterialsURP() {
            UpgradeMaterials(false);
        }
        [MenuItem("Edit/Render Pipeline/HDRP <=> URP Conversion/Upgrade lights to URP", priority = CoreUtils.editMenuPriority2)]
        internal static void UpgradeLightsURP() {
            UpgradeLights(new LightUpgrader(false, UnityExtensionsSettings.instance.renderPipelineConversionSettings));
        }
        [MenuItem("Edit/Render Pipeline/HDRP <=> URP Conversion/Remove HDRP components from project", priority = CoreUtils.editMenuPriority2)]
        internal static void UpgradeComponentsURP() {
            RemoveComponents(IsHDRPComponent);
        }

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