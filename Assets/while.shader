Shader "Unlit/while"
{
    Properties
    {
  _Alpha("Alpha value",Range(0,1))=0
  _While("color",Color)=(1,1,1,1)
         _MainTex("Texture",2D) = "while"{}
    }
    SubShader
    {
 Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
         float _Alpha;
    float4 _While;
    sampler2D  _MainTex;

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
             

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
            float final_alpha = col.a + _Alpha - col.a * _Alpha;
            final_alpha *= 1 - step(col.a, 0);

            fixed4 res = fixed4(col.rgb * col.a * (1 - _Alpha) + _While.rgb * _Alpha, final_alpha);
              
            return res;
            }
            ENDCG
        }
    }
}
