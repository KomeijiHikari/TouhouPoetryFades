Shader "ORIS Shaders/my2D Light Shader URP4.0"
{
    Properties
    {
        [Toggle]_NoShader("No shader", Float) = 0
        _SpriteColor("Oirdin color", Color) = (1,1,1,1)
        [Header( BlurDepth)]
        [NoScaleOffset] _DepthTex("Depth Texture", 2D) = "gray" {}
        _Sen("Sen", float) = 0
        _Step("Step", Range(0,1)) = 0
        _DarkColor("DarkColor", Color) = (0,0,0,0)   /// 如果是解除，那么-1到2之外会锐化 黑白锐化
        _BlurBlend("BlurBlend", Range(0,1)) = 0     /// 模糊混合

        [Toggle]_DepthBlack("DepthAndBlur Black", Float) = 0
        [Space(15)]
        [Header(Gog)]
        [Space(15)]
        [Header(PixelLight)]
        /// 像素尺寸 W分量是开关
        _Size("Size", Vector) = (0,0,0,0)
        // 精灵图整体位置
        _Bouns("Bouns", Vector) = (0,0,0,0)
        [Space(15)]
        [Header(Depth_Light)]
        _DepthTexIntensity("DepthTex Intensity", Range(-0.99,1)) = 0
        _DepthTexOri("DepthTex Ori", Range(-1,1)) = 0
        [Toggle]_DepthTexDeb("Depth Tex Debug", Float) = 0

        [Space(15)][Header(Two part lighting)]
        _Threshold("Threshold", Range(0,1)) = 0
        _ThresholdNor("Threshold Nor", Range(0,1)) = 1
        [Space(10)]
        _UpIntensity("Up Intensity", Range(1,3)) = 1.2
        _UpLightPixel("Up LightPixel", Range(0,20)) = 15
        _DownIntensity("Down Intensity", Range(0,5)) = 0.9
        _DownLightPixel("Down LightPixel", Range(0,100)) = 50
        [Toggle]_TwoPartDeb("Two part color Debug", Float) = 0

        [Space(15)]
        _LerpDarkLight("Dark Or Light?", Range(0,1)) = 0
        [Space(15)]
        [HideInInspector] _X("x Texture", 2D) = "Black" {}
        [HideInInspector] _Y("Y Texture", 2D) = "Black" {}
        [HideInInspector] _Z("Z Texture", 2D) = "white" {}

                [HideInInspector] _BlurTexx("Normal Texture", 2D) = "white" {}
        [HideInInspector] _MainTex("Sprite Texture", 2D) = "white" {}
        [HideInInspector] _NorTex("Normal Texture", 2D) = "bump" {}
        [Toggle]_NormalDebug("Normal Debug", Float) = 0
        [Space(15)]
        _Way("Light Direction", Vector) = (0,1,0,0)
        _WayColor("WayColor", Color) = (1,1,1,1)
        _WayInt("Way Int", Float) = 0
        [Toggle]_NoWayDeb("No Way Debug", Float) = 0
        [Toggle]_WayDeb("Way Debug", Float) = 0
        [Space(15)]
        _Alpha(" Alpha", Range(0,1)) = 1
        _EdgeColor("Edge Color", Color) = (1,1,1,1)
        _NoColor("No Color", Range(0,1)) = 0
        [Space(15)]
        _NormalIntensity("Normal Intensity", Range(-10,10)) = 2
        _NormalPower("Normal Power", Range(0,30)) = 5
    }

    SubShader
    {
        LOD 0
        Tags {
            "RenderPipeline" = "UniversalPipeline"
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
        }
        Cull Off
        AlphaToMask On

        Pass
        {
            Name "Forward"
            Tags { "LightMode" = "UniversalForward" }
            Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
            ZWrite Off
            ZTest LEqual
            Offset 0, 0
            ColorMask RGBA

            HLSLPROGRAM
            #pragma target 3.0
            #pragma multi_compile_instancing
            #pragma multi_compile _ LOD_FADE_CROSSFADE
            #pragma multi_compile_fog
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile _ _SHADOWS_SOFT
            #pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE
            #pragma multi_compile _ DIRLIGHTMAP_COMBINED
            #pragma multi_compile _ LIGHTMAP_ON
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

            struct VertexInput
            {
                float4 vertex : POSITION;
                float3 ase_normal : NORMAL;
                float4 ase_tangent : TANGENT;
                float4 texcoord1 : TEXCOORD1;
                float4 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct VertexOutput
            {
                float W : TEXCOORD8;
                float4 clipPos : SV_POSITION;
                float4 lightmapUVOrVertexSH : TEXCOORD0;
                half4 fogFactorAndVertexLight : TEXCOORD1;
                #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
                float4 shadowCoord : TEXCOORD2;
                #endif
                float4 tSpace0 : TEXCOORD3;
                float4 tSpace1 : TEXCOORD4;
                float4 tSpace2 : TEXCOORD5;
                #if defined(ASE_NEEDS_FRAG_SCREEN_POSITION)
                float4 screenPos : TEXCOORD6;
                #endif
                float4 ase_texcoord7 : TEXCOORD7;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };

            float4 _MainTex_ST;
            float4 _NorTex_ST;
            float _NormalIntensity;
            float _NormalPower;
            float _NoShader;
            sampler2D _MainTex;
            sampler2D _NorTex;
            float4 _MainTex_TexelSize;
            float _BlurBlend;
            float4 _DarkColor;
            float4 _Size;
            float4 _Bouns;
            float _Sen;
            float _Step;
            float4 _DepthTex_TexelSize;
            sampler2D _BlurTexx;
            sampler2D _DepthTex;
            float _DepthBlack;
            float _SenS;
            sampler2D _X;
            sampler2D _Y;
            sampler2D _Z;
            float _NoWayDeb;
            float _WayDeb;
            float3 _Way;
            float4 _WayColor;
            float _WayInt;
            float _NormalDebug;
            float _TwoPartDeb;
            float _DepthTexIntensity;
            float _DepthTexOri;
            float _DepthTexDeb;
            float _LightDeb;
            float _Threshold;
            float _ThresholdNor;
            float _UpIntensity;
            float _DownIntensity;
            int _UpLightPixel;
            int _DownLightPixel;
            float4 _SpriteColor;
            float _LerpDarkLight;
            float4 _EdgeColor;
            float _NoColor;
              float    _Alpha;
            // OC 采样封装，保留
            float4 OC(sampler2D tex, float2 UV)
            {
                float4 c=tex2D(tex, UV) ;
                return float4(c.rgb* _SpriteColor.rgb,c.a);
            }

            // 定向光（半兰伯特风格）
            half DirectionalLight(half3 worldNormal, half3 lightDirection)
            {
                half3 normalizedLightDir = normalize(-lightDirection);
                half NdotL = dot(worldNormal, normalizedLightDir);
                return max((half)0, NdotL);
            }

            float LIght(half3 rgb)
            {
                half maxVal = max(rgb.r, max(rgb.g, rgb.b));
                return maxVal;
            }

            float3 HSLToRGB(float3 c)
            {
                float3 rgb = clamp(abs(fmod(c.x * 6.0 + float3(0.0,4.0,2.0), 6) - 3.0) - 1.0, 0, 1);
                rgb = rgb * rgb * (3.0 - 2.0 * rgb);
                return c.z * lerp(float3(1,1,1), rgb, c.y);
            }

            float3 RGBToHSL(float3 c)
            {
                float4 K = float4(0.0, -1.0/3.0, 2.0/3.0, -1.0);
                float4 p = lerp(float4(c.bg, K.wz), float4(c.gb, K.xy), step(c.b, c.g));
                float4 q = lerp(float4(p.xyw, c.r), float4(c.r, p.yzx), step(p.x, c.r));
                float d = q.x - min(q.w, q.y);
                float e = 1.0e-10;
                return float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
            }

            float Quantize(float v, int i, bool b = true)
            {
                if (i <= 0) return v;
                i = floor(i);
                return b ? round(v * i) / i : ceil(v * i) / i;
            }

            float Rangee(float x, float v)
            {
                return (x / (1 - v)) + v * 0.5;
            }

            // 像素描边检测，返回1表示边缘（尽量少乘法）
            half MiaoBian(half a, float2 uv)
            {
                if (a == 0)
                {
                    // 3x3 邻域采样 alpha
                    for (int ox = -1; ox <= 1; ox++)
                    {
                        for (int oy = -1; oy <= 1; oy++)
                        {
                            if (ox == 0 && oy == 0) continue;
                            float2 offset = float2(ox, oy) * _MainTex_TexelSize.xy;
                            half neighborA = tex2D(_MainTex, uv + offset).a;
                            if (neighborA > 0.01) return 1;
                        }
                    }
                }
                return 0;
            }
             

            half3 Pn_toN_Open(half3 value) { return value * 2 - 1; }
            half3 N_toPn_Zip(half3 value) { return (value + 1) * 0.5; }

            half get_z(half2 value)
            {
                half V = 1 - value.x * value.x - value.y * value.y;
                return V <= 0 ? (half)1 : sqrt(V);
            }

            float2 PixelPos(float2 pos)
            {
                if (_Size.w == 0) return pos;
                half2 Texsize = _Bouns.zw / _Size;  /// 一个像素块的世界坐标大小
                float2 OP = pos - _Bouns.xy;        /// 模型坐标
                float2 Int = floor(OP / Texsize);
                return Int * Texsize + _Bouns.xy;
            }

            float4 CalculateContrast(float contrastValue, float4 colorTarget)
            {
                float t = 0.5 * (1.0 - contrastValue);
                return mul(float4x4(contrastValue,0,0,t, 0,contrastValue,0,t, 0,0,contrastValue,t, 0,0,0,1), colorTarget);
            }

            VertexOutput VertexFunction(VertexInput v)
            {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.ase_texcoord7.xy = v.texcoord.xy;
                o.W = TransformObjectToHClip(v.vertex).z;
                float3 positionWS = TransformObjectToWorld(v.vertex.xyz);
                float4 positionCS = TransformWorldToHClip(positionWS);

                VertexNormalInputs normalInput = GetVertexNormalInputs(v.ase_normal, v.ase_tangent);
                o.tSpace0 = float4(normalInput.normalWS, positionWS.x);
                o.tSpace1 = float4(normalInput.tangentWS, positionWS.y);
                o.tSpace2 = float4(normalInput.bitangentWS, positionWS.z);
                OUTPUT_LIGHTMAP_UV(v.texcoord1, unity_LightmapST, o.lightmapUVOrVertexSH.xy);
                OUTPUT_SH(normalInput.normalWS.xyz, o.lightmapUVOrVertexSH.xyz);

                half3 vertexLight = VertexLighting(positionWS, normalInput.normalWS);
                #ifdef ASE_FOG
                half fogFactor = ComputeFogFactor(positionCS.z);
                #else
                half fogFactor = 0;
                #endif
                o.fogFactorAndVertexLight = half4(fogFactor, vertexLight);
                o.clipPos = positionCS;
                #if defined(ASE_NEEDS_FRAG_SCREEN_POSITION)
                o.screenPos = ComputeScreenPos(positionCS);
                #endif
                return o;
            }

            VertexOutput vert(VertexInput v) { return VertexFunction(v); }

            half4 frag(VertexOutput IN, half ase_vface : VFACE) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

                #if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
                #else
                half3 WorldNormal = normalize(IN.tSpace0.xyz);
                half3 WorldTangent = IN.tSpace1.xyz;
                half3 WorldBiTangent = IN.tSpace2.xyz;
                #endif

                half3 WorldPosition = half3(IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w);
                half3 WorldViewDirection = _WorldSpaceCameraPos.xyz - WorldPosition;
                WorldViewDirection = SafeNormalize(WorldViewDirection);

                float2 uv_MainTex = IN.ase_texcoord7.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                float4 tex2DNode1 = OC(_MainTex, uv_MainTex);

                float2 uv_NorTex = IN.ase_texcoord7.xy * _NorTex_ST.xy + _NorTex_ST.zw;
                // 合并法线采样，避免重复采样
                float4 norSample = tex2D(_NorTex, uv_NorTex);
                half3 unpackNormal = UnpackNormalScale(norSample, 1);
                unpackNormal.z = lerp(1, unpackNormal.z, saturate((half)0));

                half3 Normal = unpackNormal;

                // X/Y 法线通道替换（如果有更准确的法线贴图来源）
                float3 x = tex2D(_X, uv_MainTex);
                float3 y = tex2D(_Y, uv_MainTex);
                float3 WWz = tex2D(_Z, uv_MainTex);
                if (x.x * y.y != 1)
                {
                    half3 Mxy = half3(x.x, y.y, 1);
                    Mxy = Pn_toN_Open(Mxy);
                    Mxy.z = get_z(Mxy.xy);
                    Normal = Mxy;
                }
                if(_NoShader)return  tex2DNode1 ;
                if (_NormalDebug) { return half4(N_toPn_Zip(Normal), tex2DNode1.a); }

                InputData inputData;
                inputData.positionWS = WorldPosition;
                inputData.viewDirectionWS = WorldViewDirection;
                inputData.shadowCoord = float4(0,0,0,0);

                inputData.normalWS = TransformTangentToWorld(Normal, half3x3(WorldTangent, WorldBiTangent, WorldNormal));
                inputData.normalWS = NormalizeNormalPerPixel(inputData.normalWS); /// 归一化法线

                half DLInt = DirectionalLight(inputData.normalWS, _Way);
                if (_WayDeb == 1) { return half4(DLInt.xxx, tex2DNode1.a); }

                half3 DirectionColor = DLInt * _WayColor.rgb;
                DirectionColor = 1 - sqrt(1 - DirectionColor);
                if (_NoWayDeb) { DirectionColor = 0; }

                inputData.fogCoord = 0;
                inputData.bakedGI = 1;

                // 深度映射系数
                half W = 1 - Rangee(IN.W * 4, _Sen);  //_Sen 0-1变换 
                W = W * step(_Step, W);
                _BlurBlend *= W;
              if (_DepthBlack == 1) { return half4(W, W, W, tex2DNode1.a); }


                // 描边：当既不模糊又透明时才检测边缘
                if (_BlurBlend + tex2DNode1.a == 0)
                    return half4(MiaoBian(tex2DNode1.a, uv_MainTex) * _EdgeColor.rgb, _EdgeColor.a);

                // 像素光照
                inputData.positionWS.xy = PixelPos(inputData.positionWS);

 
                // 模糊
                float4 BlurColor  = OC(_BlurTexx, uv_MainTex );
                if(BlurColor .r*BlurColor .g*BlurColor .b*BlurColor .a==1)
                BlurColor  =OC(_MainTex, uv_MainTex );

                // 获取纯粹光照贴图
                half4 Lightcolor = UniversalFragmentPBR(inputData, half3(1,1,1), 0, 0.5, 0, 1, 0, 1);
                Lightcolor = sqrt(sqrt(Lightcolor)); // 扩展光源半径
                Lightcolor.rgb -= 1;
                Lightcolor.rgb *= step(0, Lightcolor.rgb);

                half3 H_DC = RGBToHSL(_DarkColor.rgb);
                half3 H_LC = RGBToHSL(Lightcolor.rgb);

                float4 Samplecolor = 0;
                half4 Outcolor = 0;
                int LightPixel = 50;

                // 深度贴图影响强度
                half ChangeIntensity = pow(WWz.x * 2, _DepthTexIntensity + 1);
                Lightcolor *= ChangeIntensity;
                Lightcolor *= 1 + _DepthTexOri;

                if (_DepthTexDeb == 1) { return half4(ChangeIntensity.xxx, tex2DNode1.a); }

                // 二分光照
                if (length(Lightcolor.rgb) >= 0.0001)
                {
                    half zero = step(_Threshold, Lightcolor.rgb);
                    LightPixel = 0;
                    if (zero == 1)
                    {
                        Lightcolor *= _UpIntensity;
                        LightPixel = _UpLightPixel;
                        half3 Houtc = RGBToHSL(Lightcolor.rgb);
                        Houtc.z = lerp(Houtc.z, (1 + _Threshold) * _UpIntensity, _ThresholdNor);
                        Lightcolor.rgb = HSLToRGB(Houtc);
                    }
                    else
                    {
                        Lightcolor *= _DownIntensity;
                        LightPixel = _DownLightPixel;
                    }
                }

                if (_TwoPartDeb == 1)
                {
                    if (H_LC.z < -0.0001) Lightcolor = half4(0,0.1,0,1);
                    else if (H_LC.z == 0) Lightcolor = half4(0,0,1,1);
                    else if (H_LC.z > 1) Lightcolor = Lightcolor * half4(4,1,1,1);
                    return half4(Lightcolor.rgb, tex2DNode1.a);
                }
                else if (H_LC.z < -0.0001)
                {
                    H_LC.z = 0;
                }

                half3 wos = 0;
                H_DC.z = (1 - H_DC.z);
                half zzz = step(0, H_LC.z);
                if (zzz == 1) wos = H_LC.z * Lightcolor.rgb;

                half3 DC = HSLToRGB(H_DC);
                DC.rgb = lerp(DC.rgb, 1 - (1 - DC.rgb) * (1 - wos), _LerpDarkLight);

                half3 HDirC = RGBToHSL(DirectionColor);
                DC.rgb = 1 - (1 - DC.rgb) * (1 - HDirC.z);

                Outcolor.rgb = (0.00000001 + DC.rgb) * (1 + Lightcolor.rgb) * pow(1 + DirectionColor.rgb, 1 + _WayInt);

                if (LightPixel != 0)
                {
                    half3 HslOut = RGBToHSL(Outcolor.rgb);
                    HslOut.z = Quantize(HslOut.z, LightPixel, false);
                    Outcolor.rgb = HSLToRGB(HslOut);
                }

                // 清晰和模糊混合
                BlurColor.rgb = lerp(tex2DNode1.rgb * Outcolor.rgb + Samplecolor.rgb, BlurColor.rgb * Outcolor.rgb + Samplecolor.rgb, _BlurBlend);

                // 去色
                float3 NOcolor = RGBToHSL(BlurColor.rgb).z;
                BlurColor.rgb = lerp(BlurColor.rgb, NOcolor, _NoColor);

                return half4(BlurColor.rgb, _Alpha*lerp(tex2DNode1.a, BlurColor.a, _BlurBlend));
            }

            ENDHLSL
        }
    }

    CustomEditor "UnityEditor.ShaderGraph.PBRMasterGUI"
    Fallback "Hidden/InternalErrorShader"
}