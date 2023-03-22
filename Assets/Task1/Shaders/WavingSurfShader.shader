Shader "Unlit/WavingSurfShader"
{
	Properties
	{
        _Amplitude ("Amplitude", Range(0, 1)) = 0.1
        _Frequency ("Frequency", Range(0, 10)) = 1
        _Speed ("Speed", Range(0, 10)) = 1
		_ScrollSpeed ("Scroll Speed", Range(0, 10)) = 1
		_PhaseShift ("Phase Shift", Range(-3.14, 3.14)) = 0
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
        Tags {"Queue"="Transparent" "RenderType"="Opaque"}
		LOD 100

		Pass
		{
			CGPROGRAM
	        #pragma vertex vert
	        #pragma fragment frag
	        #include "UnityCG.cginc"

	        struct appdata {
	            float4 vertex : POSITION;
	            float2 uv : TEXCOORD0;
	        };

	        struct v2f {
	            float2 uv : TEXCOORD0;
	            float4 vertex : SV_POSITION;
	        };

	        float _Amplitude;
	        float _Frequency;
	        float _Speed;
			float _ScrollSpeed;
			float _PhaseShift;
	        sampler2D _MainTex;

	        v2f vert (appdata v) {
	            v2f o;
	            float4 pos = v.vertex;
	            const float pow = _Amplitude;
	        	const float xOff = _Time.y * _Speed;
	        	const float xFreq = pos.x * _Frequency;
	            const float dir = sin(xOff + xFreq + _PhaseShift);
	            pos.z += pow * dir;
	            o.vertex = UnityObjectToClipPos(pos);
	            o.uv = v.uv;
	            return o;
	        }

	        fixed4 frag (v2f i) : SV_Target {
				const float2 offset = float2(_Time.y * _ScrollSpeed, 0);
				const float2 uv = i.uv + offset;
	            fixed4 col = tex2D(_MainTex, uv);
	            return col;
	        }
	        ENDCG
		}
	}
}