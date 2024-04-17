Shader "Source/ColorTransitionBox"
{
    Properties
    {
        _Duration ("Duration Value", Range(0, 1)) = 0
        _StartColor ("Start Color", Color) = (1, 0, 0, 1)
        _EndColor ("End Color", Color) = (0, 0, 1, 1)
        _BorderColor ("Border Color", Color) = (0, 0, 1, 1)
        _BorderSize ("Border Size", Vector) = (1,1,0)
        _AlphaValue ("Alpha Value", Range(0, 1)) = 1
        _MainTex ("Main Texture", 2D) = "white" {}
        _SecondTex ("Second Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Transparent"
            "Queue"="Transparent"
        }

        Zwrite Off

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha

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

            float _Duration;
            float4 _StartColor;
            float4 _EndColor;
            float4 _BorderColor;
            float2 _BorderSize;
            float _AlphaValue;
            sampler2D _MainTex;
            sampler2D _SecondTex;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 center = float2(0.5, 0.5);

                fixed4 textureColor = tex2D(_MainTex, i.uv);
                fixed4 textureColorEnd = tex2D(_SecondTex, i.uv);
                textureColorEnd.a *= _EndColor.a;
                textureColor.a *= _StartColor.a;
                fixed4 color;

                bool onBorder = (i.uv.x < _BorderSize.x || i.uv.x > 1.0 - _BorderSize.x || i.uv.y < _BorderSize.y || i.
                    uv.y >
                    1.0 - _BorderSize.y);

                if (onBorder)
                {
                    color = _BorderColor;
                }
                else if (distance(i.uv.y, 0) <= _Duration)

                {
                    color = _EndColor;
                    fixed4 desired_color = textureColor + textureColorEnd;
                    color.rgb *= desired_color.rgb;
                    color.a = desired_color.a;
                }
                else
                {
                    color = _StartColor;
                    color.rgb *= textureColor.rgb;
                    color.a = textureColor.a;
                }

                color.a *= _AlphaValue;

                return color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}