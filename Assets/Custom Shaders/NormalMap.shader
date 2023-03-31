Shader "Custom/NormalMap"
{
    Properties
    {
       _FloorTex ("Floor Texture", 2D) = "white" {}
      _BumpMap ("Bumpmap", 2D) = "bump" {}
      _Detail ("Detail", 2D) = "gray" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
       

        CGPROGRAM
       
        #pragma surface surf Lambert

       

        sampler2D _FloorTex;
        sampler2D _BumpMap;
        sampler2D _Detail;

        struct Input
        {
            float2 uv_FloorTex;
            float2 uv_BumpMap;
            float2 uv_Detail;
        };

        

     

        void surf (Input IN, inout SurfaceOutput o)
        {
            o.Albedo = tex2D (_FloorTex, IN.uv_FloorTex).rgb;
          o.Albedo *= tex2D (_Detail, IN.uv_Detail).rgb * 2;
          o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
        }
        ENDCG
    }
    FallBack "Diffuse"
}
