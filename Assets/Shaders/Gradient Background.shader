Shader "EasyShader/GradientBackground" {
	Properties {
		_TopColor ("Top Color", Color) = (1,1,1,1)
		_BottomColor("Bottom Color", Color) = (1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		float4 _TopColor;
		float4 _BottomColor;

		struct Input {
			float4 screenPos;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) {
			float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
			fixed4 c = lerp(_BottomColor, _TopColor, screenUV.y);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Mobile/Diffuse"
}
