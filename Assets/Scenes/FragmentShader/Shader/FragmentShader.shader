Shader "Custom/FragmentShader" {
	Properties {
		_RenderTexture ("RenderTexture (RGB)", 2D) = "white" {}
		_PatternTexture ("PatternTexture", 2D) = "white" {}
	}
	SubShader {
		Pass{
		CGPROGRAM
		#pragma vertex vert_img
		#pragma fragment frag

		#include "UnityCG.cginc"
			sampler2D _RenderTexture;
			sampler2D _PatternTexture;

			fixed3 frag(v2f_img i) : SV_Target
			{
				return tex2D(_RenderTexture, i.uv) * tex2D(_PatternTexture, i.uv);

			}
		ENDCG
		}
	}
}
