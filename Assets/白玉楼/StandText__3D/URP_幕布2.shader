Shader "Custom/URP_Unlit_Transparent"
{
    Properties
    {
    	
        _Color	  ("Color", Color) = (1,1,1,1)
        		_Norsc ("NormalScale", Range( 0 , 5)) = 1
        _MainTex ("Base Texture", 2D) = "white" {}
        		_NormalTexture("Normal Texture", 2D) = "bump" {}
    	 _Light ("Light", Range( 0, 1)) = 1
    	_PixeleLight("Light", Float ) = 0
        // ������֧��͸��ͨ�������� PNG ��͸����
    }

    SubShader
    {
        // ��Ⱦ����Ϊ͸������֤��͸��������Ⱦ�׶α���Ⱦ
        Tags 
        { 
            "RenderType" = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
            "Queue" = "Transparent"
        }
 
        // ����͸����ϣ�SrcAlpha��Դ��ɫ͸���ȣ�OneMinusSrcAlpha��1 - ͸���ȣ�
// Blend DstColor Zero,

Blend DstColor Zero
        // ��д����ȣ���ѡ���������󣬰�͸������ͨ����д����Ȼ�ѡ����д�룩
        ZWrite Off

        // ����͸���������Ȳ��ԣ�Ĭ���� LEqual��
        ZTest LEqual

   Cull Off
        // Pass������Ⱦͨ��
        Pass
        {	
            Tags { "LightMode"="UniversalForward" }		
            HLSLPROGRAM 
			#pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS 
            // ������ URP Shader ����ָ��
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
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
            // ���� URP ���Ŀ⣬�ṩ�����Ķ������롢��������ȹ���
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            // ================= �ṹ�嶨�� =================
           float   _Norsc;
        	sampler2D _NormalTexture;
			float 	_Light ;
			float _PixeleLight;
            float4 _Color	;
            // ������ɫ�����룺���� Mesh ������
            struct Attributes
            {
                 
                float3  Nor : NORMAL; 
				float4 Tan : TANGENT;

                float4 positionOS : POSITION;  // ģ�ͱ��ؿռ�����
                float2 uv       : TEXCOORD0;   // ��һ�� UV��������ͼ����
            };

            // ������ɫ����� / Ƭ����ɫ������
            struct Varyings
            { 
                float3  Nor  : TEXCOORD2;   
				float3 Tan : TEXCOORD3;   
                	float3 Bitan : TEXCOORD4;  

                float4 positionHCS : SV_POSITION; // �ü��ռ�����
                float2 uv          : TEXCOORD0;   // ���� UV ��Ƭ����ɫ�� 
                float3 PosW          : TEXCOORD1;    
            }; 

            // ����һ�� 2D ������Ӧ Properties �е� _MainTex ��
            TEXTURE2D(_MainTex );
            // ����һ����������URP �Ƽ���������������ֿ�������
            SAMPLER(sampler_MainTex );
             
            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                 
               VertexNormalInputs normalInput= GetVertexNormalInputs(IN .Nor,IN.Tan);

                OUT.Nor=normalInput.normalWS;
                 OUT.Tan=normalInput.tangentWS;
                 OUT.Bitan=normalInput.tangentWS;

                // ����������ת�����ü��ռ�
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT .PosW=TransformObjectToWorld(IN.positionOS.xyz);
                // ֱ�Ӵ��� UV
                OUT.uv = IN.uv;
                return OUT;
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
            half4 frag(Varyings IN) : SV_Target
            {			
               half3  NorColor =UnpackNormalScale(tex2D ( _NormalTexture, IN.uv ),_Norsc) ; 

                half4 texColor = SAMPLE_TEXTURE2D(_MainTex , sampler_MainTex , IN.uv)* _Color; 
                InputData inputData;
				inputData.positionWS =IN.PosW;
				inputData.viewDirectionWS= SafeNormalize( _WorldSpaceCameraPos-inputData.positionWS);
                 
                float3 Nor= normalize(IN.Nor);
                float3 Tan=IN .Tan;
                float3 Bitan=IN .Bitan;

                inputData.normalWS 
                = TransformTangentToWorld(NorColor.rgb, half3x3(Tan,Bitan,Nor )); 

				inputData.shadowCoord =0;

				 inputData.fogCoord =0; 
				inputData.vertexLighting = 0; 
				inputData.bakedGI= 1 ; 

		  
                    half Metallic= 0;   ///����
                 half3  Specular= 0;    ///�ǽ����ĸ߹���ɫ
                  half  Smoothness= 0;  ///�⻬��
                   half   Occlusion= 1; ///���������  ����
                half3  Emission= 0; ///�Է��� 
                half4 color = UniversalFragmentPBR(
					inputData, 
					1, 
					Metallic, 
					Specular, 
					Smoothness,  
 
					Occlusion, 
					Emission, 
			    	texColor.a);
            	
 
	 color.rgb = sqrt(sqrt(color.rgb)) -1; //提取亮度信息
 
                color.a*=color.rgb;   //剔除  （让黑色变成透明
 
                return  (color *5)+1 ;  //*5为了光照效果更强    +1为了 0的地方  缓冲颜色不会×0变成透明
            }
            ENDHLSL
        }
    } 
    FallBack "Sprites/Default"
}