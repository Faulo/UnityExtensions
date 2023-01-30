using UnityEditor.Rendering;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Editor.RenderPipelineConversion {
    class RPMaterialUpgrader : MaterialUpgrader {
        readonly bool toHDRP;

        public RPMaterialUpgrader(bool toHDRP, string urpShaderName, string hdrpShaderName) {
            this.toHDRP = toHDRP;

            MapShader(urpShaderName, hdrpShaderName);
        }
        public override void Convert(Material srcMaterial, Material dstMaterial) {
            base.Convert(srcMaterial, dstMaterial);
        }

        protected void MapShader(string urpShader, string hdrpShader) {
            if (toHDRP) {
                RenameShader(urpShader, hdrpShader);
            } else {
                RenameShader(hdrpShader, urpShader);
            }
        }
        protected void MapTexture(string urpTexture, string hdrpTexture) {
            if (toHDRP) {
                RenameTexture(urpTexture, hdrpTexture);
            } else {
                RenameTexture(hdrpTexture, urpTexture);
            }
        }
        protected void MapColor(string urpColor, string hdrpColor) {
            if (toHDRP) {
                RenameColor(urpColor, hdrpColor);
            } else {
                RenameColor(hdrpColor, urpColor);
            }
        }
        protected void MapFloat(string urpFloat, string hdrpFloat) {
            if (toHDRP) {
                RenameFloat(urpFloat, hdrpFloat);
            } else {
                RenameFloat(hdrpFloat, urpFloat);
            }
        }
    }
}