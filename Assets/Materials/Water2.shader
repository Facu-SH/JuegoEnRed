// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Water2"
{
	Properties
	{
		_TessValue( "Max Tessellation", Range( 1, 32 ) ) = 32
		_VoronoiScale("VoronoiScale", Float) = 0
		_WaveAmplitud("WaveAmplitud", Float) = 0
		_oldVoronoi("oldVoronoi", Vector) = (0,0,0,0)
		_Water2("Water2", Color) = (0,0,0,0)
		_Water1("Water1", Color) = (0,0,0,0)
		_TimeScale("TimeScale", Float) = 0
		_WaveSpeed("WaveSpeed", Float) = 0
		_WaveFrequency("WaveFrequency", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 4.6
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc tessellate:tessFunction 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _WaveFrequency;
		uniform float _TimeScale;
		uniform float _WaveSpeed;
		uniform float _WaveAmplitud;
		uniform float4 _Water1;
		uniform float4 _Water2;
		uniform float _VoronoiScale;
		uniform float2 _oldVoronoi;
		uniform float _TessValue;


		float2 voronoihash1( float2 p )
		{
			
			p = float2( dot( p, float2( 127.1, 311.7 ) ), dot( p, float2( 269.5, 183.3 ) ) );
			return frac( sin( p ) *43758.5453);
		}


		float voronoi1( float2 v, float time, inout float2 id, inout float2 mr, float smoothness )
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
			 		float2 o = voronoihash1( n + g );
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


		float4 tessFunction( )
		{
			return _TessValue;
		}

		void vertexDataFunc( inout appdata_full v )
		{
			float3 ase_vertex3Pos = v.vertex.xyz;
			float mulTime24 = _Time.y * _TimeScale;
			float4 appendResult33 = (float4(0.0 , ( ( sin( ( ( _WaveFrequency * ase_vertex3Pos.x ) - ( mulTime24 * _WaveSpeed ) ) ) * _WaveAmplitud ) - ase_vertex3Pos.y ) , 0.0 , 0.0));
			float4 psotionChanged34 = appendResult33;
			v.vertex.xyz += psotionChanged34.xyz;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float time1 = 0.0;
			float2 coords1 = i.uv_texcoord * ( _VoronoiScale + _SinTime.w );
			float2 id1 = 0;
			float2 uv1 = 0;
			float voroi1 = voronoi1( coords1, time1, id1, uv1, 0 );
			float clampResult50 = clamp( (0.0 + (voroi1 - _oldVoronoi.x) * (1.0 - 0.0) / (_oldVoronoi.y - _oldVoronoi.x)) , 0.0 , 1.0 );
			float4 lerpResult44 = lerp( _Water1 , _Water2 , clampResult50);
			o.Albedo = lerpResult44.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
2167;178;1430;823;1141.514;-524.0424;1.081441;True;False
Node;AmplifyShaderEditor.RangedFloatNode;25;-1270.572,915.3505;Inherit;False;Property;_TimeScale;TimeScale;10;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;20;-1119.145,770.0367;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;19;-1129.032,673.7898;Inherit;False;Property;_WaveFrequency;WaveFrequency;12;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;24;-1110.161,921.0117;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-1098.837,1000.273;Inherit;False;Property;_WaveSpeed;WaveSpeed;11;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-895.1938,738.9542;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-895.923,922.7405;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;26;-741.4595,835.2565;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;2;-1237.773,201.1304;Inherit;False;Property;_VoronoiScale;VoronoiScale;5;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinTimeNode;7;-1248.091,341.489;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;28;-647.4031,971.1989;Inherit;False;Property;_WaveAmplitud;WaveAmplitud;6;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;27;-583.4031,835.1989;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;8;-1008.655,278.5339;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-415.4031,835.1989;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VoronoiNode;1;-865.201,167.0727;Inherit;False;0;0;1;0;1;False;1;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;3;FLOAT;0;FLOAT2;1;FLOAT2;2
Node;AmplifyShaderEditor.SimpleSubtractOpNode;30;-237.4031,833.1989;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;5;-841.4637,289.8866;Inherit;False;Property;_oldVoronoi;oldVoronoi;7;0;Create;True;0;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TFHCRemapNode;4;-619.5735,166.0407;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;33;-42.40308,829.1989;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ClampOpNode;50;-401.9436,440.7772;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;34;100.3812,826.1884;Inherit;False;psotionChanged;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ColorNode;49;-367.0898,26.48354;Inherit;False;Property;_Water1;Water1;9;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;46;-367.0898,249.4835;Inherit;False;Property;_Water2;Water2;8;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;35;272.0314,439.2757;Inherit;False;34;psotionChanged;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.LerpOp;44;-154.3156,344.037;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;500.4999,159.9001;Float;False;True;-1;6;ASEMaterialInspector;0;0;Standard;Water2;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;True;1;32;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;0;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;24;0;25;0
WireConnection;21;0;19;0
WireConnection;21;1;20;1
WireConnection;23;0;24;0
WireConnection;23;1;22;0
WireConnection;26;0;21;0
WireConnection;26;1;23;0
WireConnection;27;0;26;0
WireConnection;8;0;2;0
WireConnection;8;1;7;4
WireConnection;29;0;27;0
WireConnection;29;1;28;0
WireConnection;1;2;8;0
WireConnection;30;0;29;0
WireConnection;30;1;20;2
WireConnection;4;0;1;0
WireConnection;4;1;5;1
WireConnection;4;2;5;2
WireConnection;33;1;30;0
WireConnection;50;0;4;0
WireConnection;34;0;33;0
WireConnection;44;0;49;0
WireConnection;44;1;46;0
WireConnection;44;2;50;0
WireConnection;0;0;44;0
WireConnection;0;11;35;0
ASEEND*/
//CHKSM=9FF8DE3DCAE393D523F275A5FA6FFFEF5EA091DD