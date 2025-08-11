Shader "Custom/AdvancedSkyBeams"
{
    Properties
    {
        [Header(Base Settings)]
        _LightDirection ("Light Direction", Vector) = (0, -1, 0, 0)
        _LightIntensity ("Light Intensity", Range(0, 10)) = 1.0
        _RayColor ("Ray Color", Color) = (1, 0.9, 0.8, 1)
        
        [Header(Beam Geometry)]
        _BeamCount ("Beam Count", Range(1, 20)) = 5
        _BeamSpread ("Beam Spread", Range(0, 180)) = 30
        _BeamSpacing ("Beam Spacing", Range(0, 1)) = 0.2
        _RayLength ("Ray Length", Range(0, 1)) = 0.5
        _RayWidth ("Ray Width", Range(0, 1)) = 0.2
        
        [Header(Visual Quality)]
        _RayDensity ("Ray Density", Range(0, 1)) = 0.5
        _RaySoftness ("Ray Softness", Range(0, 1)) = 0.5
        _RayBrightness ("Ray Brightness", Range(0, 5)) = 1.0
        _NoiseScale ("Noise Scale", Range(0, 1)) = 0.1
        _NoiseIntensity ("Noise Intensity", Range(0, 1)) = 0.2
        
        [Header(Advanced)]
        _BeamOffset ("Beam Offset", Range(0, 1)) = 0
        _BeamRotation ("Beam Rotation", Range(0, 360)) = 0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            Cull Off
            ZWrite Off
            ZTest Always

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            TEXTURE2D_X(_CameraColorTexture);
            SAMPLER(sampler_CameraColorTexture);
            
            // 基础属性
            float3 _LightDirection;
            float _LightIntensity;
            float4 _RayColor;
            
            // 光束几何
            float _BeamCount;
            float _BeamSpread;
            float _BeamSpacing;
            float _RayLength;
            float _RayWidth;
            
            // 视觉效果
            float _RayDensity;
            float _RaySoftness;
            float _RayBrightness;
            float _NoiseScale;
            float _NoiseIntensity;
            
            // 高级
            float _BeamOffset;
            float _BeamRotation;

            // 2D旋转函数
            float2 rotateUV(float2 uv, float rotation)
            {
                float rad = rotation * 3.14159265359 / 180.0;
                float sinRot = sin(rad);
                float cosRot = cos(rad);
                return float2(
                    uv.x * cosRot - uv.y * sinRot,
                    uv.x * sinRot + uv.y * cosRot
                );
            }

            // 噪声函数
            float noise(float2 uv)
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
            }

            // 单束光计算
            float calculateSingleBeam(float2 uv, float2 lightDir, float beamIntensity)
            {
                // 计算光线投影
                float beamProjection = dot(float3(uv, 0), float3(lightDir, 0));
                
                // 计算光线距离
                float beamDistance = saturate(beamProjection / _RayLength + 0.5);
                
                // 计算光线宽度衰减
                float widthAttenuation = saturate(1.0 - abs(uv.x * 2.0) / _RayWidth);
                
                // 添加噪声效果
                float beamNoise = noise(uv * _NoiseScale) * _NoiseIntensity;
                
                // 组合光束效果
                float beam = saturate(beamDistance * widthAttenuation * _RayDensity);
                beam = pow(beam, 1.0 - _RaySoftness);
                beam *= (1.0 + beamNoise);
                
                return beam * beamIntensity;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = v.uv;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                // 获取原始屏幕颜色
                half4 col = SAMPLE_TEXTURE2D_X(_CameraColorTexture, sampler_CameraColorTexture, i.uv);
                
                // 标准化UV坐标(-0.5到0.5)
                float2 uv = i.uv - 0.5;
                
                // 应用全局旋转
                uv = rotateUV(uv, _BeamRotation);
                
                // 标准化主光线方向
                float3 mainLightDir = normalize(_LightDirection);
                
                // 计算总光束强度
                float totalBeam = 0;
                
                // 计算光束分布
                float spreadRad = _BeamSpread * 3.14159265359 / 180.0;
                float angleStep = spreadRad / (_BeamCount - 1);
                float startAngle = -spreadRad * 0.5;
                
                // 生成多束光
                for (int j = 0; j < _BeamCount; j++)
                {
                    // 计算当前光束角度
                    float angle = startAngle + angleStep * j;
                    
                    // 计算光束方向
                    float2 beamDir = float2(
                        mainLightDir.x * cos(angle) - mainLightDir.y * sin(angle),
                        mainLightDir.x * sin(angle) + mainLightDir.y * cos(angle)
                    );
                    
                    // 计算光束位置偏移
                    float2 offsetUV = uv;
                    offsetUV.x += _BeamOffset * j * _BeamSpacing;
                    
                    // 计算光束强度衰减(边缘光束较弱)
                    float beamIntensity = 1.0 - abs(angle) / spreadRad * 0.5;
                    
                    // 累加光束效果
                    totalBeam += calculateSingleBeam(offsetUV, beamDir, beamIntensity);
                }
                
                // 平均光束强度
                totalBeam /= _BeamCount;
                
                // 应用颜色和亮度
                half3 beamColor = totalBeam * _RayColor.rgb * _RayBrightness * _LightIntensity;
                
                // 混合原始颜色和光束效果
                return half4(col.rgb + beamColor, col.a);
            }
            ENDHLSL
        }
    }
}