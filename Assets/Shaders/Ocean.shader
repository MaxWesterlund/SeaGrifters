Shader "Custom/Ocean" {
    Properties {
        _DepthGradientShallow("Depth Gradient Shallow", Color) = (1,1,1,1)
        _DepthGradientDeep("Depth Gradient Deep", Color) = (1,1,1,1)
        _DepthMaxDistance("Depth Maximum Distance", Float) = 1
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
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float4 screenPosition : TEXCOORD2;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.screenPosition = ComputeScreenPos(o.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 _DepthGradientShallow;
            float4 _DepthGradientDeep;

            float _DepthMaxDistance;

            sampler2D _CameraDepthTexture;

            fixed4 frag(v2f i) : SV_Target {
                int numWaves = 20;
                float seed = 0.98912;
                float freq = 1;
                float speed = 1;

                float2 pos = i.uv.xy;
                
                float amp = 1;
                float totAmp = 0;

                float height = 0;

                for (int j = 0; j < numWaves; j++) {
                    float angle = seed * j;
                    float2 direction = float2(cos(angle), sin(angle));
                    
                    float x = dot(direction, pos) * freq + _Time.y * speed;
                    float wave = amp * sin(x);

                    height += wave;
                    totAmp += amp;

                    freq *= 1.18;
                    amp *= 0.82;
                }

                float waveHeight = height / totAmp;

                float existingDepth01 = tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPosition)).r;
                float existingDepthLinear = LinearEyeDepth(existingDepth01) + waveHeight;

                float depthDifference = existingDepthLinear - i.screenPosition.w;

                float waterDepthDifference01 = saturate(depthDifference / _DepthMaxDistance);
                float4 waterColor = lerp(_DepthGradientShallow, _DepthGradientDeep, waterDepthDifference01);
                
                return waterColor;
            }
            ENDCG
        }
    }
}
