using UnityEngine;
using UnityEngine.Events;

public abstract class Pirate : Character
{
    [SerializeField]
    Shader highlightShader;
    [SerializeField]
    public PirateProfile pirateProfilePrefab;
    public PirateProfile pirateProfileInstance;
    public Vector3Int gridPosition;
    public UnityEvent<Vector3Int> placeBuffTiles;
    public UnityEvent<Vector3Int> removeBuffTiles;
    public int price;
    private Material defaultMaterial;
    private SpriteRenderer spriteRenderer;
    protected float lifestealMultiplier = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected new void Start()
    {
        base.Start();
        creditsDroppedOnDeath = price/2;
        Highlight();

        pirateProfileInstance.upgradePage.healthUpgradeBought.AddListener(OnHealthUpgradeBought);
        pirateProfileInstance.upgradePage.damageUpgradeBought.AddListener(OnDamageUpgradeBought);
        pirateProfileInstance.upgradePage.lifestealUpgradeBought.AddListener(OnLifestealUpgradeBought);
        pirateProfileInstance.upgradePage.upgrade1Bought.AddListener(OnUpgrade1Bought);
        pirateProfileInstance.upgradePage.upgrade2Bought.AddListener(OnUpgrade2Bought);

        health.healthDepleted.AddListener(OnPirateDead);
        pirateProfileInstance.healthBar.health = health;
    }

    protected abstract void OnHealthUpgradeBought();
    protected abstract void OnDamageUpgradeBought();
    protected abstract void OnLifestealUpgradeBought();
    protected abstract void OnUpgrade1Bought();
    protected abstract void OnUpgrade2Bought();

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

    public new void DealDamageToEnemy(Character targetedEnemy)
    {
        base.DealDamageToEnemy(targetedEnemy);
        if (lifestealMultiplier != 0.0f)
            health.Heal(damage * lifestealMultiplier);
    }

    public void GrantArmorBuff()
    {
        
    }

    public void RemoveArmorBuff()
    {
        damageReduction = 0;
    }

}
