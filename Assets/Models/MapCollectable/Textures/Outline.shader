Shader "Custom/Outline"
{
   Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineWidth ("Outline Width", Range(0.0, 0.1)) = 0.01
        _BumpMap ("Bumpmap", 2D) = "bump" {}
    }

    SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Pass {
            ZWrite Off
            ColorMask RGB
            Cull Front
            Offset -1,-1

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

            float _OutlineWidth;
            float4 _OutlineColor;
            sampler2D _MainTex;
            sampler2D _BumpMap;

            struct Input
        {
           
            float2 uv_BumpMap;
           
        };


            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
              
                return o;
            }

         

            fixed4 frag (v2f i) : SV_Target {
                float4 col = tex2D(_MainTex, i.uv);
                float4 outline = tex2D(_MainTex, i.uv);
                outline.rgb = _OutlineColor.rgb;
                outline.a = col.a;
                float4 outlineAlpha = tex2D(_MainTex, i.uv + float2(-_OutlineWidth, 0)) +
                    tex2D(_MainTex, i.uv + float2(_OutlineWidth, 0)) +
                    tex2D(_MainTex, i.uv + float2(0, -_OutlineWidth)) +
                    tex2D(_MainTex, i.uv + float2(0, _OutlineWidth));
                outlineAlpha.rgb /= 6;
                col = lerp(outline, col, outlineAlpha.a);
                return col;
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}
