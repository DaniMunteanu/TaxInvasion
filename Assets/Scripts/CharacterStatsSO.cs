using System;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatsSO : ScriptableObject
{
    public CutlassPirateStats cutlassPirateStats;
    public KatanaAgentStats katanaAgentStats;
    public float treasureHealth = 10;
    public int startCredits = 100;
}

[Serializable]
public class CutlassPirateStats
{
    public float maxHealth = 20;
    public float damage = 1;
    public int price = 50;

    public float healthUpgradeBonus = 20;
    public int healthUpgradePrice = 20;

    public float damageUpgradeBonus = 5;
    public int damageUpgradePrice = 20;

    public float lifestealUpgradeMultiplier = 0.5f;
    public int lifestealUpgradePrice = 30;
    public int spinUpgradePrice = 50;
    public float captainPassiveHealAmmount = 0.1f;
    public int captainUpgradePrice = 50;
}

[Serializable]
public class KatanaAgentStats
{
    public float maxHealth = 15;
    public float damage = 5;
    public float treasureDamage = 2;
    public int creditsDroppedOnDeath = 10;
}