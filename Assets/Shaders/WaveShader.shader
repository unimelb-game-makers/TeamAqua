Shader "Unlit/OceanShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseTex ("Noise Texture", 2D) = "white" {}
        _NoiseTex2 ("Noise Texture", 2D) = "white" {}
        _ScrollSpeed ("Scroll Speed", float) = 0.05
        _ScrollSpeed2 ("Scroll Speed", float) = -0.05
        _DistortionStrength ("Distortion Strength", Range(-1, 1)) = 0.2
        _WaveAmplitude ("Wave Amplitude", Range(0, 1)) = 0.2
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

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
                UNITY_FOG_COORDS(1)
            };

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            sampler2D _NoiseTex2;

            float _ScrollSpeed;
            float _ScrollSpeed2;
            float _DistortionStrength;

            float _WaveAmplitude;

            v2f vert (appdata v)
            {
                v2f o;
                // Apply simple displacement based on sine and cosine waves for a water-like effect
                float waveFrequency = 1.0; // Frequency of the waves

                // randomize the amplitude of the waves
                float waveAmplitude = _WaveAmplitude; // Amplitude of the waves

                // Calculate displacement using sine and cosine for a more dynamic effect
                float wave1 = waveAmplitude * sin(v.vertex.x * waveFrequency + _Time.y * 2.0);
                float wave2 = waveAmplitude * cos(v.vertex.z * waveFrequency + _Time.y * 2.0);

                // Combine the waves for displacement
                float4 displacement = float4(0.0f, wave1 + wave2, 0.0f, 0.0f);
                v.vertex += displacement;
 


                // Convert vertex position to clip space
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                // Transfer fog data
                UNITY_TRANSFER_FOG(o, o.vertex);

                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // Scroll the first noise texture and wrap UVs
                float2 noiseUV1 = i.uv + float2(_ScrollSpeed, _ScrollSpeed) * _Time.y;
                noiseUV1 = frac(noiseUV1);  // Ensure UVs stay within 0-1 range

                float4 noiseCol1 = tex2D(_NoiseTex, noiseUV1);

                // Scroll the second noise texture and wrap UVs
                float2 noiseUV2 = i.uv + float2(_ScrollSpeed2, 0) * _Time.y;
                noiseUV2 = frac(noiseUV2);  // Ensure UVs stay within 0-1 range

                float4 noiseCol2 = tex2D(_NoiseTex2, noiseUV2);

                // Combine the noise textures for displacement
                float displacement = noiseCol1.r * noiseCol2.r * 0.1; // Adjust this factor as needed

                // Apply UV displacement and distortion to the main texture
                float2 uv = i.uv + displacement * _DistortionStrength;

                // Scroll the main texture UVs and wrap them to prevent scrolling off the screen
                uv += float2(_ScrollSpeed, _ScrollSpeed) * _Time.y;
                uv = frac(uv);  // Wrap UVs to keep them within 0-1 range

                // Sample the main texture with the modified UVs
                float4 col = tex2D(_MainTex, uv);

                // Set alpha for transparency (consider making this a property)
                col.a = 0.2;

                // Apply fog (optional)
                UNITY_APPLY_FOG(i.fogCoord, col);

                return col;
            }

            ENDCG
        }
    }
    Fallback "Diffuse"
}
