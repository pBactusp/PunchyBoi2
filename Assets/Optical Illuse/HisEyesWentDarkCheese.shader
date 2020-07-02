Shader "Custom/HisEyesWentDarkCheese"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}

        _VignetteMask("VignetteMask", 2D) = "white" {}
        _TintFactor("TintFactor", float) = 0
        _Color("TintColor", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha:fade
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        sampler2D _VignetteMask;
        float _TintFactor;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float vignetteFactor = 1- tex2D(_VignetteMask, IN.uv_MainTex).x;

            //float realTintFactor = _TintFactor * lerp(1, 0.3, vignetteFactor);

            //return lerp(col, _Color, realTintFactor);

            o.Albedo = _Color.rgb;
            // Metallic and smoothness come from slider variables
            //o.Metallic = _Metallic;
            //o.Smoothness = _Glossiness;
            o.Alpha = _TintFactor * vignetteFactor;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
