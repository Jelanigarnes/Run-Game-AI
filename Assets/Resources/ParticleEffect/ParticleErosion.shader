Shader "Custom/ParticleErosion"
{
    Properties
    {
       _MainTex ("Texture", 2D) = "white" {}
       _ErosionTex ("Erosion Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
        
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
 
            sampler2D _MainTex;
            sampler2D _ErosionTex;
            float4 _MainTex_ST;

            struct appdata
            {
                float4 vertex : POSITION;
                float4 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 uv : TEXCOORD0;   
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv.xy = TRANSFORM_TEX(v.uv.xy, _MainTex);
                o.uv.z = v.uv.z;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv.xy);
                float cutval = i.uv.z;
                float erode = tex2D(_ErosionTex, i.uv.xy).r;
                erode = step(erode,cutval);
                return fixed4(col.rgb * 1 - erode, 1 - erode);
            }

            ENDCG
        }
    }
    FallBack "Diffuse"
}
