
Shader "ORIS Shaders/my2D Light Shader URP"
{
	Properties
	{
		[Toggle]_NoShader("No shader ", Float) = 0
		_Ocolor(" Oirdin  color", Color) = (1,1,1,1)
			   [Header( BlurDepth)]   
			[NoScaleOffset] _DepthTex("Depth Texture", 2D) = "gray" {} 
		_Sen("Sen", float) = 0 
		_Step("Step", Range( 0 , 1)) = 0 
		 _DarkColor("DarkColor", Color) = (0,0,0,0)   ///如果是 解除   那么-1到2 之外会锐化  黑白锐化
		_BlurBlend("BlurBlend", Range( 0 , 1)) = 0 ///如果是 解除   那么-1到2 之外会锐化  黑白锐化
		
		 
			[Toggle]_DepthBlack(" Debug DepthAndBlur   Black", Float) = 0
		  [Space(15)]
			   [Header(Gog)]   
//		  _FogColor("FogColor", Color) = (0.5,0.5,0.5,1) 

		[Space(15)]
		 [Header(PixelLight)]
				/// 像素尺寸     W分量 是开关
		  _Size ("Size ",Vector) = (0,0,0,0)  
		//精灵图位置整体信息
			_Bouns ("Bouns ",Vector) = (0,0,0,0) 
	  [Space(15)]
			   [Header(Depth_Light)]  
						 _DepthTexIntensity("DepthTex  Intensity",  Range( -0.99 , 1)) = 0
						 _DepthTexOri("DepthTex   Ori", Range( -1,1 )) = 0
							[Toggle]_DepthTexDeb("Depth  Tex  Debug", Float) = 0
		
			  [Space(15)][Header(Two part lighting)]  
			 _Threshold("Threshold", Range( 0,1)) = 0
				 _ThresholdNor("Threshold  Nor", Range( 0,1)) = 1
		[Space( 10)]		///阀值 好的  不好的
			 _UpIntensity("Up Intensity ", Range( 1,3)) = 1.2
		_UpLightPixel("Up LightPixel ",Range( 0,20)) = 15 
			 _DownIntensity("Down	Intensity", Range( 0,5)) = 0.9
		 _DownLightPixel("Down LightPixel ",Range( 0,100)) = 50
			[Toggle]_TwoPartDeb("Two  part color  Debug", Float) = 0
		  [Space(15)]
		   [Header(BlendSadowAndLight)]
		 _SampleLight(" Sample Light", Float) = 0
		
		
			  [Space(15)]

		 _LerpDarkLight(" Dark  Or   Light?", Range( 0,1)) = 0
			  [Space(15)] 
		  _AlphaCutoff("Alpha Cutoff ", Range(0, 1)) = 0.5
			[HideInInspector] _X("x Texture", 2D) ="Black" {}
			[HideInInspector] _Y("Y Texture", 2D) = "Black" {}
			[HideInInspector] _Z("Z Texture", 2D) = "white" {}
		
		
			[HideInInspector] _MainTex("Sprite Texture", 2D) = "white" {}
			[HideInInspector]_NorTex("Normal Texture", 2D) = "bump" {}
				[Toggle]_NormalDebug("Normal  Debug", Float) = 0
			 [Space(15)]
			  
		    _Way ("Light Direction", Vector) = (0,1,0,0)
				_WayColor("WayColor", Color) = (1,1,1,1)
				_WayInt("Way   Int  ", Float) = 0
			[Toggle]_NoWayDeb(" Debug NO  Way  ", Float) = 0
				[Toggle]_WayDeb(" Debug Way  ", Float) = 0
		 [Space(15)] 
		[Toggle]_BlackWhite("Black&White", Float) = 0
		_R("R", Range( 0 , 5)) = 2
		_G("G", Range( 0 , 5)) = 2
		_B("B", Range( 0 , 5)) = 2
		_Contrast("Contrast", Range( 0 , 5)) = 1
		_NormalIntensity("Normal Intensity", Range( -10 , 10)) = 2
		_NormalPower("Normal Power", Range( 0 , 30)) = 5
		_LightAffection("Light Affection", Range( -30 , 20)) = 2
		_EmisionPower("Emision Power", Range( -10 , 10)) = 0
		_EmisionColor("Emision Color", Color) = (1,1,1,1)
		_Offset("Offset", Range( 0.01 , 1)) = 1
		_Thickness("Thickness", Range( 0.01 , 0.5)) = 0.5
		[ASEEnd][Toggle(_SOLIDEMISION_ON)] _SolidEmision("Solid Emision", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {} 


	}

	SubShader
	{
		LOD 0 
		Tags { 
			"RenderPipeline"="UniversalPipeline"
		"RenderType"="Transparent" 
		"Queue"="Transparent"
		}
		Cull Off
		AlphaToMask On
		HLSLINCLUDE
		#pragma target 3.0

		#ifndef ASE_TESS_FUNCS
		#define ASE_TESS_FUNCS
		float4 FixedTess( float tessValue )
		{
			return tessValue;
		}
		
		float CalcDistanceTessFactor (float4 vertex, float minDist, float maxDist, float tess, float4x4 o2w, float3 cameraPos )
		{
			float3 wpos = mul(o2w,vertex).xyz;
			float dist = distance (wpos, cameraPos);
			float f = clamp(1.0 - (dist - minDist) / (maxDist - minDist), 0.01, 1.0) * tess;
			return f;
		}

		float4 CalcTriEdgeTessFactors (float3 triVertexFactors)
		{
			float4 tess;
			tess.x = 0.5 * (triVertexFactors.y + triVertexFactors.z);
			tess.y = 0.5 * (triVertexFactors.x + triVertexFactors.z);
			tess.z = 0.5 * (triVertexFactors.x + triVertexFactors.y);
			tess.w = (triVertexFactors.x + triVertexFactors.y + triVertexFactors.z) / 3.0f;
			return tess;
		}

		float CalcEdgeTessFactor (float3 wpos0, float3 wpos1, float edgeLen, float3 cameraPos, float4 scParams )
		{
			float dist = distance (0.5 * (wpos0+wpos1), cameraPos);
			float len = distance(wpos0, wpos1);
			float f = max(len * scParams.y / (edgeLen * dist), 1.0);
			return f;
		}

		float DistanceFromPlane (float3 pos, float4 plane)
		{
			float d = dot (float4(pos,1.0f), plane);
			return d;
		}

		bool WorldViewFrustumCull (float3 wpos0, float3 wpos1, float3 wpos2, float cullEps, float4 planes[6] )
		{
			float4 planeTest;
			planeTest.x = (( DistanceFromPlane(wpos0, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[0]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.y = (( DistanceFromPlane(wpos0, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[1]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.z = (( DistanceFromPlane(wpos0, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[2]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.w = (( DistanceFromPlane(wpos0, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[3]) > -cullEps) ? 1.0f : 0.0f );
			return !all (planeTest);
		}

		float4 DistanceBasedTess( float4 v0, float4 v1, float4 v2, float tess, float minDist, float maxDist, float4x4 o2w, float3 cameraPos )
		{
			float3 f;
			f.x = CalcDistanceTessFactor (v0,minDist,maxDist,tess,o2w,cameraPos);
			f.y = CalcDistanceTessFactor (v1,minDist,maxDist,tess,o2w,cameraPos);
			f.z = CalcDistanceTessFactor (v2,minDist,maxDist,tess,o2w,cameraPos);

			return CalcTriEdgeTessFactors (f);
		}

		float4 EdgeLengthBasedTess( float4 v0, float4 v1, float4 v2, float edgeLength, float4x4 o2w, float3 cameraPos, float4 scParams )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;
			tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
			tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
			tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
			tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			return tess;
		}

		float4 EdgeLengthBasedTessCull( float4 v0, float4 v1, float4 v2, float edgeLength, float maxDisplacement, float4x4 o2w, float3 cameraPos, float4 scParams, float4 planes[6] )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;

			if (WorldViewFrustumCull(pos0, pos1, pos2, maxDisplacement, planes))
			{
				tess = 0.0f;
			}
			else
			{
				tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
				tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
				tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
				tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			}
			return tess;
		}
		#endif //ASE_TESS_FUNCS
		ENDHLSL

		
		Pass
		{
			
			Name "Forward"
			Tags { "LightMode"="UniversalForward" }
			
			Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
			ZWrite Off
			ZTest LEqual
			Offset 0 , 0
			ColorMask RGBA
			

			HLSLPROGRAM
			#define _NORMAL_DROPOFF_TS 1
			#pragma multi_compile_instancing
			#pragma multi_compile _ LOD_FADE_CROSSFADE
			#pragma multi_compile_fog
			#define ASE_FOG 1
			#define _REFRACTION_ASE 1
			#define REQUIRE_OPAQUE_TEXTURE 1
			#define ASE_NEEDS_FRAG_SCREEN_POSITION
			#define ASE_FINAL_COLOR_ALPHA_MULTIPLY 1
			#define _EMISSION
			#define _ALPHATEST_ON 1
			#define _NORMALMAP 1
			#define ASE_SRP_VERSION 70503

			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x

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

			#define SHADERPASS_FORWARD

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			
			#if ASE_SRP_VERSION <= 70108
			#define REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR
			#endif

			#if defined(UNITY_INSTANCING_ENABLED) && defined(_TERRAIN_INSTANCED_PERPIXEL_NORMAL)
			    #define ENABLE_TERRAIN_PERPIXEL_NORMAL
			#endif

			#pragma shader_feature_local _SOLIDEMISION_ON


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
				 float W:TEXCOORD8;
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
 
 
			CBUFFER_START(UnityPerMaterial)
			float4 _MainTex_ST;
			float4 _NorTex_ST;
			float4 _EmisionColor;
			float _BlackWhite;
			float _R;
			float _G;
			float _B;
			float _Contrast;
			float _NormalIntensity;
			float _NormalPower;
			float _Thickness;
			float _Offset;
			float _EmisionPower;
			float _LightAffection;
			#ifdef _TRANSMISSION_ASE
				float _TransmissionShadow;
			#endif
			#ifdef _TRANSLUCENCY_ASE
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END
			float _NoShader;
			sampler2D _MainTex;
			sampler2D _NorTex;
			float4 _MainTex_TexelSize;
			float _BlurBlend;
			float4  _DarkColor;///全局光照

			float4 _Size;
			float4 _Bouns;  
			 float _Sen;
			 float _Step;
				float4 _DepthTex_TexelSize;
		sampler2D  _DepthTex;
		 float	 _DepthBlack;  
		 float _SenS;

				sampler2D _X;
				sampler2D _Y;
				sampler2D _Z;
		float	_NoWayDeb;
			float	_WayDeb;
      float3 _Way;
		   float4	_WayColor;
float _WayInt;
			
		float	_NormalDebug;

		float	_TwoPartDeb;
					///将深度贴图范围缩放
			float _DepthTexIntensity;
			///以多少为远点安排  0还是1	
			float  _DepthTexOri;
			float _DepthTexDeb;
			// float4  _FogColor;
			////Debug开关
			 float _LightDeb;
float _Threshold;
		float _ThresholdNor;	
	float		_UpIntensity; 
			float _DownIntensity;
			int _UpLightPixel;
			int _DownLightPixel;
			
			float _SampleLight;
			float4 _Ocolor;
	float _LerpDarkLight;
 
			
			float4 OC(sampler2D  tex,float2 UV   )
			
{ 
	return tex2D(tex,UV)*_Ocolor; 
}
			     float DirectionalLight(float3 worldNormal, float3 lightDirection)
            {
                // 归一化光源方向
                float3 normalizedLightDir = normalize(-lightDirection);
                
                // 计算法线和光源方向的点积（兰伯特光照模型）
                float NdotL = dot(worldNormal, normalizedLightDir);
                
                // 将点积结果从[-1,1]映射到[0,1]范围
                // 使用半兰伯特光照模型，使背光面也有一定亮度
                float halfLambert = NdotL  ;
                
                // 应用光照强度并确保不小于0
                return max(0, halfLambert  );
            } 
			float LIght (half3 rgb) 
			{
			half3 hsb; 
				 // 找出 RGB 中的最大值和最小值
			  half maxVal = max(rgb.r, max(rgb.g, rgb.b));
			  half minVal = min(rgb.r, min(rgb.g, rgb.b)); 
				 return   maxVal;
			}

			float3 HSLToRGB( float3 c )
			{
    float3 rgb = clamp( abs(fmod(c.x*6.0+float3(0.0,4.0,2.0),6)-3.0)-1.0, 0, 1);
    rgb = rgb*rgb*(3.0-2.0*rgb);
    return c.z * lerp( float3(1,1,1), rgb, c.y);
}
			float3 RGBToHSL(float3 c)
{
    float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
    float4 p = lerp(float4(c.bg, K.wz), float4(c.gb, K.xy), step(c.b, c.g));
    float4 q = lerp(float4(p.xyw, c.r), float4(c.r, p.yzx), step(p.x, c.r));

    float d = q.x - min(q.w, q.y);
    float e = 1.0e-10;
    return float3(
    	abs(q.z + (q.w - q.y) / (6.0 * d + e))
	, d / (q.x + e) 
    , q.x);
}
 
		float Quantize(float v, int i,bool b=true)
		{
			if (i<=0)
			{
				return v;
			}
			     	i=floor(i);
			if (b)
			{
					return round(v * i) / i;
			}
			else
			{
				return ceil(v * i) / i;
			} 
		}
			
	 float  Rangee(float x,float v )
 	{ 
    float y = (x   / (1-  v  ))+v/2 ;  
    return y ; 
	}
 
			
 float4 Blur (float2 uv ,float f=1)//f是采样像素之间的距离
{
	if (_BlurBlend==0)  return  OC(_MainTex,uv  ); 

    half2 texelSize = _MainTex_TexelSize.xy*f;
    float4 Color  =0;
  float  Valid = 0;   
 
  int i=(_DepthTex_TexelSize.z-1)/2;
  i=1;
  half2 Do=(0.5,0.5);
    for (int y = -i; y <= i; y++)
    {
        for (int x = -i; x <=i; x++)
        {  
			 half2 Of=half2(x, y); 
			float L=tex2D( _DepthTex,  Do+Of*_DepthTex_TexelSize) .x  ;//权重
        	// float L=1;
            // 计算采样点的UV偏移
            half2 offset =Of * texelSize; 
            // 采样 
            float4 sampleColor = OC(_MainTex,uv + offset); 
			Color += sampleColor*L; 
            Valid+=L;  
        }
    } 
	     Color /=Valid	   ;   
		     //Color.rgb*=3.913894324-1.956947162*Color.a  ; 
	//Color.rgb/=0.511;  
 //   Color.rgb*=1+(1-Color.a) ;  
	// Color= tex2D(_MainTex,uv  );
			 
	    return  Color ; 
}
			float3 Pn_toN_Open(float3 value)
	 {
return   value *2-1;
	 	
	 }
	float3 N_toPn_Zip(float3 value)
	 {
return  (value+1)/2;
	 	
	 }

			
	 		float get_z(float2 value) 
				{  /// 输入是 三个0 或者三个 1 时
					 float V= 1-  value.x*value.x-value.y*value.y  ;
					 if (V<=0) 
					 {
						 return 1;
					 }
					return sqrt (V) ; 
				}
			float2 PixelPos(float2   pos)
			{
                if (_Size.w==0) return pos;
			half2 Texsize=_Bouns.zw/_Size;  ///世界坐标中  一个像素块的大小             整个精灵图obj  除以   当前像素尺寸数量
			float2 OP=pos-_Bouns.xy; ///模型坐标 
			float2 Int=floor(OP/Texsize);
			 
			return Int*Texsize+_Bouns.xy;
			}

			float4 CalculateContrast( float contrastValue, float4 colorTarget )
			{
				float t = 0.5 * ( 1.0 - contrastValue );
				return mul( float4x4( contrastValue,0,0,t, 0,contrastValue,0,t, 0,0,contrastValue,t, 0,0,0,1 ), colorTarget );
			}

			VertexOutput VertexFunction( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				o.ase_texcoord7.xy = v.texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord7.zw = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif 
				
				o. W=TransformObjectToHClip(v.vertex).z;
				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );
				float3 positionVS = TransformWorldToView( positionWS );
				float4 positionCS = TransformWorldToHClip( positionWS );

				VertexNormalInputs normalInput = GetVertexNormalInputs( v.ase_normal, v.ase_tangent );

				o.tSpace0 = float4( normalInput.normalWS, positionWS.x);
				o.tSpace1 = float4( normalInput.tangentWS, positionWS.y);
				o.tSpace2 = float4( normalInput.bitangentWS, positionWS.z);

				OUTPUT_LIGHTMAP_UV( v.texcoord1, unity_LightmapST, o.lightmapUVOrVertexSH.xy );
				OUTPUT_SH( normalInput.normalWS.xyz, o.lightmapUVOrVertexSH.xyz );

				#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
					o.lightmapUVOrVertexSH.zw = v.texcoord;
					o.lightmapUVOrVertexSH.xy = v.texcoord * unity_LightmapST.xy + unity_LightmapST.zw;
				#endif

				half3 vertexLight = VertexLighting( positionWS, normalInput.normalWS );
				#ifdef ASE_FOG
					half fogFactor = ComputeFogFactor( positionCS.z );
				#else
					half fogFactor = 0;
				#endif
				o.fogFactorAndVertexLight = half4(fogFactor, vertexLight);
				
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
				VertexPositionInputs vertexInput = (VertexPositionInputs)0;
				vertexInput.positionWS = positionWS;
				vertexInput.positionCS = positionCS;
				o.shadowCoord = GetShadowCoord( vertexInput );
				#endif
				
				o.clipPos = positionCS;
				#if defined(ASE_NEEDS_FRAG_SCREEN_POSITION)
				o.screenPos = ComputeScreenPos(positionCS);
				#endif
				return o;
			}
			
			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				float4 ase_tangent : TANGENT;
				float4 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				o.ase_tangent = v.ase_tangent;
				o.texcoord = v.texcoord;
				o.texcoord1 = v.texcoord1;
				
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				o.ase_tangent = patch[0].ase_tangent * bary.x + patch[1].ase_tangent * bary.y + patch[2].ase_tangent * bary.z;
				o.texcoord = patch[0].texcoord * bary.x + patch[1].texcoord * bary.y + patch[2].texcoord * bary.z;
				o.texcoord1 = patch[0].texcoord1 * bary.x + patch[1].texcoord1 * bary.y + patch[2].texcoord1 * bary.z;
				
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			#if defined(ASE_EARLY_Z_DEPTH_OPTIMIZE)
				#define ASE_SV_DEPTH SV_DepthLessEqual  
			#else
				#define ASE_SV_DEPTH SV_Depth
			#endif


			half4 frag ( VertexOutput IN 
						#ifdef ASE_DEPTH_WRITE_ON
						,out float outputDepth :  SV_DEPTH
						#endif
						, half ase_vface : VFACE ) : SV_Target
			{
		
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

				#ifdef LOD_FADE_CROSSFADE
					LODDitheringTransition( IN.clipPos.xyz, unity_LODFade.x );
				#endif

				#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
					float2 sampleCoords = (IN.lightmapUVOrVertexSH.zw / _TerrainHeightmapRecipSize.zw + 0.5f) * _TerrainHeightmapRecipSize.xy;
					float3 WorldNormal = TransformObjectToWorldNormal(normalize(SAMPLE_TEXTURE2D(_TerrainNormalmapTexture, sampler_TerrainNormalmapTexture, sampleCoords).rgb * 2 - 1));
					float3 WorldTangent = -cross(GetObjectToWorldMatrix()._13_23_33, WorldNormal);
					float3 WorldBiTangent = cross(WorldNormal, -WorldTangent);
				#else
					float3 WorldNormal = normalize( IN.tSpace0.xyz );
					float3 WorldTangent = IN.tSpace1.xyz;
					float3 WorldBiTangent = IN.tSpace2.xyz;
				#endif
				float3 WorldPosition = float3(IN.tSpace0.w,IN.tSpace1.w,IN.tSpace2.w);
				float3 WorldViewDirection = _WorldSpaceCameraPos.xyz  - WorldPosition;
				float4 ShadowCoords = float4( 0, 0, 0, 0 );
				#if defined(ASE_NEEDS_FRAG_SCREEN_POSITION)
				float4 ScreenPos = IN.screenPos;
				#endif

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
					ShadowCoords = IN.shadowCoord;
				#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
					ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
				#endif
	
				WorldViewDirection = SafeNormalize( WorldViewDirection );

				float2 uv_MainTex = IN.ase_texcoord7.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 tex2DNode1 = OC( _MainTex, uv_MainTex ); 

				float4 appendResult9 = (float4(( tex2DNode1.r * _R ) , ( tex2DNode1.g * _G ) , ( tex2DNode1.b * _B ) , 0.0));
				float4 appendResult30 = (float4(( ( tex2DNode1.r + tex2DNode1.b + tex2DNode1.b ) * _R ) , ( ( tex2DNode1.r + tex2DNode1.g + tex2DNode1.b ) * _G ) , ( ( tex2DNode1.r + tex2DNode1.g + tex2DNode1.b ) * _B ) , 0.0));
				float4 temp_cast_0 = (_Contrast).xxxx;
				
				float2 uv_NorTex = IN.ase_texcoord7.xy * _NorTex_ST.xy + _NorTex_ST.zw;
				float3 unpack83 = UnpackNormalScale( tex2D( _NorTex, uv_NorTex ), 1 );/// Normal
				unpack83.z = lerp( 1, unpack83.z, saturate(float2( -1,1 ).x) );
				float3 switchResult82 = (((ase_vface>0)?(UnpackNormalScale( tex2D( _NorTex, uv_NorTex ), 1.0f )):(unpack83)));
				float3 temp_cast_5 = (_NormalPower).xxx;
				float3 clampResult57 = clamp( switchResult82 , CalculateContrast(_NormalIntensity,float4( switchResult82 , 0.0 )).rgb , pow( switchResult82 , temp_cast_5 ) );
				
				float smoothstepResult79 = smoothstep( _Thickness , ( _Thickness - 0.1 ) , distance( tex2DNode1.a , _Offset ));
				float grayscale61 = (clampResult57.r + clampResult57.g + clampResult57.b) / 3;
				float4 temp_cast_6 = (grayscale61).xxxx;
				float4 temp_output_62_0 = CalculateContrast(( 1.0 - _LightAffection ),temp_cast_6);
				float4 temp_output_71_0 = ( _EmisionColor * CalculateContrast(_EmisionPower,( ( ( 1.0 - temp_output_62_0 ) - ( temp_output_62_0 * _EmisionColor ) ) * _EmisionPower )) );
				#ifdef _SOLIDEMISION_ON
				float4 staticSwitch81 = temp_output_71_0;
				#else
				float4 staticSwitch81 = ( smoothstepResult79 * temp_output_71_0 );
				#endif
				
				float3 Albedo = pow(
				
				(( 0 )?( appendResult30 ):( appendResult9 )) , 
				
				temp_cast_0
				
				)
				.xyz;
				float3 Normal = clampResult57;
				float3 Emission = staticSwitch81.rgb;
				float3 Specular = 0.5;
				float Metallic = 0;
				float Smoothness = 0.5;
				float Occlusion = 1;
				float Alpha = ( tex2DNode1.a * 1.0 );
				float AlphaClipThreshold = 1.0;
				float AlphaClipThresholdShadow = 0.5;
				float3 BakedGI = 0;
				float3 RefractionColor = 1;
				float RefractionIndex = 1;
				float3 Transmission = 1;
				float3 Translucency = 1;
				#ifdef ASE_DEPTH_WRITE_ON
				float DepthValue = 0;
				#endif

	

				InputData inputData;
				inputData.positionWS = WorldPosition;
				inputData.viewDirectionWS = WorldViewDirection;
				inputData.shadowCoord = ShadowCoords;

				half2 XXYY=IN.clipPos.xy;
				XXYY/=_ScreenParams.xy;
				if (XXYY.x*XXYY.y<0||XXYY.x>1||XXYY.y>1)
				{
					 // discard;
					clip(-1);
				}
				
 				if (_NoShader==1)
				{
					return tex2DNode1 ;
				}


	///////////////////////////////////////////////法线运算///////////////////////////////////////////////////// 

				////当 法线贴图没有 法线贴图颜色就是000
float3  x =tex2D( _X, uv_MainTex );
float3  y =tex2D( _Y, uv_MainTex );
float3  WWz =tex2D( _Z, uv_MainTex );
 // float3  NN =tex2D( _NorTex, uv_MainTex ); 
//
				float4 NNsafasdadso=  tex2D ( _NorTex, uv_MainTex ); 
					// return   half4(  NNsafasdadso .xyz,tex2DNode1.a);  
					float3 NNo= UnpackNormalScale(  NNsafasdadso,1) ;
// return   half4(  N_toPn(NNo .rgb ) ,tex2DNode1.a);
						// return   half4(  NNo .rgb ,tex2DNode1.a);
//原先采样
				
				// float3 Mxy =float3(x.x,y.y,1) ;
				if (x.x*y.y!=1) ///表示有X
				{
					float3 Mxy =float3(x.x,y.y,1) ;
// 
				  Mxy =Pn_toN_Open(Mxy);
					Mxy.z=get_z(Mxy.xy);

						Normal= Mxy;
					
				}
      	if (_NormalDebug)
				{
						return   half4(  N_toPn_Zip(Normal) ,tex2DNode1.a);
				}
					 // Mxy.z=sqrt(1- x* x+ y* y);
	 // Mxy.z= N_toPn(Mxy.zzz); 
				
					// Normal=NNo;
					// return   half4( Mxy,tex2DNode1.a);
						// return   half4( N_toPn(NNo) ,tex2DNode1.a);
			 
 		// return   half4( N_toPn_Zip(Mxy) ,tex2DNode1.a);


			
				// Normal=NNo;
			 // Normal=half3(0,0,1);
			
	 // return half4(N_toPn_Zip()   inputData.normalWS,tex2DNode1.a);
							// return  half4(2,1,2,tex2DNode1.a);
				#ifdef _NORMALMAP
					#if _NORMAL_DROPOFF_TS
					inputData.normalWS = TransformTangentToWorld(Normal, half3x3( WorldTangent, WorldBiTangent, WorldNormal )); 
			 
					#elif _NORMAL_DROPOFF_OS
					inputData.normalWS = TransformObjectToWorldNormal(Normal);
					#elif _NORMAL_DROPOFF_WS
					inputData.normalWS = Normal;
					#endif
					inputData.normalWS = NormalizeNormalPerPixel(inputData.normalWS);
				#else
					inputData.normalWS = WorldNormal;
				#endif 

 float DLInt=DirectionalLight(inputData.normalWS ,_Way);
				// DLInt=(DLInt-0.5)*2;
					// DLInt*=  _WayColor.a ;
				// DLInt*=DLInt ;
				
				// if (step(0.2,DLInt)==1)
				// {
				// 			DLInt*=1+(DLInt*_WayColor.a); 
				// }
				// else
				// {
				// 	// DLInt*=pow(DLInt,1.5); 
				// }
				// DLInt=0.5+0.5*pow(DLInt,9);

		
				if (_WayDeb==1)
				{
						return half4(DLInt.xxx,tex2DNode1.a);
				}
			
				
float3 DirectionColor= DLInt* _WayColor.rgb  ;
		DirectionColor=1-sqrt(1-DirectionColor);		
		if (_NoWayDeb)
				{
					DirectionColor=0;
				} 
	///////////////////////////////////////////////法线运算↑/////////////////////////////////////////////////////
				#ifdef ASE_FOG
					inputData.fogCoord = IN.fogFactorAndVertexLight.x;
				#endif
// return half4(N_toPn_Zip(inputData.normalWS) ,tex2DNode1.a);
				inputData.vertexLighting = IN.fogFactorAndVertexLight.yzw;
				#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
					float3 SH = SampleSH(inputData.normalWS.xyz);
				#else
					float3 SH = IN.lightmapUVOrVertexSH.xyz;
				#endif

				inputData.bakedGI = SAMPLE_GI( IN.lightmapUVOrVertexSH.xy, SH, inputData.normalWS );
				#ifdef _ASE_BAKEDGI
					inputData.bakedGI = BakedGI;///全局光照  全局光设置中亮度为75   时显示的是原光源
				#endif	
				
				// #ifdef _ALPHATEST_ON
				//	clip(Alpha - AlphaClipThreshold);
				//#endif
				///边缘色彩有可能是rgb不为0为0



			    inputData.positionWS.xy=	PixelPos(inputData.positionWS);
	 float4 BlurColor=Blur(uv_MainTex );
				
  // return  BlurColor;
   ////到这边是没有接收光照的模糊像素
 /////////////////////////////////////////////// 模糊到此结束  /////////////////// / //////////////////

  // return  half4(tex2DNode1 );  

   //BlurColor.rgb*=_CCC; 
		// return  half4(IN.W*4,IN.W*4,IN.W*4,tex2DNode1.a) ;
 float W=1-Rangee(IN.W*4,_Sen ); //_Sen 0-1变成类似0.1-0.9  0不做变化
  W=W*step(_Step,W);
   _BlurBlend  *=W;
 // _BlurBlend=  clamp (0,1,  _BlurBlend  *W) ;   ///FFF   1 是全白   0是有些黑

				if (_DepthBlack==1)
				{
						return  half4(W,W,W,tex2DNode1.a) ;
				}
			
		 //return  _BlurBlend;
      
	  /////////////////////////////////////////////// 模糊和深度等等的混合  /////////////////// / //////////////////
 ///色彩输入1        输出后除2    是纯光照数据    +原采样
 half4 Lightcolor = UniversalFragmentPBR( inputData,  half3(1,1,1)  ,  Metallic,  Specular,  0,  Occlusion,  Emission,1 ); 
				  Lightcolor=sqrt(sqrt(Lightcolor));  //扩大光源距离   代价是光源强度没那么强了
				 Lightcolor .rgb  -= 1 ;            //提纯
				Lightcolor.rgb*=step(0, Lightcolor .rgb ) ;//提纯
			 					// return  half4((    Lightcolor .rgb) ,tex2DNode1.a);
 // Lightcolor .rgb  -=0.511;
		 	 // return  half4(Lightcolor .rgb*tex2DNode1.rgb , tex2DNode1.a ) ;
			
   // if (Lightcolor .r <=0 )    Lightcolor=0; 
	///////////////////////////	获取纯粹光照系数↑ ///////////////////////////////////
	 half3 OutHSLcolor=0;
	 half3 H_DC=RGBToHSL(_DarkColor.rgb); 
	 half3 H_LC=RGBToHSL(Lightcolor.rgb ); 
				///应该是    全局光照/阴影 +点光源 +色彩本身
				///该方法之前是色彩本身   色彩*（光照（1+（我们计算的              ）））
				///
				///
			// float3 Zerogood=1; ///对高的和低的进行分别运算
			// float3 ZerpNo=1;
			// 	if (LIght(Lightcolor)>1)
			// 	{
			// 			Zerogood=LIght(Lightcolor);
			// 	}
			// 	else
			// 	{
			// 		ZerpNo=LIght(Lightcolor);
			// 	}

				

	 
///////////////////////////////////////////////深度贴图计算///////////////////////////////////////////////////////
	 // return half4(Lightcolor.rgb,tex2DNode1.a);
// _DarkColor=_DarkColor;
	///////////////////////////////////////////////阴影的计算///////////////////////////////////////////////////////

					 // return half4(Lightcolor.rgb,tex2DNode1.a);

 // OutHSLcolor.y=lerp(0  ,OutHSLcolor.y ,  H_DC.z   );
				
//  OutHSLcolor.y=H_DC.y;
// OutHSLcolor.x=H_DC.x;
				float4 Samplecolor=0;
				half4  Outcolor=0;

	
				int LightPixel=50;
		// if (WWz.x*WWz.y*WWz.z!=1)

		/////////////////////////////////	深度贴图 光照	////////////////////////////////////////////////////
					///排除无深度贴图区域
				///总得有默认
					// if (false)
				{
					///正负1
					float ChangeIntensity =   pow( WWz.x*2,_DepthTexIntensity+1) ; ///1为中心  放大活着缩小  数值为以就不动  零次方返回1   
					
					 Lightcolor*= ChangeIntensity; 
					Lightcolor*=1+_DepthTexOri;

					if (_DepthTexDeb==1)
					{
								 return half4(ChangeIntensity.xxx,tex2DNode1.a);
					}
			
// 	/////////////////////////////////	光照二分	////////////////////////////////////////////////////////////
					// if (false)
					if ( length( Lightcolor.rgb)>=0.0001  )
					{ 
					float zero=step(_Threshold, Lightcolor );
					//二分光照-
						///排除 无光照区域
							 // if (false)
					  LightPixel= 0; 
					 if (zero==1) 
					 { 
					 	
					 	 Lightcolor  *= _UpIntensity ;
					 LightPixel= _UpLightPixel;
					 	
					 half3	Houtc=RGBToHSL( Lightcolor) ;
					 		///归一化后的颜色 饱和度很低
					 		// half3 Oric= lerp( 	Lightcolor ,  _Threshold* _UpIntensity,_ThresholdNor);
					 	 	//饱和度
					 		// Houtc.y=lerp(Houtc.y 	 ,Oric, _UpsaturabilityLrep );
							//亮度
					 		Houtc.z=lerp( 	Houtc.z ,  (1+_Threshold)* _UpIntensity,_ThresholdNor);
						
 				 
					 	 Lightcolor.rgb =HSLToRGB(Houtc);
					 	
					 	// =lerp( Lightcolor ,_Threshold* _UpIntensity,_ThresholdNor);
					 	 	 // Lightcolor =lerp( Lightcolor ,_Threshold* _UpIntensity,_ThresholdNor);
					     // if (true)
					     // {
						    //  half3 hc= Lightcolor 
					     // }
					 }
					 else
					 {
					 		 Lightcolor*= _DownIntensity  ;
					 	 LightPixel= _DownLightPixel; 
					 }
						}

						
	 
		 
					if (_TwoPartDeb==1)
					{
						if (H_LC.z   < -0.0001)   Lightcolor =half4(0,0.1,0,1); 
						else if (H_LC.z ==0) 	 Lightcolor =half4(0,0,1,1); 
						else if (H_LC.z >1)   Lightcolor =Lightcolor*half4(4,1,1,1);
						
						return  half4( Lightcolor.xyz,tex2DNode1.a);
					} 
					else if (H_LC.z  < -0.0001)  H_LC.z  =0; 
					 
			 			// return tex2DNode1*_DarkColor;
		 
				
				} 
		
							if (_SampleLight==1 )
				{
					// Outcolor=lerp( OutHSLcolor.z)  
					///应该简写的很原先URP 效果一致的那种
					// 可能就是阴影颜色之后加上光照颜色
								H_DC.z=1-H_LC;
								half3 HH_LC=RGBToHSL(_DarkColor) ;
								HH_LC.z=1-HH_LC;
								
								Outcolor.rgb=HSLToRGB(HH_LC);
								Samplecolor+=Lightcolor;
				}
				
							else if (_SampleLight==-1 )
							{
								half3  wos=0;
								H_DC.z=(1-H_DC.z) ;
										 
								 //反转后 
						 ///step 阀门越高范围越是大，越是低  边界越是不平滑
						 ///_LerpDarkLight  越高越越亮
						 float zzz =step(0,H_LC.z);
						
								if (zzz==1)
								{
									
									///光照范围内
									///光照和阴影混合？？？？
								// zzz =step(1-_LerpDarkLight,H_LC.z);	
								wos	=H_LC.z*Lightcolor.rgb;
								} 
								// *(1-DirectionColor)
								half3 DC=HSLToRGB(H_DC)  ;
								//  DC 越大画面越亮
								// 问题  为0时 阴影颜色受什么有关？
								  DC.rgb=lerp (
								  	DC.rgb,
								  	1-   ( 1-DC.rgb)*(1- wos)  
								  	,  _LerpDarkLight); 

 							half3 HDirC=RGBToHSL(DirectionColor.rgb); 
								  DC.rgb=	1-   ( 1-DC.rgb)*(1-HDirC.z)  ;
	

		
									 Outcolor.rgb= (0.00000001+DC.rgb)*(  1+Lightcolor .rgb)*pow(  1+DirectionColor.rgb,1+ _WayInt) ;
				 
			 // return half4(DC.xxx,tex2DNode1.a);
								if (LightPixel!=0)
								{
								 half3 HslOut=	RGBToHSL( Outcolor); 
								// HslOut.y=Quantize( HslOut.y,LightPixel );
								HslOut.z=Quantize( HslOut.z,LightPixel,false );
								
								 Outcolor.rgb=  HSLToRGB(HslOut);
						
							}
								}
					
							else if (_SampleLight== 0)
							{
 half LILI= H_LC.z /*光照的亮度  应该是 混合的比例？ */ ;
 OutHSLcolor.z= 1- (H_DC.z*(1-LILI /*光照的阴影值*/)); 
 OutHSLcolor.x= RGBToHSL(lerp( _DarkColor,Lightcolor, LILI)) .x ; 
 OutHSLcolor.y=lerp( H_DC.y,H_LC.y , LILI);  ///饱和度？？？？    当为对比色时   边界部分饱和度会变成灰色(色彩理论)
 if (1-LILI<0.1)  OutHSLcolor.y=lerp(H_LC.y,OutHSLcolor.y,H_LC.y); 
								
 Outcolor=half4( HSLToRGB(OutHSLcolor) ,1);
							}
				
  BlurColor.rgb=lerp(tex2DNode1*Outcolor +Samplecolor,BlurColor*Outcolor+Samplecolor,_BlurBlend);  
	 return half4(BlurColor.rgb,lerp(tex2DNode1.a,BlurColor.a,_BlurBlend)); 
 //////////// ///////// ///////// ///////// ///////// /// ///模糊和清晰的混合////// ///////// ///////// /////////   
					Lightcolor .rgb+=BlurColor.rgb;
					Lightcolor.a=BlurColor.a;
					///原shader 代码后面会对rgb 运算  结果 半透明边缘有奇怪颜色
						half4 color=Lightcolor ;  
 
	 return  color ;
			}

			ENDHLSL
		}
	} 
	CustomEditor "UnityEditor.ShaderGraph.PBRMasterGUI"
	Fallback "Hidden/InternalErrorShader" 
} 