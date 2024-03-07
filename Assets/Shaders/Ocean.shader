Shader "Custom/Ocean" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
	}
 
	SubShader{
		Tags { "RenderType" = "Opaque" }
 
		CGPROGRAM
		//Notice the "vertex:vert" at the end of the next line
		#pragma surface surf Standard fullforwardshadows vertex:vert
 
		sampler2D _MainTex;
 
		struct Input {
			float2 uv_MainTex;
		};
 
		fixed4 _Color;
 
		void vert(inout appdata_full v, out Input o) {
            float seed = 1;
            int amount = 30;
            float speed = 1;
    
            float amplitude = 1;
            float frequency = 10;

            float height = 0;

            float x = v.vertex.x;
            float z = v.vertex.z;

            float maxHeight;

            for (int i = 0; i < amount; i++) {
                float angle = seed * i;
                float2 direction = float2(cos(angle), sin(angle));

                height += amplitude * sin((x * direction.x + z * direction.y) * frequency + _Time * speed * frequency);

                maxHeight += amplitude;

                amplitude *= 0.82;
                frequency *= 1.18;
            }

            height /= maxHeight;

            v.vertex.y += height;
        
			UNITY_INITIALIZE_OUTPUT(Input, o);
		}
 
		void surf(Input IN, inout SurfaceOutputStandard o) {
 
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
 
			o.Alpha = c.a;
		}
		ENDCG
	}
	
FallBack "Diffuse"
}