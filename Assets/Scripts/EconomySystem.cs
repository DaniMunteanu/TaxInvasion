using System;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class EconomySystem : MonoBehaviour
{
    public int currentCredits;

    public UnityEvent<int> creditsSpent;
    public UnityEvent<int> creditsEarned;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        creditsEarned.Invoke(creditsEarnedOnDeath);
    }
    
}
