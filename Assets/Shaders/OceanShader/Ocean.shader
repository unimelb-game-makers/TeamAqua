Shader "Unlit/Simple Water"
{
	Properties
	{
		_Color("Tint", Color) = (1, 1, 1, .5) 
		_FoamC("Foam", Color) = (1, 1, 1, .5) 
		_MainTex ("Main Texture", 2D) = "white" {}
		_TextureDistort("Texture Wobble", range(0,1)) = 0.1
		_NoiseTex("Extra Wave Noise", 2D) = "white" {}
        _ScrollSpeed ("Scroll Speed", float) = 0.05
        _ScrollSpeed2 ("Scroll Speed", float) = -0.05
		_Speed("Wave Speed", Range(0,1)) = 0.5
		_Amount("Wave Amount", Range(0,1)) = 0.6
		_Scale("Scale", Range(0,1)) = 0.5
		_Height("Wave Height", Range(0,1)) = 0.1
		_Foam("Foamline Thickness", Range(0,10)) = 8
	}
	SubShader
	{
		Tags { "RenderType"="Opaque"  "Queue" = "Transparent" }
		LOD 100
		Blend OneMinusDstColor One
		Cull Off
 
		GrabPass{
			Name "BASE"
			Tags{ "LightMode" = "Always" }
				}
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
 
			#include "UnityCG.cginc"
 
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};
 
			struct v2f
			{
				float2 uv : TEXCOORD3;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float4 scrPos : TEXCOORD2;//
				float4 worldPos : TEXCOORD4;//
			};
			float _TextureDistort;
			float4 _Color;
			sampler2D _CameraDepthTexture; //Depth Texture
			sampler2D _MainTex, _NoiseTex;//
            float _ScrollSpeed;
            float _ScrollSpeed2;
			float4 _MainTex_ST;
			float _Speed, _Amount, _Height, _Foam, _Scale;// 
			float4 _FoamC;
 
			v2f vert (appdata v)
			{
				v2f o;
				UNITY_INITIALIZE_OUTPUT(v2f, o);
				float4 tex = tex2Dlod(_NoiseTex, float4(v.uv.xy, 0, 0));//extra noise tex
				v.vertex.y += sin(_Time.z * _Speed + (v.vertex.x * v.vertex.z * _Amount * tex)) * _Height;//movement
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
 
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.scrPos = ComputeScreenPos(o.vertex); 
				UNITY_TRANSFER_FOG(o,o.vertex);			
				return o;
			}
 
			fixed4 frag (v2f i) : SV_Target
			{
                // Scroll the first noise texture and wrap UVs
                float2 noiseUV1 = i.uv + float2(_ScrollSpeed, _ScrollSpeed) * _Time.y;
                noiseUV1 = frac(noiseUV1);  // Ensure UVs stay within 0-1 range

                float4 noiseCol1 = tex2D(_NoiseTex, noiseUV1);

                // Combine the noise textures for displacement
                float displacement = noiseCol1.r * 0.1; // Adjust this factor as needed

                // Apply UV displacement and distortion to the main texture
                float2 uv = i.uv + displacement * _TextureDistort;

                // Scroll the main texture UVs and wrap them to prevent scrolling off the screen
                uv += float2(_ScrollSpeed, _ScrollSpeed) * _Time.y;
                uv = frac(uv);  // Wrap UVs to keep them within 0-1 range

				fixed distortx = tex2D(_NoiseTex, (i.uv * _Scale)  + (_Time.x * 2)).r;// distortion alpha
 
				// half4 col = tex2D(_MainTex, (i.uv * _Scale) - (distortx * _TextureDistort));// texture times tint;			
				half4 col = tex2D(_MainTex, uv);

				// Foam Stuff
				half depth = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.scrPos ))); // depth
				half4 foamLine =1 - saturate(_Foam* (depth - i.scrPos.w ) ) ;// foam line by comparing depth and screenposition
				col *= _Color;
				col += (step(0.4 * distortx,foamLine) * _FoamC); // add the foam line and tint to the texture
				col = saturate(col) * col.a;


                // Apply fog (optional)
                UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}