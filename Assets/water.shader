Shader "Custom/water" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

#include "SimplexNoise2D.hlsl"

		sampler2D _MainTex;

		struct Input {
			float3 worldPos;
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		float3 WaveNormal(float2 plane)
		{
			float2 plane2 = plane;
			float2 plane3 = plane;

			plane.x += 5. * _Time.x;
			plane.y += 6.5 * _Time.x;

			plane2.x += 8.2 * _Time.x;
			plane2.y += 10.3 * _Time.x;

			plane3.x += 3. * _Time.x;
			plane3.y += 2.5 * _Time.x;

			return normalize(lerp(float3(0., 1., 0.), snoise_grad(plane * .75) * .24 + snoise(plane2 * 1.6) * .3 + snoise(plane3 * .25) * .4, .125));
		}

		void surf (Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Normal = WaveNormal(IN.worldPos.xz);
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
