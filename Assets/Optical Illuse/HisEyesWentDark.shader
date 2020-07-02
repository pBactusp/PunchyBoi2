Shader "Hidden/HisEyesWentDark"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _VignetteMask("Texture", 2D) = "white" {}
        _TintFactor("Float", float) = 0
        _Color ("Vector4", Color) = (1, 1, 1, 1)
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

            #include "UnityCG.cginc"

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

            sampler2D _MainTex;
            sampler2D _VignetteMask;
            float _TintFactor;
            fixed4 _Color;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float vignetteFactor = tex2D(_VignetteMask, i.uv).x;

                float realTintFactor = _TintFactor * lerp(1, 0.3, vignetteFactor);

                return lerp(col, _Color, realTintFactor);
            }
            ENDCG
        }
    }
}
