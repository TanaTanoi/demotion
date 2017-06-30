// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "PxlShader"
{
    Properties
    {
        _PxlSize("Pixelation Amount", Float) = 10

    }
 
    SubShader
    {
        Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" }
        Fog{ Mode Off } 
        ZWrite Off
        LOD 200
        Cull Off  //turn off cull so we can see what is behind
        GrabPass{ "_GrabTexture" } 
        Pass
        {
            CGPROGRAM 
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc" 
            struct v2f
            {
                float4 pos : SV_POSITION; //get position and uv mapping
                float4 uv : TEXCOORD0;
            }; 
            float _PxlSize=20; 
            v2f vert(appdata_base v) //vertext shader to grab the data
            {
                v2f bg; 
                bg.pos = UnityObjectToClipPos(v.vertex);
                bg.uv = ComputeGrabScreenPos(bg.pos);
                return bg;
            } 
            sampler2D _GrabTexture; 
            float4 frag(v2f Behind) : COLOR //fragment shader to deal with the pixelation of what is behind
            {
                float2 steppedUV = Behind.uv.xy / Behind.uv.w;
                steppedUV /= _PxlSize / (_ScreenParams.xy*4);
                steppedUV = round(steppedUV);
                steppedUV *= _PxlSize / (_ScreenParams.xy*4); 
                return tex2D(_GrabTexture, steppedUV);
            }
         
            ENDCG
        }
    }
}