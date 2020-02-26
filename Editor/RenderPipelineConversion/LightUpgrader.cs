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
                var serializedLight = new SerializedObject(light);
                var intensityProperty = serializedLight.FindProperty(settings.lightIntensityProperty);
                intensityProperty.floatValue = ScaleIntensity(intensityProperty.floatValue, light.type);
                serializedLight.ApplyModifiedPropertiesWithoutUndo();
            } else {
                var serializedLight = light.gameObject;
                var intensityProperties = properties.Where(property => property.propertyPath == settings.lightIntensityProperty);
                if (intensityProperties.Count() > 0) {
                    intensityProperties.ForAll(property => property.value = ScaleIntensity(property.value, light.type));
                    PrefabUtility.SetPropertyModifications(serializedLight, properties);
                    PrefabUtility.SavePrefabAsset(serializedLight);
                }
            }
        }

        string ScaleIntensity(string value, LightType type) => ScaleIntensity(float.Parse(value, CultureInfo.InvariantCulture), type).ToString(CultureInfo.InvariantCulture);

        float ScaleIntensity(float value, LightType type) {
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