Shader "Custom/ToonRamp"
{
    Properties
    {
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Ramp("Toon Ramp", 2D) = "white" {}
        _Threshold("Threshold", Range(0,1)) = 1
    }

    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _Ramp;
            float _Threshold;

            float4 frag(v2f i) : SV_Target {
                float4 col = tex2D(_MainTex, i.uv);
                float ramp = tex2D(_Ramp, float2(col.r, 0.5)).r;
                col.rgb = step(ramp, _Threshold) * col.rgb;
                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}