Shader "Custom/SimpleSpecular"
{
    Properties
    {
       
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap ("Bumpmap", 2D) = "bump" {}
       
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
      

        CGPROGRAM
      
        #pragma surface surf SimpleSpecular

        sampler2D _MainTex;
        sampler2D _BumpMap;

        

        half4 LightingSimpleSpecular (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {
        half3 h = normalize (lightDir + viewDir);

        half diff = max (0, dot (s.Normal, lightDir));

        float nh = max (0, dot (s.Normal, h));
        float spec = pow (nh, 48.0);

        half4 c;
        c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec) * atten;
        c.a = s.Alpha;
        return c;
        }

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
        };
       

        void surf (Input IN, inout SurfaceOutput o)
        {
            o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
             o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
        }
        ENDCG
    }
    FallBack "Diffuse"
}
