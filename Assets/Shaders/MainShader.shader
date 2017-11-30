Shader "Custom/MainShader" {
	// Properties copy pasted from standard shader
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo", 2D) = "white" {}

	_Cutoff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5

		_Glossiness("Smoothness", Range(0.0, 1.0)) = 0.5
		_GlossMapScale("Smoothness Scale", Range(0.0, 1.0)) = 1.0
		[Enum(Metallic Alpha,0,Albedo Alpha,1)] _SmoothnessTextureChannel("Smoothness texture channel", Float) = 0

		[Gamma] _Metallic("Metallic", Range(0.0, 1.0)) = 0.0
		_MetallicGlossMap("Metallic", 2D) = "white" {}

	[ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
		[ToggleOff] _GlossyReflections("Glossy Reflections", Float) = 1.0

		_BumpScale("Scale", Float) = 1.0
		_BumpMap("Normal Map", 2D) = "bump" {}

	_Parallax("Height Scale", Range(0.005, 0.08)) = 0.02
		_ParallaxMap("Height Map", 2D) = "black" {}

	_OcclusionStrength("Strength", Range(0.0, 1.0)) = 1.0
		_OcclusionMap("Occlusion", 2D) = "white" {}

	_EmissionColor("Color", Color) = (0,0,0)
		_EmissionMap("Emission", 2D) = "white" {}

	_DetailMask("Detail Mask", 2D) = "white" {}

	_DetailAlbedoMap("Detail Albedo x2", 2D) = "grey" {}
	_DetailNormalMapScale("Scale", Float) = 1.0
		_DetailNormalMap("Normal Map", 2D) = "bump" {}

	[Enum(UV0,0,UV1,1)] _UVSec("UV Set for secondary textures", Float) = 0


		// Blending state
		[HideInInspector] _Mode("__mode", Float) = 0.0
		[HideInInspector] _SrcBlend("__src", Float) = 1.0
		[HideInInspector] _DstBlend("__dst", Float) = 0.0
		[HideInInspector] _ZWrite("__zw", Float) = 1.0
	}
	SubShader {
		UsePass "Standard/FORWARD"
		UsePass "Standard/FORWARD_DELTA"
		UsePass "Standard/SHADOWCASTER"
		UsePass "Standard/DEFERRED"
		UsePass "Standard/META"
		
		Pass {
			Name "FOG_PASS"
			Cull Off
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma target 4.0
			#pragma vertex vert
			#pragma fragment frag
			#pragma debug

			#include "UnityCG.cginc"

			#define FOG_COLOR fixed4(0.68, 0.68, 0.8, 1)
			#define FOG_DISTANCE 0.15

			uniform int _FogEnabled;

			struct vInput {
				float4 vertex : POSITION;
			};

			struct fInput {
				float4 position : POSITION;
			};

			fInput vert(vInput i) {
				fInput o;
				o.position = UnityObjectToClipPos(i.vertex);

				return o;
			}

			fixed4 frag(fInput i) : SV_TARGET{
				fixed fogStrength = (1 - i.position.z * FOG_DISTANCE * _ProjectionParams.z);
				fixed4 fogLight = FOG_COLOR * fogStrength * (_FogEnabled ? 1 : 0);
				return fogLight;
			}

			ENDCG
		}
		Pass {
			Name "LIGHT_PASS"
			Cull Off
			ZWrite Off
			Blend SrcAlpha One
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma debug

			#include "UnityCG.cginc"

			#define AMBIENT_LIGHT_COLOR fixed4(0.221, 0.18, 0.25, 1)
			#define FLASHLIGHT_COLOR fixed4(0.9, 0.82, 0.52, 1)

			#define FLASHLIGHT_SPREAD 100
			#define FLASHLIGHT_INITIAL_SIZE 100
			#define FLASHLIGHT_FALLOFF 1

			uniform int _Night;
			uniform int _FlashlightEnabled;

			struct vInput {
				float4 vertex : POSITION;
			};

			struct fInput {
				float4 position : SV_POSITION;
			};

			fInput vert(vInput i) {
				fInput o;
				o.position = UnityObjectToClipPos(i.vertex);

				return o;
			}

			fixed4 frag(fInput i) : SV_TARGET {
				fixed2 flashlightPos = fixed2(_ScreenParams.x / 2, _ScreenParams.y / 3);
				fixed2 posDiff = fixed2(i.position.x - flashlightPos.x, i.position.y - flashlightPos.y);
				fixed dst = 1 - (i.position.z * 100);
				int flashRegion = (length(posDiff) < FLASHLIGHT_INITIAL_SIZE + (dst * FLASHLIGHT_SPREAD)) ? 1 : 0;
				fixed4 ambientLight = AMBIENT_LIGHT_COLOR * (_Night ? 0 : 1);
				fixed4 flashLight = FLASHLIGHT_COLOR * flashRegion * _FlashlightEnabled;
				flashLight *= clamp(1 - dst * dst * FLASHLIGHT_FALLOFF, 0, 1);
				flashLight.w = 1;
				return ambientLight + flashLight;
			}

			ENDCG
		}
	}
}
