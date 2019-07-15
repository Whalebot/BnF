Shader "Unlit/RefractionShader"
{
	Properties
	{
		_MainTex("Sprite Texture", 2D) = "white" {}
		_Distortion("DistortionValue", Float) = 1
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


			struct appdata
					{
						float4 vertex : POSITION;
						float4 uv : TEXCOORD0;
					};



					struct v2f
				{
					float4 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
				};

					//					v2f p;

									v2f vert(appdata v) {
										v2f o;
										// use UnityObjectToClipPos from UnityCG.cginc to calculate 
										// the clip-space of the vertex
										o.vertex = UnityObjectToClipPos(v.vertex);
										// use ComputeGrabScreenPos function from UnityCG.cginc
						// to get the correct texture coordinate
						//o.uv = ComputeGrabScreenPos(o.vertex);
						o.uv = ComputeGrabScreenPos(o.vertex);
						o.uv = v.uv;
						//p.uv = v.uv;
						return o;
					}

							sampler2D _MainTex;

					sampler2D _BackgroundTexture;
					float _Distortion;
					float _Transparency;

					half4 frag(v2f i) : SV_Target
					{
						//float4 bgcolor = tex2D(_MainTex, i.uv);
						half4 bgcolor = tex2D(_BackgroundTexture, i.uv - _Distortion);
						//half4 bgcolor = tex2D(_MainTex, i.uv - _Distortion);
												bgcolor.a = tex2D(_MainTex, i.uv - _Distortion).a;
						return bgcolor;
						//	float4 color = tex2D(_MainTex, i.uv);
						//	return color;

						}

							ENDCG
						}

		}
}