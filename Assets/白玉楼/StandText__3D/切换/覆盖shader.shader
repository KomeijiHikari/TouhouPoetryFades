Shader "Hidden/NewImageEffectShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

           _SpriteColor("Oirdin color", Color) = (1,1,1,1)
    }
    SubShader
    {
        // No culling or depth
              Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
            ZWrite Off
            ZTest LEqual
            Offset 0, 0
            ColorMask RGBA

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
                      float4 _SpriteColor;
            struct appdata
            {
              float4 color : COLOR; // 顶点颜色
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
             
            struct v2f
            {
                               float4 color : COLOR; // 传递顶点颜色到片元着色器
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                 o.color = v.color;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
 
                // half L=  _SpriteColor.a;
                // _SpriteColor.a=col.a;
                // half4 cc=lerp(col,_SpriteColor,L);

         
                return half4( 1,1,1 ,1);
            }
            ENDCG
        }
    }
}
