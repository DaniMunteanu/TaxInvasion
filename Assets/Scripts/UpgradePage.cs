using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradePage : MonoBehaviour
{
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

    [SerializeField]
    Image healthPath;
    [SerializeField]
    Image damagePath;
    [SerializeField]
    Image upgrade1Path;
    [SerializeField]
    Image upgrade2Path;

    public UnityEvent upgrade1Bought;

    private bool lifestealPicked = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.localScale = new Vector3(1, 1, 1);
        transform.localPosition = new Vector3(0, 0, 0);
    }

    void Awake()
    {
        lifestealButton.interactable = false;
        upgrade1Button.interactable = false;
        upgrade2Button.interactable = false;

        healthButton.onClick.AddListener(OnHealthButtonPressed);
        damageButton.onClick.AddListener(OnDamageButtonPressed);
        lifestealButton.onClick.AddListener(OnLifestealButtonPressed);
        upgrade1Button.onClick.AddListener(OnUpgrade1ButtonPressed);
        upgrade2Button.onClick.AddListener(OnUpgrade2ButtonPressed);
    }

    void HighlightPath(Image path)
    {
        foreach (Transform segment in path.transform)
        {
            RawImage currentSegment = segment.GetComponent<RawImage>();
            currentSegment.GetComponent<RawImage>().color = new Color(0,0,0,1);
            //Invoke("HighlightPathSegment",2);
        }
    }

    void HighlightPathSegment()
    {
        //currentSegment.color = new Color(255,255,255,1);
        //currentSegment.GetComponent<RawImage>().color = new Color(0,0,0,1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnHealthButtonPressed()
    {
        if(lifestealPicked == false)
            lifestealButton.interactable = true;

        SpriteState ss = new SpriteState();
        ss.disabledSprite = healthButton.spriteState.highlightedSprite;
        healthButton.spriteState = ss;
        healthButton.interactable = false;

        HighlightPath(healthPath);
    }

    void OnDamageButtonPressed()
    {
        if(lifestealPicked == false)
            lifestealButton.interactable = true;
            
        SpriteState ss = new SpriteState();
        ss.disabledSprite = damageButton.spriteState.highlightedSprite;
        damageButton.spriteState = ss;
        damageButton.interactable = false;

        HighlightPath(damagePath);
    }

    void OnLifestealButtonPressed()
    {
        lifestealPicked = true;

        upgrade1Button.interactable = true;
        upgrade2Button.interactable = true;
        SpriteState ss = new SpriteState();
        ss.disabledSprite = lifestealButton.spriteState.highlightedSprite;
        lifestealButton.spriteState = ss;
        lifestealButton.interactable = false;

        HighlightPath(upgrade1Path);
        HighlightPath(upgrade2Path);
    }

    void OnUpgrade1ButtonPressed()
    {
        upgrade2Button.interactable = false;
        SpriteState ss = new SpriteState();
        ss.disabledSprite = upgrade1Button.spriteState.highlightedSprite;
        upgrade1Button.spriteState = ss;
        upgrade1Button.interactable = false;

        upgrade1Bought.Invoke();
    }

    void OnUpgrade2ButtonPressed()
    {
        upgrade1Button.interactable = false;
        SpriteState ss = new SpriteState();
        ss.disabledSprite = upgrade2Button.spriteState.highlightedSprite;
        upgrade2Button.spriteState = ss;
        upgrade2Button.interactable = false;
    }
}
