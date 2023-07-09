Shader "Unlit/CharacterShader"
{
    Properties
    {
        _MainTex2("Texture", 2D) = "white" {}
        _Color("color", Color) = (.25, .5, .5, 1)
        _ShadowColor("Shadow Color", Color) = (.25, .5, .5, 1)

    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass
            {



                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"
                #include "Lighting.cginc"
                #include "AutoLight.cginc"

                sampler2D _MainTex2;
                float4 _MainTex_ST;
                float4 _Color;
                float4 _ShadowColor;

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                    float3 normal : NORMAL;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                    float3 normal : TEXCOORD1;
                    //float4 cameraLocalPos : TEXCOORD2;
                };



                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    o.normal = UnityObjectToWorldNormal(v.normal);
                    //o.cameraLocalPos = mul(unity_WorldToObject, float4(_WorldSpaceCameraPos, 1.0));

                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // sample the texture
                    float3 N = i.normal;
                    float3 L = _WorldSpaceLightPos0.xyz;
                    float diffuseLight = 0.5 < dot(N,L);
                    float finalLight = diffuseLight * 0.7 + (.8 < dot(N, L));
                    float4 outputColor = lerp(_ShadowColor,_Color, diffuseLight);



                    return outputColor * tex2D(_MainTex2, i.uv);
                }
                ENDCG
            }
        }
}