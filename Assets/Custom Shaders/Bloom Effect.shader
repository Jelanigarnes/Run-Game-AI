Shader "Custom/Bloom Effect"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" {}
        _BloomThreshold ("Bloom Threshold", Range(0.0, 1.0)) = 0.8
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard
        #include "UnityCG.cginc"

        sampler2D _MainTex;
        float _BloomThreshold;

        struct Input {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o) {
            // Sample the base texture
            float3 baseColor = tex2D(_MainTex, IN.uv_MainTex).rgb;

            // Calculate the brightness of the pixel
            float brightness = dot(baseColor, float3(0.299, 0.587, 0.114));

            // Calculate the bloom threshold value
            float bloomThreshold = max(0.0, _BloomThreshold - 0.25);

            // Apply the bloom effect based on the brightness of the pixel
            float bloomIntensity = max(0.0, brightness - bloomThreshold);
            o.Emission = bloomIntensity * 4.0; // adjust the bloom intensity as needed
            o.Albedo = baseColor - bloomIntensity; // subtract the bloom color from the base color
        }
        ENDCG
    }
    FallBack "Diffuse"
}
