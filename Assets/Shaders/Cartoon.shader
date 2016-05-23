Shader "Custom/Cartoon"
{
    Properties
    {
        // we have removed support for texture tiling/offset,
        // so make them not be displayed in material inspector
        [NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
        _ColorEdge ("Edge Color", Color) = (0,0,0,1)
        _Edge ("Edge limit", Range(0.0,1.0)) = 0.3
        _ColorMiddle ("Edge Color", Color) = (0.5,0.5,0.5,1)
		_Phong ("Phong limit", Range(0.0,1.0)) = 0.8
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            // use "vert" function as the vertex shader
            #pragma vertex vert
            // use "frag" function as the pixel (fragment) shader
            #pragma fragment frag

			float _Edge;
			float _Phong;
			float4 _ColorEdge;
			float4 _ColorMiddle;
            // vertex shader inputs
            struct appdata
            {
                float4 vertex : POSITION; // vertex position
                float2 uv : TEXCOORD0; // texture coordinate
                float3 normal : NORMAL;
            };

            // vertex shader outputs ("vertex to fragment")
            struct v2f
            {
                float2 uv : TEXCOORD0; // texture coordinate
                float4 vertex : SV_POSITION; // clip space position
                float3 normal : NORMAL;
            };

            // vertex shader
            v2f vert (appdata v)
            {
                v2f o;
                // transform position to clip space
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                // just pass the texture coordinate
                o.uv = v.uv;
                o.normal = mul(UNITY_MATRIX_IT_MV, v.normal);
                return o;
            }
            
            // texture we will sample
            sampler2D _MainTex;

            // pixel shader; returns low precision ("fixed4" type)
            // color ("SV_Target" semantic)
            fixed4 frag (v2f i) : SV_Target
            {
                // sample texture and return it
                fixed4 col = tex2D(_MainTex, i.uv);
               	if (i.normal.z < _Edge)
				{
					col *= _ColorEdge;
				}
				else if (i.normal.z < _Phong)
				{
						col *= _ColorMiddle;
				}
				
                return col;
            }
            ENDCG
        }
    }
}