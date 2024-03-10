Shader "Custom/Ocean2" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _DepthGradientShallow("Depth Gradient Shallow", Color) = (0.325, 0.807, 0.971, 0.725)
        _DepthGradientDeep("Depth Gradient Deep", Color) = (0.086, 0.407, 1, 0.749)
        _DepthMaxDistance("Depth Maximum Distance", Float) = 1
    }
    SubShader {
        Tags { 
            "LightMode" = "ForwardBase"
            "PassFlags" = "OnlyDirectional"
        }
        LOD 100

        Pass {
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
                float4 screenPosition : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            uniform float _Seed;
            uniform int _Amount;
            uniform float _Speed;
            uniform float _MaxHeight;
            uniform float _Frequency;

            v2f vert (appdata v) {
                float height = 0;

                float amplitude = 1;
                float frequency = _Frequency;
                
                float x = v.vertex.x;
                float z = v.vertex.z;
                
                float totalHeight;
                
                for (int i = 0; i < _Amount; i++) {
                    float angle = _Seed * i;
                    float2 direction = float2(cos(angle), sin(angle));
    
                    height += amplitude * sin((x * direction.x + z * direction.y) * _Frequency + _Time * _Speed * _Frequency);
    
                    totalHeight += amplitude;
    
                    amplitude *= 0.82;
                    frequency *= 1.18;
                }
    
                height /= totalHeight;
                height *= _MaxHeight;
    
                v.vertex.y += height;

                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.screenPosition = ComputeScreenPos(o.vertex);
                return o;
            }

            float4 _DepthGradientShallow;
            float4 _DepthGradientDeep;

            float _DepthMaxDistance;

            sampler2D _CameraDepthTexture;

            fixed4 frag (v2f i) : SV_Target {
                float existingDepth01 = tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPosition)).r;
                float existingDepthLinear = LinearEyeDepth(existingDepth01);

                float depthDifference = existingDepthLinear - i.screenPosition.w;

                float waterDepthDifference01 = saturate(depthDifference / _DepthMaxDistance);
                float4 waterColor = lerp(_DepthGradientShallow, _DepthGradientDeep, waterDepthDifference01);

                return waterColor;

                // fixed4 col = tex2D(_MainTex, i.uv);
                // return col;
            }
            ENDCG
        }
    }
}
