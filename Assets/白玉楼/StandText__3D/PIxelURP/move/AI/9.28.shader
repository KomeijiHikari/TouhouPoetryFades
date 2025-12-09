Shader "Optimized/ORIS_2DLight_URP"
{
    Properties
    {
        [Toggle]_NoShader("No Shader", Float) = 0
        _MainTex("Main Texture", 2D) = "white" {}
        _NorTex("Normal Texture", 2D) = "bump" {}
        _Ocolor("Color Tint", Color) = (1,1,1,1)
        
        // 光照参数
        _Way("Light Direction", Vector) = (0,1,0,0)
        _WayColor("Light Color", Color) = (1,1,1,1)
        _WayInt("Light Intensity", Float) = 1
        [Toggle]_WayDeb("Debug Light", Float) = 0
        
        // 模糊参数
        _BlurBlend("Blur Blend", Range(0, 1)) = 0
        _BlurAmount("Blur Amount", Range(0, 5)) = 1
        
        // 材质参数
        _Metallic("Metallic", Range(0, 1)) = 0
        _Smoothness("Smoothness", Range(0, 1)) = 0.5
        _EmissivePower("Emissive Power", Range(0, 5)) = 1
        _EmissiveColor("Emissive Color", Color) = (0,0,0,1)
        
        // 高级选项
        [Toggle]_NormalDebug("Debug Normals", Float) = 0
        [Toggle]_DepthDebug("Debug Depth", Float) = 0
    }

    SubShader
    {
        Tags {
            "RenderPipeline"="UniversalPipeline"
            "RenderType"="Transparent"
            "Queue"="Transparent"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        HLSLINCLUDE
        #pragma target 3.0
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);
        TEXTURE2D(_NorTex);
        SAMPLER(sampler_NorTex);
        
        CBUFFER_START(UnityPerMaterial)
        float4 _MainTex_ST;
        float4 _NorTex_ST;
        float4 _Ocolor;
        float4 _WayColor;
        float4 _EmissiveColor;
        float3 _Way;
        float _WayInt;
        float _BlurBlend;
        float _BlurAmount;
        float _Metallic;
        float _Smoothness;
        float _EmissivePower;
        float _NoShader;
        float _WayDeb;
        float _NormalDebug;
        float _DepthDebug;
        float4 _MainTex_TexelSize;
        CBUFFER_END

        // 优化后的光照计算
        half3 DirectionalLightOptimized(half3 normal, half3 lightDir)
        {
            half3 NdotL = dot(normal, normalize(-lightDir));
            return saturate(NdotL) * _WayInt;
        }

        // 优化后的模糊算法
        half4 OptimizedBlur(float2 uv, float blurAmount)
        {
            half4 color = 0;
            half2 texelSize = _MainTex_TexelSize.xy * blurAmount;
            half weightSum = 0;
            
            // 3x3高斯核采样
            const int samples = 1;
            for (int y = -samples; y <= samples; y++)
            {
                for (int x = -samples; x <= samples; x++)
                {
                    half2 offset = half2(x, y) * texelSize;
                    half weight = exp(-(x*x + y*y) / 4.0);
                    color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv + offset) * weight;
                    weightSum += weight;
                }
            }
            
            return color / weightSum;
        }

        // 获取法线
        half3 GetNormal(float2 uv)
        {
            #ifdef _NORMALMAP
                half4 normalData = SAMPLE_TEXTURE2D(_NorTex, sampler_NorTex, uv);
                return UnpackNormalScale(normalData, 1.0);
            #else
                return half3(0, 0, 1);
            #endif
        }
        ENDHLSL

        Pass
        {
            Tags { "LightMode"="UniversalForward" }
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ _NORMALMAP

            struct VertexInput
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
            };

            struct VertexOutput
            {
                float4 clipPos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normalWS : TEXCOORD1;
                float3 viewDirWS : TEXCOORD2;
                float4 shadowCoord : TEXCOORD3;
            };

            VertexOutput vert(VertexInput v)
            {
                VertexOutput o;
                o.clipPos = TransformObjectToHClip(v.vertex.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                
                o.normalWS = TransformObjectToWorldNormal(v.normal);
                o.viewDirWS = GetWorldSpaceNormalizeViewDir(TransformObjectToWorld(v.vertex.xyz));
                
                #ifdef _MAIN_LIGHT_SHADOWS
                    o.shadowCoord = TransformWorldToShadowCoord(TransformObjectToWorld(v.vertex.xyz));
                #else
                    o.shadowCoord = float4(0, 0, 0, 0);
                #endif
                
                return o;
            }

            half4 frag(VertexOutput i) : SV_Target
            {
                if (_NoShader > 0.5)
                {
                    return SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv) * _Ocolor;
                }
                
                // 采样基础纹理
                half4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv) * _Ocolor;
                
                // 获取法线
                half3 normal = GetNormal(i.uv);
                
                // 光照计算
                half3 lighting = DirectionalLightOptimized(normal, _Way.xyz);
                lighting *= _WayColor.rgb;
                
                // 应用光照
                half4 finalColor = texColor * half4(lighting, 1.0);
                
                // 应用模糊
                if (_BlurBlend > 0)
                {
                    half4 blurColor = OptimizedBlur(i.uv, _BlurAmount);
                    finalColor = lerp(finalColor, blurColor, _BlurBlend);
                }
                
                // 自发光
                finalColor.rgb += _EmissiveColor.rgb * _EmissivePower;
                
                // 调试模式
                #if defined(_NORMALDEBUG)
                    return half4(normal * 0.5 + 0.5, texColor.a);
                #elif defined(_WAYDEB)
                    return half4(lighting.xxx, texColor.a);
                #elif defined(_DEPTHDEBUG)
                    half depth = i.clipPos.z / i.clipPos.w;
                    return half4(depth.xxx, texColor.a);
                #endif
                
                return finalColor;
            }
            ENDHLSL
        }
         
    }
    Fallback "Universal Render Pipeline/SimpleLit"
}