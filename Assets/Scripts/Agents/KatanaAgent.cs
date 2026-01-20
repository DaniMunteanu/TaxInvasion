using UnityEngine;
using UnityEngine.AI;

public class KatanaAgent : Agent
{
    public int difficultyLevel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
    {
        base.Start();

        switch (difficultyLevel)
        {
            case 1:
                health.maxHitpoints = characterStats.katanaAgent1Stats.maxHealth;
                health.currentHitpoints = characterStats.katanaAgent1Stats.maxHealth;
                damage = characterStats.katanaAgent1Stats.damage;
                treasureDamage = characterStats.katanaAgent1Stats.treasureDamage;
                creditsDroppedOnDeath = characterStats.katanaAgent1Stats.creditsDroppedOnDeath;
                break;
            case 2:
                health.maxHitpoints = characterStats.katanaAgent2Stats.maxHealth;
                health.currentHitpoints = characterStats.katanaAgent2Stats.maxHealth;
                damage = characterStats.katanaAgent2Stats.damage;
                treasureDamage = characterStats.katanaAgent2Stats.treasureDamage;
                creditsDroppedOnDeath = characterStats.katanaAgent2Stats.creditsDroppedOnDeath;
                break;
        }
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

}
