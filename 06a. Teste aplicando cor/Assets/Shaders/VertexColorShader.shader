Shader "Custom/VertexColorShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
    }
 
    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Opaque"}
        
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
 
            struct appdata {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };
 
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };
 
            sampler2D _MainTex;
            float4 _Color;
 
            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color;
                o.uv = v.uv;
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target {
                fixed4 tex = tex2D(_MainTex, i.uv) * _Color;
                return tex * i.color;
            }

            ENDCG
        }
    }

    FallBack "Diffuse"
}
