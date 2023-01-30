namespace Slothsoft.UnityExtensions.Editor.RenderPipelineConversion {
    sealed class ToLitMaterialUpgrader : RPMaterialUpgrader {
        public ToLitMaterialUpgrader(bool toHDRP, string urpShaderName, string hdrpShaderName) : base(toHDRP, urpShaderName, hdrpShaderName) {
            MapTexture("_BaseMap", "_BaseColorMap");
            //MapColor("_BaseColor", "_BaseColor");
            //MapFloat("_Smoothness", "_Smoothness");
            MapTexture("_EmissionMap", "_EmissiveColorMap");
            MapFloat("_Surface", "_SurfaceType");
        }
    }
}