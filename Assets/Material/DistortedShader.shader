// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/DistortedShader"
{
	Properties
	{
		_MainTex("Sprite Texture", 2D) = "white" {}
	_Distortion("DistortionValue", Float) = 1
	}

		SubShader
	{
		Tags{
		"Queue" = "Transparent"
		"IgnoreProjector" = "True"
		"RenderType" = "Transparent"
		"PreviewType" = "Plane"
		"CanUseSpriteAtlas" = "True"

	}

		Pass
	{
		Cull Off
		Lighting Off
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha


		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"



		struct appdata
	{
		float4 vertex : POSITION;
		float4 uv : TEXCOORD0;
	};

	struct v2f
	{
		float4 vertex : SV_POSITION;
		float4 uv : TEXCOORD0;
	};
	float _Distortion;
	
	v2f vert(appdata v)
	{
	v2f o;
	v.vertex += _Distortion;
	o.vertex = UnityObjectToClipPos(v.vertex);
	o.uv = v.uv;
	return o;
	}


	sampler2D _MainTex;



		float4 frag(v2f i) : SV_Target
		{
		float4 color = tex2D(_MainTex, i.uv- _Distortion);
		return color;
		}
		ENDCG
	}
	}
}
