Shader "EasyShader/Self Illumin Diffuse" 
{
	Properties 
	{
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}

	}
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200

		Pass
		{
			ZWrite On
			ColorMask 0
		}

		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		fixed4 _Color;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
			fixed4 c = tex * _Color;
			o.Albedo = c.rgb;
			o.Emission = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Mobile/Diffuse"
}