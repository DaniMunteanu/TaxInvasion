using UnityEngine;

public class Pirate : Character
{
    [SerializeField]
    Shader highlightShader;
    [SerializeField]
    public PirateProfile pirateProfilePrefab;
    public PirateProfile pirateProfileInstance;
    public Vector3Int gridPosition;
    public int price;
    private Material defaultMaterial;
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected new void Start()
    {
        base.Start();
        creditsDroppedOnDeath = price/2;
        Highlight();
        pirateProfileInstance.upgradePage.upgrade1Bought.AddListener(OnUpgrade1Bought);
        health.healthDepleted.AddListener(OnPirateDead);
        pirateProfileInstance.healthBar.health = health;
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

    public void OnPirateDead()
    {
        pirateProfileInstance.destroyProfilePage.Invoke(gridPosition);
    }

}
