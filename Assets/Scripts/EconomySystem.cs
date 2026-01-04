using System;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class EconomySystem : MonoBehaviour
{
    public int currentCredits;

    public UnityEvent<int> purchaseMade;
    public UnityEvent creditsModified;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        purchaseMade.AddListener(OnPurchaseMade);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterCharacterDeath(Character character)
    {
        character.characterDead.AddListener(OnCharacterDead);
    }

    void OnCharacterDead(int creditsEarnedOnDeath)
    {
        currentCredits = Mathf.Clamp(currentCredits + creditsEarnedOnDeath, 0, 999);
        creditsModified.Invoke();
    }

    void OnPurchaseMade(int creditsSpent)
    {
        currentCredits = Mathf.Clamp(currentCredits - creditsSpent, 0, 999);
        creditsModified.Invoke();
    }
    
}
