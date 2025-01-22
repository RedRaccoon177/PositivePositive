Shader "Custom/Outline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {} // 메인 텍스처
        _OutlineColor ("Outline Color", Color) = (1, 0, 0, 1) // 외곽선 색상
        _OutlineThickness ("Outline Thickness", Float) = 0.05 // 외곽선 두께
    }
    SubShader
    {
        Tags { "Queue"="Overlay" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _OutlineColor;
            float _OutlineThickness;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // 텍스처 샘플링 (현재 픽셀)
                fixed4 texColor = tex2D(_MainTex, i.uv);

                // 주변 픽셀 샘플링 (외곽선 생성용)
                float2 offsetX = float2(_OutlineThickness, 0);
                float2 offsetY = float2(0, _OutlineThickness);

                float alphaLeft = tex2D(_MainTex, i.uv - offsetX).a;
                float alphaRight = tex2D(_MainTex, i.uv + offsetX).a;
                float alphaUp = tex2D(_MainTex, i.uv + offsetY).a;
                float alphaDown = tex2D(_MainTex, i.uv - offsetY).a;

                // 외곽선 생성 조건
                float outline = step(0.1, alphaLeft) + step(0.1, alphaRight) + step(0.1, alphaUp) + step(0.1, alphaDown);
                outline = clamp(outline, 0, 1);

                // 아웃라인 색상 적용
                if (outline > 0 && texColor.a == 0)
                {
                    return _OutlineColor;
                }

                // 원래 텍스처 색상 반환
                return texColor;
            }
            ENDCG
        }
    }
}
