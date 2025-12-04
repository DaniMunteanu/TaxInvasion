using UnityEngine;

public class Pirate : Character
{
    [SerializeField]
    Shader highlightShader;

    private Material defaultMaterial;
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected new void Start()
    {
        
    }

    protected void Awake()
    {
        defaultMaterial = new Material(Shader.Find("Sprites/Default"));
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    protected new void Update()
    {
        
    }

    public void Highlight()
    {
        spriteRenderer.material = new Material(highlightShader);
    }

    public void UnHighlight()
    {
        spriteRenderer.material = defaultMaterial;
    }

}
