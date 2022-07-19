Shader "Unlit/testWater"
{
    Properties
    {
      _MainTex ("Main texture", 2D) = "white" {}
      _Color ("Color", Color) = (1,1,1,1)
      _NormalRefract ("normal refract",float) = 1
      _NormalScale ("normal scale",Range(0,1)) = 0.5
      _Specular ("specular",float) = 0.5
      _Glossy ("glossy",float) = 0.5
      _Amp1 ("Amplititude1",Range(0,1)) = 0.5
      _Amp2 ("Amplititude2",Range(0,1)) = 0.5
      _Amp3 ("Amplititude3",Range(0,1)) = 0.5
      _WaveLength1 ("Wave Length 1",float) = 1
      _WaveLength2 ("Wave Length 2",float) = 1
      _WaveLength3 ("Wave Length 3",float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        

        Pass
        {
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            
           
            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _Color;
            float _NormalScale;
            float _NormalRefract;
            float _Specular;
            float _Glossy;
            float _Amp1;
            float _Amp2;
            float _Amp3;
            float _WaveLength1;
            float _WaveLength2;
            float _WaveLength3;

            struct MeshData
            {
                float4 vertex : POSITION;
                float3 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
            };

            struct Interpolator
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;  
                float3 normal:TEXCOORD1;
                float4 TtoW0:TEXCOORD2;
                float4 TtoW1:TEXCOORD3;
                float4 TtoW2:TEXCOORD4;
                
            };

            

            Interpolator vert (MeshData v)
            {
                Interpolator o;

                
                
                float3 worldPos = mul(unity_ObjectToWorld,v.vertex).xyz;
                float3 worldNormal = UnityObjectToWorldNormal(v.normal);
                fixed3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);  
                fixed3 worldBinormal = cross(worldNormal, worldTangent) * v.tangent.w; 
                
                o.TtoW0 = float4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x); 
                o.TtoW1 = float4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y); 
                o.TtoW2 = float4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z); 
               
                // _CameraDepthTexture
                // _CameraDepthNormalsTexture
                // float wave = sin(dot(_D.xz,v.uv) * _w + _Time.y * _g);
                // float wave2 = sin(dot(_D2.xz,v.uv) * _w2 + _Time.y * _g2);
                // v.vertex.y = wave+wave2;
                 
                float wave = sin(v.vertex.x*2/_WaveLength1 + _Time.y * 2 / _WaveLength1) * _Amp1;
                float wave2 = -sin(v.vertex.y*2/_WaveLength2 + _Time.y * 2 / _WaveLength2) * _Amp2;
                float wave3 = sin(v.vertex.z*2/_WaveLength3 + _Time.y * 2 / _WaveLength3) * _Amp3;
                float wave4 = sin(v.vertex.zy*2/_WaveLength1 + _Time.y * 2 / _WaveLength1) * _Amp1;
                float wave5 = sin(v.vertex.zx*2/_WaveLength1 + _Time.y * 2 / _WaveLength1) * _Amp1;
                float wave6 = cos(v.vertex.yz*2/_WaveLength1 + _Time.y * 2 / _WaveLength1) * _Amp1;
                float wave7 = sin(v.vertex.xz*2/_WaveLength1 + _Time.y * 2 / _WaveLength1) * _Amp1;
                // float wave3 = cos((v.uv.z - _Time.y * 0.1) *TAU*5 );
                
                v.vertex.y += (wave + wave2 + wave3+wave4+wave5+wave6+wave7) ;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
               
                o.uv = TRANSFORM_TEX(v.uv,_MainTex);
                return o;
            }

            float4 frag (Interpolator i) : SV_Target
            {

                float3 worldPos = float3(i.TtoW0.w, i.TtoW1.w, i.TtoW2.w);
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                float3 viewDir = normalize(UnityWorldSpaceViewDir(worldPos));
                float3 halfDir = normalize(lightDir + viewDir);


                float4 offsetColor = (tex2D(_MainTex, i.uv 
                + float2(_Time.x,0)) 
                + tex2D(_MainTex, float2(i.uv.y,i.uv.x) 
                + float2(_Time.x,0)))/2;
 
                //   float4 waveOffset = tex2D(_NormalTex ,i.uv +  wave_offset);
                half2 offset = UnpackNormal(offsetColor).xy * _NormalRefract;


                fixed3 tangentNormal1 = UnpackNormal(tex2D(_MainTex , i.uv  + offset)).rgb;
                fixed3 tangentNormal2 = UnpackNormal(tex2D(_MainTex , i.uv  - offset)).rgb;
                fixed3 tangentNormal = normalize(tangentNormal1 + tangentNormal2);
                tangentNormal.xy *= _NormalScale;
                tangentNormal.z = sqrt(1 - saturate(dot(tangentNormal.xy, tangentNormal.xy)));
                float3 worldNormal = normalize(half3(dot(i.TtoW0.xyz, tangentNormal), dot(i.TtoW1.xyz, tangentNormal), dot(i.TtoW2.xyz, tangentNormal)));

                float NdotH = max(0,dot(halfDir , worldNormal));  //BlinnPhong
                float NdotL = max(0,dot(worldNormal , lightDir)); // 漫反射


                fixed3 diffuse = _LightColor0.rgb*_Color*saturate(dot(worldNormal , lightDir)) ;
                fixed3 specular = pow( NdotH , _Specular * 128.0) * _Glossy;
                float3 ambient = _Color*UNITY_LIGHTMODEL_AMBIENT.xyz;


            //    float wave = cos((i.uv.y - _Time.y * 0.1) *TAU*5 )*0.5*0.5;
               //float wave2 = sin(dot(_D2.xz,i.uv) * _w2 + _Time.y * _g2);
                                
               return float4(tex2D(_MainTex, i.uv)*_Color + (diffuse + specular + ambient),1);
            }
            ENDCG
        }
    }
}
