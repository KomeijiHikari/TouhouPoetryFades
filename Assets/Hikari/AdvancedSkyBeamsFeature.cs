using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AdvancedSkyBeamsFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class Settings
    {
        [Header("Base Settings")]
        public Material material = null;
        public Vector3 lightDirection = new Vector3(0, -1, 0);
        [Range(0, 10)] public float lightIntensity = 1.0f;
        public Color rayColor = new Color(1, 0.9f, 0.8f, 1);

        [Header("Beam Geometry")]
        [Range(1, 20)] public int beamCount = 5;
        [Range(0, 180)] public float beamSpread = 30f;
        [Range(0, 1)] public float beamSpacing = 0.2f;
        [Range(0, 1)] public float rayLength = 0.5f;
        [Range(0, 1)] public float rayWidth = 0.2f;

        [Header("Visual Quality")]
        [Range(0, 1)] public float rayDensity = 0.5f;
        [Range(0, 1)] public float raySoftness = 0.5f;
        [Range(0, 5)] public float rayBrightness = 1.0f;
        [Range(0, 1)] public float noiseScale = 0.1f;
        [Range(0, 1)] public float noiseIntensity = 0.2f;

        [Header("Advanced")]
        [Range(0, 1)] public float beamOffset = 0f;
        [Range(0, 360)] public float beamRotation = 0f;
    }

    public Settings settings = new Settings();

    class SkyBeamsRenderPass : ScriptableRenderPass
    {
        public Material material;
        public Settings settings;

        private RenderTargetIdentifier source;
        private RenderTargetHandle tempTexture;

        public void Setup(RenderTargetIdentifier source)
        {
            this.source = source;
        }

        public SkyBeamsRenderPass(Material material, Settings settings)
        {
            this.material = material;
            this.settings = settings;
            tempTexture.Init("_TempSkyBeamsTexture");
            renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            cmd.GetTemporaryRT(tempTexture.id, cameraTextureDescriptor);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (material == null || renderingData.cameraData.isSceneViewCamera) return;

            CommandBuffer cmd = CommandBufferPool.Get("AdvancedSkyBeams");

            // 设置材质参数
            material.SetVector("_LightDirection", settings.lightDirection);
            material.SetFloat("_LightIntensity", settings.lightIntensity);
            material.SetColor("_RayColor", settings.rayColor);

            material.SetFloat("_BeamCount", settings.beamCount);
            material.SetFloat("_BeamSpread", settings.beamSpread);
            material.SetFloat("_BeamSpacing", settings.beamSpacing);
            material.SetFloat("_RayLength", settings.rayLength);
            material.SetFloat("_RayWidth", settings.rayWidth);

            material.SetFloat("_RayDensity", settings.rayDensity);
            material.SetFloat("_RaySoftness", settings.raySoftness);
            material.SetFloat("_RayBrightness", settings.rayBrightness);
            material.SetFloat("_NoiseScale", settings.noiseScale);
            material.SetFloat("_NoiseIntensity", settings.noiseIntensity);

            material.SetFloat("_BeamOffset", settings.beamOffset);
            material.SetFloat("_BeamRotation", settings.beamRotation);

            // 应用后处理效果
            cmd.Blit(source, tempTexture.Identifier(), material, 0);
            cmd.Blit(tempTexture.Identifier(), source);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(tempTexture.id);
        }
    }

    SkyBeamsRenderPass m_ScriptablePass;

    public override void Create()
    {
        m_ScriptablePass = new SkyBeamsRenderPass(settings.material, settings);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (settings.material == null)
        {
            Debug.LogWarning("Advanced Sky Beams material not set");
            return;
        }

        m_ScriptablePass.Setup(renderer.cameraColorTarget);
        renderer.EnqueuePass(m_ScriptablePass);
    }
}