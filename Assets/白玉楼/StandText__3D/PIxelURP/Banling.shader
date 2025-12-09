Shader "Custom/URP_Unlit_Transparent"
{
    Properties
    { 
        _MainTex ("Base Texture", 2D) = "white" {} 
                _DarkColor("_DarkColor", Color) = (0,0,0,0)    
                        _Blend("_Blend", Range(0,1)) = 0      
    }

    SubShader
    {
        // 渲染队列为透明，保证在透明物体渲染阶段被渲染
        Tags 
        { 
            "RenderType" = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
            "Queue" = "Transparent"
        }

        // 开启透明混合（SrcAlpha：源颜色透明度，OneMinusSrcAlpha：1 - 透明度）
        Blend SrcAlpha OneMinusSrcAlpha

        // 不写入深度（可选，根据需求，半透明物体通常不写入深度或选择性写入）
        ZWrite Off

        // 开启透明物体的深度测试（默认是 LEqual）
        ZTest LEqual

        // Pass：主渲染通道
        Pass
        {	
            Tags { "LightMode"="UniversalForward" }		
            HLSLPROGRAM  
            #pragma vertex vert
            #pragma fragment frag

            // 引入 URP 核心库，提供基础的顶点输入、纹理采样等功能
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"   
            
            // 顶点着色器输入：来自 Mesh 的数据
            struct Attributes
            {
                float4 positionOS : POSITION;  // 模型本地空间坐标
                float2 uv       : TEXCOORD0;   // 第一套 UV，用于贴图采样
            };

            // 顶点着色器输出 / 片段着色器输入
            struct Varyings
            {
                float4 positionHCS : SV_POSITION; // 裁剪空间坐标
                float2 uv : TEXCOORD0;           // UV 坐标
            }; 
            float _Blend;
            float4 _DarkColor;
            // 声明一个 2D 纹理（对应 Properties 中的 _MainTex ）
            TEXTURE2D(_MainTex);
            // 声明一个采样器（URP 推荐将纹理与采样器分开声明）
            SAMPLER(sampler_MainTex);
             
            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                
                // 将本地坐标转换到裁剪空间
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                // 直接传递 UV
                OUT.uv = IN.uv;
                return OUT;
            } 
            
            half4 frag(Varyings IN) : SV_Target
            {			
                // 采样主纹理
                half4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv); 
                
                 texColor.rgb=lerp(texColor.rgb,texColor.rgb*_DarkColor.rgb,_Blend);  
                // 直接返回纹理颜色（包含透明度）
                return texColor;
            }
            ENDHLSL
        }
    }

    // 如果所有 SubShader 都失败，回退到内置错误 Shader
    FallBack "Sprites/Default"
}