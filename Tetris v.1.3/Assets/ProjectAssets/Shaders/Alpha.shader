Shader "Custom/Alpha" {
	Properties {
		_Color ("Color", Color) = (1, 1, 1, 1)
		_MainTex ("Texture (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Transparent" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert finalcolor:mycolor

		sampler2D _MainTex;
		fixed4 _Color;
		
		struct Input {
			float2 uv_MainTex;
			//float2 uv_BumpMap;
		};

      void mycolor (Input IN, SurfaceOutput o, inout fixed4 color)
      {
          color *= _Color;
      }

		void surf (Input IN, inout SurfaceOutput o) {

		fixed4 tex = tex2D(_MainTex, IN.uv_MainTex) * _Color.a;
			o.Albedo = tex;
			o.Alpha = o.Albedo;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
