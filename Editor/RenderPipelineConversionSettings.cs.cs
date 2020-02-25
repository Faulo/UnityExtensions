using System;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Editor {
    [Serializable]
    internal class RenderPipelineConversionSettings {
        [Header("Lighting conversion")]
        [SerializeField, Range(0, 100), Tooltip("Multiplier to directional light intensity when converting URP => HDRP.")]
        internal float directionLightIntensityMultiplier = 5;
        [SerializeField, Range(0, 100), Tooltip("Multiplier to point light intensity when converting URP => HDRP.")]
        internal float pointLightIntensityMultiplier = 25;
        [SerializeField, Tooltip("Property to use when converting URP => HDRP.")]
        internal string lightIntensityProperty = "m_Intensity";
    }
}