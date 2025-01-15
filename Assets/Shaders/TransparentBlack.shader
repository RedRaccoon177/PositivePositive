Shader "Custom/TransparentBlack"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Threshold ("Black Threshold", Range(0, 1)) = 0.1 // 범위 추가
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Threshold;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 texcoord : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, i.texcoord);

                // 검은색에 가까운 픽셀 투명 처리
                float brightness = (texColor.r + texColor.g + texColor.b) / 3.0;
                if (brightness < _Threshold) // 밝기가 임계값보다 작으면
                {
                    texColor.a = 0; // 투명화
                }

                return texColor;
            }
            ENDCG
        }
    }
}
