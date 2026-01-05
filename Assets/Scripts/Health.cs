using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    public float maxHitpoints;
    [SerializeField]
    public float currentHitpoints;
    public UnityEvent healthChanged;
    public UnityEvent healthDepleted;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHitpoints = maxHitpoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damageHitpoints)
    {
        currentHitpoints = Mathf.Clamp(currentHitpoints - damageHitpoints, 0, maxHitpoints);
        healthChanged.Invoke();

        if (currentHitpoints == 0.0)
            healthDepleted.Invoke();
    }

    void Heal(float healHitpoints)
    {
        currentHitpoints = Mathf.Clamp(currentHitpoints + healHitpoints, 0, maxHitpoints);
        healthChanged.Invoke();
    }

    void IncreaseMaxHealth(float bonusHitpoints)
    {
        maxHitpoints += bonusHitpoints;
        Heal(bonusHitpoints);
    }
}
