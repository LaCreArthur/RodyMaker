///<summary>
/// Transparent clone of the bending shader, needed to preserve the bending aspect of transparent materials
///</summary>

Shader "Custom/Bendy diffuse - Transparent" 
{
	Properties 
	{
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}

	SubShader 
	{
		Tags {"Queue"="Geometry" "RenderType"="Transparent" }
		LOD 200

		ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

		CGPROGRAM
		#pragma surface surf Lambert vertex:vert addshadow
		
		#include "UnityCG.cginc"
		#pragma multi_compile __ HORIZON_WAVES 
		#pragma multi_compile __ BEND_ON
		#pragma multi_compile __ CURVES

		// Global properties to be set by BendControllerRadial script
		uniform half3 _CurveOrigin;
		uniform fixed3 _ReferenceDirection;
		uniform half _Curvature;
		uniform fixed3 _Scale;
		uniform half _FlatMargin;
		uniform half _HorizonWaveFrequency;
		uniform half _curveMultiplier;

		// Per material properties
		sampler2D _MainTex;
		fixed4 _Color;

		struct Input 
		{
			float2 uv_MainTex;
		};

		half4 Bend(half4 v)
		{
			half4 wpos = mul (unity_ObjectToWorld, v);
			
			half2 xzDist = (wpos.xz - _CurveOrigin.xz) / _Scale.xz;
			half dist = length(xzDist);
			fixed waveMultiplier = 1;
			fixed curveMultiplier = 0;
			
			#if defined(HORIZON_WAVES)
			half2 direction = lerp(_ReferenceDirection.xz, xzDist, min(dist, 1));
			
			half theta = acos(clamp(dot(normalize(direction), _ReferenceDirection.xz), -1, 1));
			
			waveMultiplier = cos(theta * _HorizonWaveFrequency);
			#endif
			
			dist = max(0, dist - _FlatMargin);
			
			wpos.y -= dist * dist * _Curvature * waveMultiplier;

			#if defined(CURVES)
			curveMultiplier = _curveMultiplier;
			wpos.x -= dist * dist * (_Curvature / 2) * curveMultiplier;
			#endif

			wpos = mul (unity_WorldToObject, wpos);
			
			return wpos;
		}

		void vert (inout appdata_full v) 
		{
			#if defined(BEND_ON)
			v.vertex = Bend(v.vertex);	
			#endif
		}

		void surf (Input IN, inout SurfaceOutput o) 
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}

	Fallback "Legacy Shaders/Diffuse"
}
