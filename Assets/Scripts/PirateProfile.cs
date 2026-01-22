using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PirateProfile : MonoBehaviour
{
    public UpgradePage upgradePage;
    public UnityEvent<Vector3Int> destroyProfilePage;

    public HealthBar healthBar;

    [SerializeField]
    private Button sellButton;

    [SerializeField]
    TooltipTrigger tooltipTrigger;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.localScale = new Vector3(1, 1, 1);
        transform.localPosition = new Vector3(0, 0, 0);
        sellButton.onClick.AddListener(OnSellButtonPressed);

        tooltipTrigger.headerText = null;
        tooltipTrigger.descriptionText = healthBar.health.currentHitpoints + "/" + healthBar.health.maxHitpoints;
        tooltipTrigger.costText = null;
        healthBar.health.healthChanged.AddListener(UpdateTooltipTrigger);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSellButtonPressed()
    {
        healthBar.health.healthDepleted.Invoke();
        SoundManager.PlaySound(SoundType.TRANSACTION);
    }

    void UpdateTooltipTrigger()
    {
        tooltipTrigger.descriptionText = healthBar.health.currentHitpoints + "/" + healthBar.health.maxHitpoints;
        if (tooltipTrigger.isShown)
            TooltipSystem.UpdateText(null, tooltipTrigger.descriptionText, null);
    }
}
