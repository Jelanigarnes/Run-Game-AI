Shader "Custom/Window for Wall"
{
    Properties
    {
        _MainTex ("Diffuse", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue" = "Geometry" }

        Stencil {
            Ref 1               // use 1 as the value to check against
            Comp notequal// If this stencil Ref 1 is not equal to what's in the stencil buffer, then we will keep this pixel that belongs to the Wall
            Pass keep           // If you do find a 1, don't draw it.
            //Ref 2 // set the stencil reference value to 2 for this quad
            //Comp always // always pass the stencil test for this quad
            //Pass replace // set the stencil buffer value to 2 if the test passes
        }

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;

        struct Input{
            float2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutput o){
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
