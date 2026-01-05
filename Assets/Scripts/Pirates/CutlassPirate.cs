using System;
using UnityEngine;

public class CutlassPirate : Pirate
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected new void Start()
    {
        base.Start();
        
    }

    protected new void Awake()
    {
        base.Awake();
        health.maxHitpoints = characterStats.cutlassPirateStats.maxHealth;
        health.currentHitpoints = characterStats.cutlassPirateStats.maxHealth;
        damage = characterStats.cutlassPirateStats.damage;
        price = characterStats.cutlassPirateStats.price;
        creditsDroppedOnDeath = price/2;
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }

}
