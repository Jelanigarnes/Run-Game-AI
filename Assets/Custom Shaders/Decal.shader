Shader "Custom/Decal"
{
    Properties
    {        
        _MainTex ("Texture", 2D) = "black" {}      
    }
    SubShader
    {
        Tags { "Queue"="Transparent" }

        //Remove all Black
        Blend One One
        //Remove Black on outside
        //Blend SrcAlpha OneMinusSrcAlpha

        //Black Only removes white i think?
        //Blend DstColor Zero

        Pass{
        SetTexture[_MainTex] {combine texture}
        }
    }
    FallBack "Diffuse"
}
