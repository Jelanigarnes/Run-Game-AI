Shader "Custom/RimLighting"
{
    Properties
    {
       _RimColor ("Rim Color", Color) = (0,0.5,0.5,0.0)
       _RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
       _BatteryTex ("Texture", 2D) = "white"{}
    }
    SubShader
    {
    //For Hologram effect
    Tags { "RenderType" = "Opaque" }

    //For Hologram
    //Pass{
    //    ZWrite On 
    //    ColorMask 0
    //}

    CGPROGRAM
    //alpha:fade for hologram
        #pragma surface surf Lambert 

        sampler2D _BatteryTex;
        float4 _RimColor;
        float _RimPower;
        

        struct Input {
            float3 viewDir;
            float2 uv_BatteryTex;
        };

        

        void surf (Input IN, inout SurfaceOutput o) {
        //half rim = dot(normalize(IN.viewDir), o.Normal);
         o.Albedo = tex2D (_BatteryTex, IN.uv_BatteryTex).rgb;
        half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
        //o.Emission = _RimColor.rgb * rim;
        
        o.Emission = _RimColor.rgb * pow(rim, _RimPower);
        
        //For Hologram
        //o.Alpha = pow(rim, _RimPower);
        }

        ENDCG
        
    }
    FallBack "Diffuse"
}
