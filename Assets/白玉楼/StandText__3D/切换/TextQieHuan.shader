Shader "Hidden/TextQieHuan"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ SHOW_RED
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR; // 顶点颜色
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR; // 传递顶点颜色到片元着色器
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color;
                return o;
            }

            sampler2D _MainTex;

            float4 frag (v2f i) : SV_Target
            {
                float4 texCol = tex2D(_MainTex, i.uv);

                // 修正 alpha 计算：以颜色最大分量调节 alpha，避免类型错误
                float luminance = max(max(texCol.r, texCol.g), texCol.b);
                float alpha = texCol.a * luminance;
                clip(alpha - 0.001); // 若 alpha 很小则裁剪

                // SHOW_RED 开时输出红色；否则使用顶点颜色（保持原 alpha）
    #ifdef SHOW_RED
                float3 outRgb = float3(1.0, 0.0, 0.0);
    #else
                float3 outRgb = i.color.rgb; // 使用顶点颜色
    #endif

                return float4(outRgb, alpha);
            }
            ENDCG
        }
    }
}