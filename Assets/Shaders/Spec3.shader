
Shader "Surface/Colored Specular Bumped wGloss" {
Properties {
  _MainTex ("Texture(RGB) Gloss(A)", 2D) = "white" {}
  _SpecMap ("SpecMap", 2D) = "white" {}
  _BumpMap ("Normalmap", 2D) = "bump" {}
  _Shininess ("Shininess", Range (0.01, 1)) = 0.078125
}
 
SubShader {
    Tags { "RenderType" = "Opaque" }
 
CGPROGRAM
 
#pragma surface surf ColoredSpecular
 
struct MySurfaceOutput {
    half3 Albedo;
    half3 Normal;
    half3 Emission;
    half Specular;
    half3 GlossColor;
    half Gloss;
    half Alpha;
};
 
inline half4 LightingColoredSpecular (MySurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
{
  half3 h = normalize (lightDir + viewDir);
 
  half diff = max (0, dot (s.Normal, lightDir));
 
  float nh = max (0, dot (s.Normal, h));
  float spec = pow (nh, s.Specular * 128.0f) * s.Gloss;
  half3 specCol = spec * s.GlossColor;
 
  half4 c;
  c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * specCol) * (atten * 2);
  c.a = s.Alpha;
 
  return c;
}
 
inline half4 LightingColoredSpecular_PrePass (MySurfaceOutput s, half4 light)
{
    half3 spec = light.a * s.GlossColor * s.Gloss;
 
    half4 c;
    c.rgb = (s.Albedo * light.rgb + light.rgb * spec);
    c.a = s.Alpha + spec * _SpecColor.a;
 
    return c;
}
 
struct Input {
  float2 uv_MainTex;
  float2 uv_SpecMap;
  float2 uv_BumpMap;
};
 
sampler2D _MainTex;
sampler2D _SpecMap;
sampler2D _BumpMap;
half _Shininess;
 
 
void surf (Input IN, inout MySurfaceOutput o)
{
  float4 diff = tex2D (_MainTex, IN.uv_MainTex);
  o.Albedo = diff.rgb;
 
  half4 spec = tex2D (_SpecMap, IN.uv_SpecMap);
  o.GlossColor = spec.rgb;
  o.Gloss = diff.a;
  o.Specular = _Shininess;
  o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
}
 
ENDCG
}
 
Fallback "Diffuse"
}