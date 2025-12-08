using UnityEngine;

public class Pirate : Character
{
    [SerializeField]
    Shader highlightShader;
    [SerializeField]
    public UpgradePage upgradePagePrefab;
    public UpgradePage upgradePageInstance;
    private Material defaultMaterial;
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected new void Start()
    {
        Highlight();
        upgradePageInstance.upgrade1Bought.AddListener(OnUpgrade1Bought);
    }

    protected void OnUpgrade1Bought()
    {
        animator.SetBool("usesAttack1", true);
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
