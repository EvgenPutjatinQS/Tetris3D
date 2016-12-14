Shader "Custom/Transparent" {
	Properties {
		_Color ("Color", Color) = (1, 1, 1, 1)
		_MainTex ("Texture (RGB)", 2D) = "white" {}
		_CurrAlpha ("Current Alpha", Range(0,1)) = 1

	}
	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType" = "Transporent"}
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert alpha

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		half _CurrAlpha;
		fixed4 _Color;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a * _CurrAlpha;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
