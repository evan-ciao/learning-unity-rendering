// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/First Shader"
{
    // properties are shader values configurable in materials
    Properties
    {
        _Tint ("Tint", Color) = (1, 1, 1, 1)
        _MainTex ("Texture", 2D) = "White" {}
    }
    
    // subshaders can group together shader variants for the same shader.
    // ex : different shaders for multiple platforms
    SubShader
    {
        // passes render the object multiple times (vfxs, ...)
        Pass
        {
            CGPROGRAM

            float4 _Tint;
            sampler2D _MainTex;
            float4 _MainTex_ST; // "Scale/Translation" info. those values configurable foreach texture in the inspector.

            /// semantics specify to the compiler what shall be done with the returned value
            /// SV_POSITION = System Value Position
            // float4 VertexProgram(float4 position : POSITION) : SV_POSITION

            // "Pietro fa il servizio e torna indietro"
            struct Pietro
            {
                float4 position : SV_POSITION;      // the channel name is important! the shader program knows what to do with the struct because of the semantics!
                float3 localPosition : TEXCOORD0;
                float2 uvs : TEXCOORD1;
            };

            struct VertexData
            {
                float4 position : POSITION;
                float2 uvs : TEXCOORD0;
            };

            // pragma -> act, from greek
            #pragma vertex VertexProgram
            #pragma fragment FragmentProgram

            // shaders dont have classes.
            // be like shaders.
            #include "UnityCG.cginc"

            Pietro VertexProgram(VertexData v)
            {
                Pietro o;
                o.localPosition = v.position.xyz;
                o.position = UnityObjectToClipPos(v.position);
                
                o.uvs = v.uvs;

                return o;
            }

            // the fragment program gets the vertex position in input
            float4 FragmentProgram(Pietro i) : SV_TARGET
            {
                // return float4(i.localPosition + 0.5, 1);
                // return float4(i.uvs, 1, 1);

                // also theres a macro for this : TRANSFORM_TEX
                // macros are functions whose returned values are evaluated at pre-processing time and substituted before compilation takes place.
                return tex2D(_MainTex, i.uvs * _MainTex_ST.xy * _MainTex_ST.zw);
            }

            ENDCG
        }
    }
}