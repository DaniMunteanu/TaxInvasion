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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.localScale = new Vector3(1, 1, 1);
        transform.localPosition = new Vector3(0, 0, 0);
        sellButton.onClick.AddListener(OnSellButtonPressed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSellButtonPressed()
    {
        healthBar.health.healthDepleted.Invoke();
    }
}
