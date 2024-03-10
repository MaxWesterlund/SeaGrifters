Shader "Custom/Ocean3" {
    Properties {
        _DepthGradientShallow("Depth Gradient Shallow", Color) = (1,1,1,1)
        _DepthGradientDeep("Depth Gradient Deep", Color) = (1,1,1,1)
        _DepthMaxDistance("Depth Maximum Distance", Float) = 1
        _SurfaceNoise("Surface Noise", 2D) = "white" {}
        _SurfaceNoiseScroll("Surface Noise Scroll Amount", Vector) = (0,0,0,0)
        _FoamDistance("Foam Distance", Float) = 1
    }
    SubShader {
        Tags { 
            "Queue"="Transparent"
        }
        LOD 100
        
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float4 uv : TEXCOORD0;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float4 screenPosition : TEXCOORD2;
                float2 noiseUV : TEXCOORD0;
            };

            sampler2D _SurfaceNoise;
            float4 _SurfaceNoise_ST;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.screenPosition = ComputeScreenPos(o.vertex);
                o.noiseUV = TRANSFORM_TEX(v.uv, _SurfaceNoise);
                return o;
            }

            float4 _DepthGradientShallow;
            float4 _DepthGradientDeep;

            float _DepthMaxDistance;

            sampler2D _CameraDepthTexture;

            float _SurfaceNoiseCutoff;
            float4 _SurfaceNoiseScroll;

            float _FoamDistance;

            fixed4 frag (v2f i) : SV_Target {
                float existingDepth01 = tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPosition)).r;
                float existingDepthLinear = LinearEyeDepth(existingDepth01);

                float depthDifference = existingDepthLinear - i.screenPosition.w;

                float waterDepthDifference01 = saturate(depthDifference / _DepthMaxDistance);
                float4 waterColor = lerp(_DepthGradientShallow, _DepthGradientDeep, waterDepthDifference01);
                
                float2 noiseUV = float2(i.noiseUV.x + _Time.y * _SurfaceNoiseScroll.x, i.noiseUV.y + _Time.y * _SurfaceNoiseScroll.y);
                
                float surfaceNoiseSample = tex2D(_SurfaceNoise, noiseUV).r;
                
                float foamDepthDifference01 = saturate(depthDifference / _FoamDistance);
                float surfaceNoiseCutoff = foamDepthDifference01;

                float surfaceNoise = surfaceNoiseSample > surfaceNoiseCutoff ? 1 : 0;

                return waterColor + surfaceNoise;
            }
            ENDCG
        }
    }
}
