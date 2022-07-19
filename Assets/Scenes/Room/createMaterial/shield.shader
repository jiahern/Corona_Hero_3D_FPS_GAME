Shader "Unlit/shield"
{
    Properties
    {
        _Color("Color", Color) = (0,0,0,0)
        _HexEdgeColor("Hex Edge Color", COLOR) = (0,0,0,0)
        _PulseTex("Hex Pulse Texture", 2D) = "white" {}
        _HexEdgeTex("Hex Edge Texture", 2D) = "white" {}
        _PulseIntensity ("Hex Pulse Intensity", float) = 3.0
        _PulseTimeScale("Hex Pulse Time Scale", float) = 2.0
        _PulsePosScale("Hex Pulse Position Scale", float) = 50.0
        _PulseTexOffsetScale("Hex Pulse Texture Offset Scale", float) = 1.5
        _HexEdgeIntensity("Hex Edge Intensity", float) = 2.0
        _HexEdgeTimeScale("Hex Edge Time Scale", float) = 2.0
        _HexEdgeWidthModifier("Hex Edge Width Modifier", Range(0,1)) = 0.8
        _HexEdgePosScale("Hex Edge Position Scale", float) = 80.0
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType"="Transparent"  }
        Cull Off
        Blend One One

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            
            float4 _Color;
            sampler2D _PulseTex;
            float4 _PulseTex_ST;
            sampler2D _HexEdgeTex;
            float4 _HexEdgeTex_ST;
            float4 _HexEdgeColor;

            float _PulseIntensity;
            float _PulseTimeScale;
            float _PulsePosScale;
            float _PulseTexOffsetScale;
            float _HexEdgeIntensity;
            float _HexEdgeTimeScale;
            float _HexEdgeWidthModifier;
            float _HexEdgePosScale;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 vertexObjPos : TEXCOORD1;
            };

            

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _PulseTex);
                o.vertexObjPos = v.vertex;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float verticalDist = abs(i.vertexObjPos.z);

                float4 hexEdgeTex = tex2D(_HexEdgeTex, i.uv);
                

                float4 pulseTex = tex2D(_PulseTex, i.uv);
                float horizontalDist = abs(i.vertexObjPos.x);
                fixed4 hexEdgeTerm = hexEdgeTex * _HexEdgeColor * _HexEdgeIntensity *
                    max(sin((horizontalDist + verticalDist) * _HexEdgePosScale - _Time.y * _HexEdgeTimeScale) - _HexEdgeWidthModifier, 0.0f) *
                    (1 / (1 - _HexEdgeWidthModifier));
                float4 pulseTerm = pulseTex * _Color * _PulseIntensity *
                    abs(sin(_Time.y * _PulseTimeScale + horizontalDist * _PulsePosScale + pulseTex.r * _PulseTexOffsetScale));
                return float4(_Color.rgb + pulseTerm.rgb + hexEdgeTerm.rgb, _Color.a);
            }
            ENDCG
        }
    }
}
