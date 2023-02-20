Shader "Custom/ShaderFantasma"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        [NoScaleOffset] _NormalMap("Normals",2D)= "bump"{}
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
       _Metallic ("Metallic", Range(0,1)) = 0.0
       _Amount("Amount", Range(0,.3)) = .3
       _displacementAmount("Displacement Amount", Range(0,5)) = .3
       _DisplacementTexture("Displacement Texture", 2D) = "white"{} //displacement texture
    }
    SubShader
    {
        Tags { "RenderType"="Opaque"  "ForceNoShadowCasting" = "True"}
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float multiplyValue; 
            float displacementValue;
            float2 uv_NormalMap;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        fixed _Amount;
        fixed _displacementAmount;
        sampler2D _DisplacementTexture;
        sampler2D _NormalMap;

      
        void vert(inout appdata_full v, out Input o) {

            //moves the object horizontally, in this case we add the sin of _Time
	       	float multiplyValue = abs(sin(_Time * 30 + v.vertex.y)); //how much we want to multiply our vertex
			v.vertex.x *= multiplyValue * v.normal.x;
			v.vertex.z *= multiplyValue * v.normal.y;

            //extrude
            v.vertex.xyz += _Amount * v.normal.xyz;

            	//How much we expand, based on our DisplacementTexture
				float value = tex2Dlod(_DisplacementTexture, v.texcoord*7).x * _displacementAmount;
				v.vertex.xyz += v.normal.xyz * value * 5; //Expand

            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.multiplyValue = multiplyValue; //assing the multiply data to the "Input" value, so the surface shader can use it
            o.displacementValue = value; //Pass this info to the surface shader
		}

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
          

            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = lerp(c.rgb,
				float3(.3,.3,1),
				IN.multiplyValue);//the lerp factor is how much we've scaled our vertex

                o.Albedo = lerp(c.rgb * c.a, float3(0, 0, 0), IN.displacementValue); //lerp based on the displacement
                 o.Normal = UnpackNormal (tex2D (_NormalMap, IN.uv_NormalMap));
			o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
