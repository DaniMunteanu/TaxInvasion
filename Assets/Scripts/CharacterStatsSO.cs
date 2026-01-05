using System;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatsSO : ScriptableObject
{
    public CutlassPirateStats cutlassPirateStats;
    public KatanaAgentStats katanaAgentStats;
    public float treasureHealth;
    public int startCredits;
}

[Serializable]
public class CutlassPirateStats
{
    public float maxHealth;
    public float damage;
    public int price;

    public float healthUpgradeBonus;
    public int healthUpgradePrice;

    public float damageUpgradeBonus;
    public int damageUpgradePrice;

    public float lifestealUpgradeMultiplier;
    public int lifestealUpgradePrice;
    public int spinUpgradePrice;
    public float captainPassiveHealAmmount;
    public int captainUpgradePrice;
}

[Serializable]
public class KatanaAgentStats
{
    public float maxHealth;
    public float damage;
    public float treasureDamage;
    public int creditsDroppedOnDeath;
}