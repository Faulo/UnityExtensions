using System.Globalization;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Editor.RenderPipelineConversion {
    internal class LightUpgrader {
        readonly bool toHDRP;
        readonly RenderPipelineConversionSettings settings;

        internal LightUpgrader(bool toHDRP, RenderPipelineConversionSettings settings) {
            this.toHDRP = toHDRP;
            this.settings = settings;
        }

        public void UpgradeLight(Light light) {
            var properties = PrefabUtility.GetPropertyModifications(light);
            if (properties == null) {
                light.intensity = ScaleIntensity(light.type, light.intensity);
            } else {
                var intensityProperty = properties.FirstOrDefault(property => property.propertyPath == settings.lightIntensityProperty);
                if (intensityProperty != null) {
                    intensityProperty.value = ScaleIntensity(light.type, float.Parse(intensityProperty.value, CultureInfo.InvariantCulture)).ToString(CultureInfo.InvariantCulture);
                    PrefabUtility.SetPropertyModifications(light, properties);
                    PrefabUtility.SavePrefabAsset(light.gameObject);
                }
            }
        }

        float ScaleIntensity(LightType type, float value) {
            switch (type) {
                case LightType.Directional:
                    return toHDRP
                        ? value * settings.directionalLightIntensityMultiplier
                        : value / settings.directionalLightIntensityMultiplier;
                case LightType.Point:
                    return toHDRP
                        ? value * settings.pointLightIntensityMultiplier
                        : value / settings.pointLightIntensityMultiplier;
                default:
                    throw new System.Exception("LightType " + type + " is not supported by this implementation.");
            }
        }
    }
}