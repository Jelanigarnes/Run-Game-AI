Shader "Custom/OutlineWithBump"
{
   Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineWidth ("Outline Width", Range(0, 1)) = 0.1
    }
 
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100
 
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };
 
            struct v2f {
                float2 uv : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
            sampler2D _NormalMap;
            float4 _OutlineColor;
            float _OutlineWidth;
 
            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }
 
           float4 frag (v2f i) : SV_Target {
    float3 normal = UnpackNormal(tex2D(_NormalMap, i.uv));
    float3 worldNormal = normalize(mul(i.worldNormal, (float3x3)UNITY_MATRIX_MV));
    float3 outline = fwidth(normal) * _OutlineWidth * _OutlineColor.rgb;
    float3 worldPos = mul(unity_ObjectToWorld, i.vertex).xyz;
    float3 finalColor = tex2D(_MainTex, i.uv).rgb + outline * (1 - saturate(dot(worldNormal, normalize(_WorldSpaceCameraPos - worldPos))));
    return float4(finalColor, 1);
}
            ENDCG
        }
    }
    FallBack "Diffuse"
}
