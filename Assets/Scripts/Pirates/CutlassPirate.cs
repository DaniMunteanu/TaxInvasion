using System;
using UnityEngine;

public class CutlassPirate : Pirate
{
    private bool isCaptain = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected new void Start()
    {
        base.Start();
        health.healthDepleted.AddListener(OnHealthDepleted);
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

    protected override void OnHealthUpgradeBought()
    {
        health.IncreaseMaxHealth(characterStats.cutlassPirateStats.healthUpgradeBonus);
    }
    protected override void OnDamageUpgradeBought()
    {
        damage += characterStats.cutlassPirateStats.damageUpgradeBonus;
    }
    protected override void OnLifestealUpgradeBought()
    {
        lifestealMultiplier = characterStats.cutlassPirateStats.lifestealUpgradeMultiplier;
    }
    protected override void OnUpgrade1Bought()
    {
        animator.SetBool("usesAttack1", true);
        SoundManager.PlaySound(SoundType.SPECIAL_UPGRADE);
    }
    protected override void OnUpgrade2Bought()
    {
        animator.SetBool("usesAttack2", true);
        isCaptain = true;
        placeBuffTiles.Invoke(gridPosition);
        SoundManager.PlaySound(SoundType.SPECIAL_UPGRADE);
    }

    public new void DealDamageToEnemy(Character targetedEnemy)
    {
        base.DealDamageToEnemy(targetedEnemy);
        SoundManager.PlaySound(SoundType.CUTLASS_CLASH);
    } 

    public new void GrantArmorBuff()
    {
        base.GrantArmorBuff();
        damageReduction = characterStats.cutlassPirateStats.captainDamageReduction;
    }

    public new void OnHealthDepleted()
    {
        if (isCaptain)
            removeBuffTiles.Invoke(gridPosition);
        
        characterDead.Invoke(creditsDroppedOnDeath);
        SoundManager.PlaySound(SoundType.CHARACTER_DEAD);
        Destroy(this.gameObject);
    }
}
