Shader "CustomRenderTexture/WaveEffectWithRandom"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Frequency ("Frequency", Float) = 1.0
        _Amplitude ("Amplitude", Float) = 0.05
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _Frequency;
            float _Amplitude;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Unity에서 제공하는 _Time 변수 사용 (_Time.y가 경과한 시간)
                float wave = sin(i.uv.y * _Frequency + _Time.y * (1.0 + _Frequency)) * _Amplitude;
                float2 uv = i.uv + float2(wave, 0);  // X축으로 물결 효과 추가
                return tex2D(_MainTex, uv);
            }
            ENDCG
        }
    }
}
