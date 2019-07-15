Shader "Unlit/GlassShader"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
		_Distortion ("DistortionValue", Float) = 1
			_Transparency("TransparencyValue", Float) = 1
	}

		SubShader
	{
		Tags {
		"Queue" = "Transparent"
		"IgnoreProjector" = "True"
		"RenderType" = "Transparent"
		"PreviewType" = "Plane"
		"CanUseSpriteAtlas" = "True"

	}

		GrabPass
	{
		"_BackgroundTexture"
	}

		// Render the object with the texture generated above, and invert the colors
		Pass
	{
		Cull Off
		Lighting Off
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma target 2.0
#pragma multi_compile_instancing
#pragma multi_compile _ PIXELSNAP_ON
#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
//#include "UnitySprites.cginc"
#include "UnityCG.cginc"




		struct v2f
	{
		float4 grabPos : TEXCOORD0;
		float4 pos : SV_POSITION;
	};



	v2f vert(appdata_base v) {
		v2f o;
		// use UnityObjectToClipPos from UnityCG.cginc to calculate 
		// the clip-space of the vertex
		o.pos = UnityObjectToClipPos(v.vertex);
		// use ComputeGrabScreenPos function from UnityCG.cginc
		// to get the correct texture coordinate
		o.grabPos = ComputeGrabScreenPos(o.pos);


		return o;
	}

	sampler2D _MainTex;

	sampler2D _BackgroundTexture;
	float _Distortion;
	float _Transparency;

	half4 frag(v2f i) : SV_Target
	{
		half4 bgcolor = tex2D(_BackgroundTexture, i.grabPos-_Distortion);
		bgcolor.a = _Transparency;
		return bgcolor;
	//	float4 color = tex2D(_MainTex, i.grabPos);
	//	return color;

	}
		ENDCG
	}

	}
}