using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradePage : MonoBehaviour
{
    public CharacterStatsSO characterStats;
    public EconomySystem economySystem;

    [SerializeField]
    Button healthButton;
    [SerializeField]
    Button damageButton;
    [SerializeField]
    Button lifestealButton;
    [SerializeField]
    Button upgrade1Button;
    [SerializeField]
    Button upgrade2Button;

    private TooltipTrigger healthTooltipTrigger;
    private TooltipTrigger damageTooltipTrigger;
    private TooltipTrigger lifestealTooltipTrigger;
    private TooltipTrigger upgrade1TooltipTrigger;
    private TooltipTrigger upgrade2TooltipTrigger;

    [SerializeField]
    Image healthPath;
    [SerializeField]
    Image damagePath;
    [SerializeField]
    Image upgrade1Path;
    [SerializeField]
    Image upgrade2Path;

    [SerializeField]
    Anchor anchor1;
    [SerializeField]
    Anchor anchor2;

    public UnityEvent healthUpgradeBought;
    public UnityEvent damageUpgradeBought;
    public UnityEvent lifestealUpgradeBought;
    public UnityEvent upgrade1Bought;
    public UnityEvent upgrade2Bought;

    private bool lifestealPicked = false;

    private int healthUpgradePrice;
    private int damageUpgradePrice;
    private int lifestealUpgradePrice;
    private int upgrade1Price;
    private int upgrade2Price;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        transform.localScale = new Vector3(1, 1, 1);
        transform.localPosition = new Vector3(0, 0, 0);

        healthUpgradePrice = characterStats.cutlassPirateStats.healthUpgradePrice;
        damageUpgradePrice = characterStats.cutlassPirateStats.damageUpgradePrice;
        lifestealUpgradePrice = characterStats.cutlassPirateStats.lifestealUpgradePrice;
        upgrade1Price = characterStats.cutlassPirateStats.spinUpgradePrice;
        upgrade2Price = characterStats.cutlassPirateStats.captainUpgradePrice;

        healthTooltipTrigger.costText = healthUpgradePrice.ToString();
        damageTooltipTrigger.costText = damageUpgradePrice.ToString();
        lifestealTooltipTrigger.costText = lifestealUpgradePrice.ToString();
        upgrade1TooltipTrigger.costText = upgrade1Price.ToString();
        upgrade2TooltipTrigger.costText = upgrade2Price.ToString();
    }

    void Awake()
    {
        InitializeButtonsData();
    }

    void InitializeButtonsData()
    {
        lifestealButton.interactable = false;
        upgrade1Button.interactable = false;
        upgrade2Button.interactable = false;

        healthButton.onClick.AddListener(OnHealthButtonPressed);
        damageButton.onClick.AddListener(OnDamageButtonPressed);
        lifestealButton.onClick.AddListener(OnLifestealButtonPressed);
        upgrade1Button.onClick.AddListener(OnUpgrade1ButtonPressed);
        upgrade2Button.onClick.AddListener(OnUpgrade2ButtonPressed);

        healthTooltipTrigger = healthButton.gameObject.GetComponent<TooltipTrigger>();
        damageTooltipTrigger = damageButton.gameObject.GetComponent<TooltipTrigger>();
        lifestealTooltipTrigger = lifestealButton.gameObject.GetComponent<TooltipTrigger>();
        upgrade1TooltipTrigger = upgrade1Button.gameObject.GetComponent<TooltipTrigger>();
        upgrade2TooltipTrigger = upgrade2Button.gameObject.GetComponent<TooltipTrigger>();
    }

    void HighlightPath(Image path)
    {
        foreach (Transform segment in path.transform)
        {
            RawImage currentSegment = segment.GetComponent<RawImage>();
            Color segmentColor = currentSegment.GetComponent<RawImage>().color;
            segmentColor.a = 1;
            currentSegment.GetComponent<RawImage>().color = segmentColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnHealthButtonPressed()
    {
        if(economySystem.currentCredits < healthUpgradePrice)
            return;

        economySystem.purchaseMade.Invoke(healthUpgradePrice);

        if(lifestealPicked == false)
            lifestealButton.interactable = true;

        SpriteState ss = new SpriteState();
        ss.disabledSprite = healthButton.spriteState.highlightedSprite;
        healthButton.spriteState = ss;
        healthButton.interactable = false;

        HighlightPath(healthPath);

        healthUpgradeBought.Invoke();
    }

    void OnDamageButtonPressed()
    {
        if(economySystem.currentCredits < damageUpgradePrice)
            return;
        
        economySystem.purchaseMade.Invoke(damageUpgradePrice);

        if(lifestealPicked == false)
            lifestealButton.interactable = true;
            
        SpriteState ss = new SpriteState();
        ss.disabledSprite = damageButton.spriteState.highlightedSprite;
        damageButton.spriteState = ss;
        damageButton.interactable = false;

        HighlightPath(damagePath);

        damageUpgradeBought.Invoke();
    }

    void OnLifestealButtonPressed()
    {
        if(economySystem.currentCredits < lifestealUpgradePrice)
            return;

        economySystem.purchaseMade.Invoke(lifestealUpgradePrice);

        lifestealPicked = true;

        upgrade1Button.interactable = true;
        upgrade2Button.interactable = true;
        SpriteState ss = new SpriteState();
        ss.disabledSprite = lifestealButton.spriteState.highlightedSprite;
        lifestealButton.spriteState = ss;
        lifestealButton.interactable = false;

        HighlightPath(upgrade1Path);
        HighlightPath(upgrade2Path);

        lifestealUpgradeBought.Invoke();
    }

    void OnUpgrade1ButtonPressed()
    {
        if(economySystem.currentCredits < upgrade1Price)
            return;

        economySystem.purchaseMade.Invoke(upgrade1Price);

        upgrade2Button.interactable = false;

        anchor2.animator.SetTrigger("anchorDown");

        SpriteState ss = new SpriteState();
        ss.disabledSprite = upgrade1Button.spriteState.highlightedSprite;
        upgrade1Button.spriteState = ss;
        upgrade1Button.interactable = false;

        upgrade1Bought.Invoke();
    }

    void OnUpgrade2ButtonPressed()
    {
        if(economySystem.currentCredits < upgrade2Price)
            return;

        economySystem.purchaseMade.Invoke(upgrade2Price);

        upgrade1Button.interactable = false;

        anchor1.animator.SetTrigger("anchorDown");

        SpriteState ss = new SpriteState();
        ss.disabledSprite = upgrade2Button.spriteState.highlightedSprite;
        upgrade2Button.spriteState = ss;
        upgrade2Button.interactable = false;

        upgrade2Bought.Invoke();
    }
}
