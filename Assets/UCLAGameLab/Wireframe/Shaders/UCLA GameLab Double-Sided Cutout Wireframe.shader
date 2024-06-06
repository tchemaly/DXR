Shader "UCLA Game Lab/Wireframe/Double-Sided Cutout"
{
    Properties
    {
        _Color("Line Color", Color) = (1,1,1,1)
        _MainTex("Main Texture", 2D) = "white" {}
        _Thickness("Thickness", Float) = 1
    }

        SubShader
        {
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
            LOD 200

            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off

            CGPROGRAM
            #pragma surface surf Standard keepalpha
            #pragma target 3.0

            struct Input
            {
                float2 uv_MainTex;
            };

            sampler2D _MainTex;
            fixed4 _Color;
            float _Thickness;

            void surf(Input IN, inout SurfaceOutputStandard o)
            {
                // Corrected UV-based wireframe effect
                float2 uv = IN.uv_MainTex * _Thickness; // Scaled by thickness for visibility
                float edgeWidth = fwidth(uv.x) + fwidth(uv.y);
                float edgeMin = 0.01;
                float edgeMax = 0.02;
                float mask = smoothstep(edgeMin, edgeMax, edgeWidth);

                o.Albedo = _Color.rgb;
                o.Alpha = mask * _Color.a; // Adjust alpha based on mask
            }
            ENDCG
        }
            FallBack "Diffuse"
}
