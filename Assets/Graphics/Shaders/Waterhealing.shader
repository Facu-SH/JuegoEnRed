// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "DeepWater"
{
	Properties
	{
		_Float0("Float 0", Float) = 66.67
		_Bias("Bias", Float) = 8.2
		_VoronoiScale("VoronoiScale", Float) = 9.4
		_Escala("Escala", Float) = 0.06
		_Intensidad("Intensidad", Float) = 1.6
		_Bias2("Bias2", Float) = 0.93
		_Color0("Color 0", Color) = (0.3215686,0.8156863,0.9058824,0)
		_FrecuenciadeOlas("FrecuenciadeOlas", Float) = 6.1
		_Color1("Color 1", Color) = (0.4392157,0.9607843,1,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityCG.cginc"
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float4 screenPos;
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform float4 _Color0;
		uniform float4 _Color1;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _Bias2;
		uniform float _Float0;
		uniform float _Bias;
		uniform float _Escala;
		uniform float _Intensidad;
		uniform float _VoronoiScale;
		uniform float _FrecuenciadeOlas;


		float2 voronoihash43( float2 p )
		{
			
			p = float2( dot( p, float2( 127.1, 311.7 ) ), dot( p, float2( 269.5, 183.3 ) ) );
			return frac( sin( p ) *43758.5453);
		}


		float voronoi43( float2 v, float time, inout float2 id, inout float2 mr, float smoothness )
		{
			float2 n = floor( v );
			float2 f = frac( v );
			float F1 = 8.0;
			float F2 = 8.0; float2 mg = 0;
			for ( int j = -1; j <= 1; j++ )
			{
				for ( int i = -1; i <= 1; i++ )
			 	{
			 		float2 g = float2( i, j );
			 		float2 o = voronoihash43( n + g );
					o = ( sin( time + o * 6.2831 ) * 0.5 + 0.5 ); float2 r = f - g - o;
					float d = 0.5 * dot( r, r );
			 		if( d<F1 ) {
			 			F2 = F1;
			 			F1 = d; mg = g; mr = r; id = o;
			 		} else if( d<F2 ) {
			 			F2 = d;
			 		}
			 	}
			}
			return F1;
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float screenDepth2 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth2 = abs( ( screenDepth2 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( 1.0 ) );
			float temp_output_28_0 = ( 1.0 - saturate( pow( ( distanceDepth2 + _Bias2 ) , _Float0 ) ) );
			float4 lerpResult36 = lerp( _Color0 , _Color1 , temp_output_28_0);
			float4 Colorbase70 = ( lerpResult36 * ( saturate( pow( ( ( distanceDepth2 + _Bias ) * _Escala ) , _Intensidad ) ) + temp_output_28_0 ) );
			float4 color66 = IsGammaSpace() ? float4(0.759434,0.9831772,1,0) : float4(0.5373389,0.9621564,1,0);
			float time43 = 0.0;
			float2 coords43 = i.uv_texcoord * _VoronoiScale;
			float2 id43 = 0;
			float2 uv43 = 0;
			float voroi43 = voronoi43( coords43, time43, id43, uv43, 0 );
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float MascaraEfectoMovimiento68 = saturate( ( (0.0 + (voroi43 - 0.0) * (1.0 - 0.0) / (0.3 - 0.0)) * cos( ( ( ase_vertex3Pos.x + _SinTime.w ) * _FrecuenciadeOlas ) ) ) );
			float4 lerpResult65 = lerp( Colorbase70 , color66 , MascaraEfectoMovimiento68);
			o.Emission = lerpResult65.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
0;73;906;465;1638.809;-302.1545;1.3;True;False
Node;AmplifyShaderEditor.CommentaryNode;71;-1719.394,-402.5633;Inherit;False;1527.235;791.2426;Color con profundidad y borde;20;19;23;18;16;7;22;25;8;9;28;41;2;14;6;15;42;5;36;1;70;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;69;-1483.682,418.4256;Inherit;False;1283.266;570.2533;mascara Efecto Curacion movimiento;12;55;50;54;52;64;63;47;43;44;58;67;68;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-1392.192,98.37812;Inherit;False;Property;_Bias;Bias;1;0;Create;True;0;0;0;False;0;False;8.2;8.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;2;-1669.394,1.312874;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-1546.88,195.7;Inherit;False;Property;_Bias2;Bias2;5;0;Create;True;0;0;0;False;0;False;0.93;0.93;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-1258.543,97.33469;Inherit;False;Property;_Escala;Escala;3;0;Create;True;0;0;0;False;0;False;0.06;0.06;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;55;-1403.594,584.5004;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SinTimeNode;54;-1372.594,727.5004;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;6;-1247.845,2.163765;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;18;-1388.187,175.3461;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-1418.175,272.6793;Inherit;False;Property;_Float0;Float 0;0;0;Create;True;0;0;0;False;0;False;66.67;66.67;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;22;-1259.725,177.0527;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-1120.499,0.9028707;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;44;-1395.682,505.6785;Inherit;False;Property;_VoronoiScale;VoronoiScale;2;0;Create;True;0;0;0;False;0;False;9.4;9.4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;50;-1186.682,643.6784;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;52;-1433.682,872.6784;Inherit;False;Property;_FrecuenciadeOlas;FrecuenciadeOlas;7;0;Create;True;0;0;0;False;0;False;6.1;6.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-1131.199,96.59492;Inherit;False;Property;_Intensidad;Intensidad;4;0;Create;True;0;0;0;False;0;False;1.6;1.6;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;8;-983.0666,-0.3578181;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.VoronoiNode;43;-1161.889,471.517;Inherit;False;0;0;1;0;1;False;1;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;3;FLOAT;0;FLOAT2;1;FLOAT2;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;64;-1051.977,642.7348;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;25;-1112.94,177.0255;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;9;-844.3749,-0.3580165;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;1;-1108.983,-352.5633;Inherit;False;Property;_Color0;Color 0;6;0;Create;True;0;0;0;False;0;False;0.3215686,0.8156863,0.9058824,0;0.3215686,0.8156863,0.9058824,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CosOpNode;63;-918.0919,641.6901;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;28;-977.5317,176.2071;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;47;-976.6821,470.6788;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0.3;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;5;-1094.816,-182.1532;Inherit;False;Property;_Color1;Color 1;8;0;Create;True;0;0;0;False;0;False;0.4392157,0.9607843,1,0;0.4392157,0.9607843,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;36;-764.2156,-145.8308;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;58;-775.5931,471.5008;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;41;-700.9666,-0.484817;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;67;-634.1254,474.9494;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-567.0579,-111.9468;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;68;-493.9149,469.7255;Inherit;False;MascaraEfectoMovimiento;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;70;-416.1596,-113.5406;Inherit;False;Colorbase;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;72;-97.06372,227.0286;Inherit;False;68;MascaraEfectoMovimiento;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;66;-77.70752,41.80467;Inherit;False;Constant;_Color2;Color 2;2;0;Create;True;0;0;0;False;0;False;0.759434,0.9831772,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;73;21.93628,-47.97144;Inherit;False;70;Colorbase;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;65;235.2922,27.80477;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;398.3037,-20.83385;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;DeepWater;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;6;0;2;0
WireConnection;6;1;14;0
WireConnection;18;0;2;0
WireConnection;18;1;19;0
WireConnection;22;0;18;0
WireConnection;22;1;23;0
WireConnection;7;0;6;0
WireConnection;7;1;15;0
WireConnection;50;0;55;1
WireConnection;50;1;54;4
WireConnection;8;0;7;0
WireConnection;8;1;16;0
WireConnection;43;2;44;0
WireConnection;64;0;50;0
WireConnection;64;1;52;0
WireConnection;25;0;22;0
WireConnection;9;0;8;0
WireConnection;63;0;64;0
WireConnection;28;0;25;0
WireConnection;47;0;43;0
WireConnection;36;0;1;0
WireConnection;36;1;5;0
WireConnection;36;2;28;0
WireConnection;58;0;47;0
WireConnection;58;1;63;0
WireConnection;41;0;9;0
WireConnection;41;1;28;0
WireConnection;67;0;58;0
WireConnection;42;0;36;0
WireConnection;42;1;41;0
WireConnection;68;0;67;0
WireConnection;70;0;42;0
WireConnection;65;0;73;0
WireConnection;65;1;66;0
WireConnection;65;2;72;0
WireConnection;0;2;65;0
ASEEND*/
//CHKSM=3AF1FBD47E2820F7C3FA53F6AC6895D7B6160886