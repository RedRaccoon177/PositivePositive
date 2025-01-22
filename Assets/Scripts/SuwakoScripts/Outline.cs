using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Outline : MonoBehaviour
{
    public Color outlineColor = Color.black;
    public float outlineSize = 1f;

    private MaterialPropertyBlock mpb;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        mpb = new MaterialPropertyBlock();
    }

    void Update()
    {
        mpb.SetColor("_OutlineColor", outlineColor);
        mpb.SetFloat("_OutlineThickness", outlineSize);
        sr.SetPropertyBlock(mpb);
    }
}
