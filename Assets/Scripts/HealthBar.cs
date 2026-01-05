using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    Image primaryFill;
    [SerializeField]
    Image secondaryFill;
    
    public Health health;
    public bool forCharacter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeFillColors();
        health.healthChanged.AddListener(OnHealthChanged);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeFillColors()
    {
        if(forCharacter == false)
            return;

        GameObject parentCharacter = this.transform.parent.gameObject;
        if (parentCharacter.tag == "Pirate")
        {
            primaryFill.color = Color.green;
            secondaryFill.color = Color.yellowGreen;
        }
        else
        {
            primaryFill.color = Color.red;
            secondaryFill.color = Color.orange;
        }
    }

    void OnHealthChanged()
    {
        primaryFill.fillAmount = health.currentHitpoints / health.maxHitpoints;
        Invoke("UpdateSecondaryFill",0.5f);
    }

    void UpdateSecondaryFill()
    {
        secondaryFill.fillAmount = primaryFill.fillAmount;
    }
}
