using UnityEngine;
using UnityEngine.AI;

public class KatanaAgent : Agent
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
    {
        base.Start();
        health.maxHitpoints = characterStats.katanaAgentStats.maxHealth;
        health.currentHitpoints = characterStats.katanaAgentStats.maxHealth;
        damage = characterStats.katanaAgentStats.damage;
        treasureDamage = characterStats.katanaAgentStats.treasureDamage;
        creditsDroppedOnDeath = characterStats.katanaAgentStats.creditsDroppedOnDeath;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

}
