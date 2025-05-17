Shader "Unlit/WavingWaterWithFoam"
{
    Properties
    {
        _Color("Water Color", Color) = (0.2, 0.6, 1, 0.8)
        _FoamColor("Foam Color", Color) = (1, 1, 1, 1)
        _MainTex ("Wave Pattern", 2D) = "white" {}
        _NoiseTex("Noise Texture", 2D) = "white" {}
        _ScrollSpeed("Scroll Speed", Float) = 0.05
        _WaveSpeed("Wave Speed", Float) = 0.5
        _WaveAmount("Wave Amount", Float) = 0.6
        _WaveHeight("Wave Height", Float) = 0.1
        _FoamWidth("Foam Width", Range(0,10)) = 3
        _FoamFalloff("Foam Falloff", Range(1,20)) = 5
        _WaterVisibility("Water Visibility", Range(0,1)) = 0.7
    }

    SubShader
    {
        Tags {
            "RenderType"="Transparent"
            "Queue"="Transparent-1"
            "IgnoreProjector"="True"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite On
        Cull Off

        GrabPass { "_WaterBackground" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };

            sampler2D _MainTex, _NoiseTex;
            sampler2D _CameraDepthTexture;
            sampler2D _WaterBackground;
            float4 _MainTex_ST;
            float _ScrollSpeed, _WaveSpeed, _WaveAmount, _WaveHeight;
            float _FoamWidth, _FoamFalloff, _WaterVisibility;
            float4 _Color, _FoamColor;

            v2f vert (appdata v) {
                v2f o;
                
                // Add wave animation to vertex position
                float4 noise = tex2Dlod(_NoiseTex, float4(v.uv.xy, 0, 0));
                v.vertex.y += sin(_Time.z * _WaveSpeed + (v.vertex.x * v.vertex.z * _WaveAmount * noise)) * _WaveHeight;
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.screenPos = ComputeScreenPos(o.vertex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                // Sample textures with scrolling
                float2 scrollUV = i.uv + _Time.x * _ScrollSpeed;
                float4 noise = tex2D(_NoiseTex, scrollUV);
                float4 water = tex2D(_MainTex, i.uv + noise.xy * 0.1);

                // Depth calculation for foam
                float sceneDepth = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPos)));
                float surfaceDepth = i.screenPos.w;
                float depthDiff = sceneDepth - surfaceDepth;

                // Foam calculation with sharp edge
                float foam = 1.0 - saturate(_FoamWidth * depthDiff);
                foam = pow(foam, _FoamFalloff);

                // Combine water and foam
                float4 finalColor = _Color * water;
                finalColor.rgb = lerp(finalColor.rgb, _FoamColor.rgb, foam);
                finalColor.a = lerp(_Color.a, 1.0, foam * _WaterVisibility);

                // Apply fog and return
                UNITY_APPLY_FOG(i.fogCoord, finalColor);
                return finalColor;
            }
            ENDCG
        }
    }
}