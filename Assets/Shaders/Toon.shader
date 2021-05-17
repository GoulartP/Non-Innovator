Shader "Custom/Toon"
{/*
  =====================================================
  SHADER CARTOON UTILIZANDO ESTRUTURA DE VERTEX SHADER
  =====================================================
 */
    
    Properties //Definindo as informações do inspector
    {
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Brightness("Brightness", Range(0,1)) = 0.3
        _Strength("Strength",Range(0,1)) = 0.5
        _Color("Color", Color) = (1,1,1,1)
        _Detail("Detail",Range(0,1)) = 0.3
        _Suavizar("Suavizar",Range(0,10))=1
    }
        SubShader
        {
            Tags { "LightMode" = "ForwardBase" }
            Lighting On
            LOD 100

          Pass   // Estrutura Vertex Shader
          {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"             //Inclusão de bibliotecas de maneira manual, diferente do surface shader
                #include "UnityLightingCommon.cginc" //Inclusão de bibliotecas de maneira manual, diferente do surface shader


           /*
           ========================================================================
           STRUCT APP E V2F É A ENTRADA E SAÍDA DE INFORMAÇÔES PARA O VERTEX SHADER
           ========================================================================
          */
                struct appdata
                {
                    float2 uv     : TEXCOORD0; //TEXCOORD0 informando qual a função da variavel criada, neste caso, coordenada da textura principal
                    float3 normal : NORMAL;
                    float4 vertex : POSITION;
                };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                half3 worldNormal : NORMAL;
                float4 color : COLOR;
            };

            /*
            ====================================
            INICIALIZANDO VARIÁVEIS NO INSPECTOR
            ====================================
            */            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Brightness;
            float _Strength;
            float4 _Color;
            float _Detail;
            float _Suavizar;

            /*
            ===========================================================
            FUNÇÂO TOON È A RESPONSÁVEL PELO EFEITO CARTOON NOS OBJETOS 
            ===========================================================           
            */
            float Toon(float3 normal, float3 lightDir)
            {
                float NdotL = max(0.0, dot(normalize(normal), normalize(lightDir)));
                return floor((NdotL / _Detail) + _Suavizar); //Suavizar utilizado para controlar a dureza do outline
            }


            /*
            =========================================================
            ENTRADAS informação para cada vertice do modelo
            =========================================================
            */
            v2f vert(appdata v) 
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.color = _LightColor0;

                return o;       //Entrada para o fragmente shader

            }


            /*
            =========================================================
            Saída das informações recebidas pelo "v2f"
            =========================================================
           */
            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col *= Toon(i.worldNormal, _WorldSpaceLightPos0.xyz) * _Strength * _Color + _Brightness * i.color;
                return col;

            }

            ENDCG
          }

        }
}
