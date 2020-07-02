using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public string ShaderName;
    public Texture2D VignetteMask;

    private Material shaderMaterial;

    public float TintFactor
    {
        get { return shaderMaterial.GetFloat("_TintFactor"); }
        set { shaderMaterial.SetFloat("_TintFactor", value); }
    }
    public Color TintColor
    {
        get { return shaderMaterial.GetColor("_Color"); }
        set { shaderMaterial.SetColor("_Color", value); }
    }


    void Start()
    {
        shaderMaterial = new Material(Shader.Find(ShaderName));

        shaderMaterial.SetTexture("_VignetteMask", VignetteMask);
    }


    void Update()
    {
        
    }


    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, shaderMaterial);
    }
}
